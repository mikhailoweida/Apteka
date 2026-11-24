using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace Apteka;

public sealed record FriendlyError(string Title, string Message, MessageBoxImage Icon);

/// <summary>
/// Turns raw SQL Server / .NET exceptions into messages a pharmacist can understand:
/// what happened + what to do about it. The raw text is only shown for errors we do not recognise
/// (and always goes to ActionLog).
/// </summary>
public static class DbErrors
{
    static readonly HashSet<int> Connection = new() { -1, 2, 20, 26, 40, 52, 53, 64, 233, 1225, 10053, 10054, 10060, 10061 };
    static readonly HashSet<int> Overflow   = new() { 220, 232, 245, 8114, 8115 };
    static readonly HashSet<int> BadDate    = new() { 241, 242 };
    static readonly HashSet<int> Syntax     = new() { 102, 105, 156, 170 };
    static readonly HashSet<int> NoRights   = new() { 229, 230, 262 };

    // ---------- helpers ----------
    static string Grab(string text, string pattern)
    {
        var m = Regex.Match(text ?? "", pattern);
        return m.Success ? m.Groups[1].Value : null;
    }

    static SqlException FindSql(Exception ex)
    {
        for (var e = ex; e != null; e = e.InnerException)
            if (e is SqlException s) return s;
        return null;
    }

    static Entity FindEntity(string rawTable)
    {
        if (string.IsNullOrEmpty(rawTable)) return null;
        string n = rawTable.Split('.').Last().Trim('[', ']', '"');
        return Entity.All.FirstOrDefault(e => e.Table.Trim('[', ']').Equals(n, StringComparison.OrdinalIgnoreCase));
    }

    static string TableTitle(string rawTable) =>
        FindEntity(rawTable)?.Title ?? rawTable?.Split('.').Last().Trim('[', ']', '"');

    static string ColumnTitle(string rawTable, string col)
    {
        if (col == null) return null;
        var f = FindEntity(rawTable)?.Fields.FirstOrDefault(x => x.Col.Equals(col, StringComparison.OrdinalIgnoreCase));
        return f?.Header ?? col;
    }

    static FriendlyError Warn(string titleRu, string msgRu, params object[] args) =>
        new(Loc.T(titleRu), Loc.T(msgRu, args), MessageBoxImage.Warning);

    static FriendlyError Err(string titleRu, string msgRu, params object[] args) =>
        new(Loc.T(titleRu), Loc.T(msgRu, args), MessageBoxImage.Error);

    // ---------- main entry ----------
    public static FriendlyError Describe(Exception ex)
    {
        if (ex is InputError)
            return new(Loc.T("Проверьте данные"), ex.Message, MessageBoxImage.Warning);

        var se = FindSql(ex);
        if (se == null)
        {
            if (ex is TimeoutException)
                return Warn("Сервер отвечает слишком долго",
                    "База данных не ответила вовремя.\n\nЧто делать: подождите немного и повторите попытку. Если ошибка повторяется — сообщите администратору.");
            return Err("Ошибка",
                "Произошла непредвиденная ошибка.\n\nЧто делать: повторите попытку. Если ошибка повторяется — покажите администратору текст ниже.\n\nТехнические подробности:\n{0}",
                ex.Message);
        }

        string m = se.Message ?? "";
        int n = se.Number;

        // ----- Constraint violations -----
        if (n == 547)
        {
            string table = Grab(m, "table \"([^\"]+)\"");
            string col = Grab(m, "column '([^']+)'");

            // The error from the screenshot: stock would drop below zero.
            if (m.Contains("CK_ReadyMed_qty", StringComparison.OrdinalIgnoreCase))
                return Warn("Недостаточно лекарства на складе",
                    "Остаток лекарства на складе не может стать меньше нуля.\n\nЧто делать: если вы оформляете заказ — уменьшите количество или сначала пополните склад (оформите поставку). Если вы меняете остаток вручную — введите число 0 или больше.");

            if (m.Contains("CHECK constraint", StringComparison.OrdinalIgnoreCase))
            {
                string c = ColumnTitle(table, col);
                return c != null
                    ? Warn("Недопустимое значение",
                        "Значение в поле «{0}» не допускается правилами базы данных (например, отрицательное число или значение вне допустимого диапазона).\n\nЧто делать: исправьте значение и повторите попытку.", c)
                    : Warn("Недопустимое значение",
                        "Одно из введённых значений не допускается правилами базы данных.\n\nЧто делать: проверьте введённые данные (числа, даты, количество) и повторите попытку.");
            }

            if (m.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase))
            {
                string t = TableTitle(table);
                return t != null
                    ? Warn("Запись нельзя удалить",
                        "Эта запись используется в другом месте — в таблице «{0}».\n\nЧто делать: сначала удалите или измените связанные записи, затем повторите удаление.", t)
                    : Warn("Запись нельзя удалить",
                        "Запись нельзя удалить: на неё ссылаются другие таблицы.\n\nЧто делать: сначала удалите или измените связанные записи, затем повторите удаление.");
            }

            if (m.Contains("FOREIGN KEY", StringComparison.OrdinalIgnoreCase))
                return Warn("Связанная запись не найдена",
                    "Одно из выбранных значений (из списка) больше не существует в базе — возможно, его удалили.\n\nЧто делать: закройте окно, обновите данные и выберите значение заново.");

            return Warn("Операция невозможна",
                "Данные нарушают правило целостности базы данных.\n\nЧто делать: проверьте введённые значения и повторите попытку.");
        }

        switch (n)
        {
            case 2627:
            case 2601:
                {
                    string dup = Grab(m, @"duplicate key value is \(([^)]*)\)");
                    string extra = dup != null ? "\n" + Loc.T("Повторяющееся значение: {0}", dup) : "";
                    return new(Loc.T("Такая запись уже существует"),
                        Loc.T("В базе уже есть запись с таким значением, а повторы не допускаются.") + extra + "\n\n" +
                        Loc.T("Что делать: введите другое значение или откройте существующую запись."),
                        MessageBoxImage.Warning);
                }

            case 515:
                {
                    string c = ColumnTitle(Grab(m, "table '([^']+)'"), Grab(m, "column '([^']+)'"));
                    return c != null
                        ? Warn("Не заполнено обязательное поле",
                            "Поле «{0}» обязательно для заполнения.\n\nЧто делать: заполните его и повторите попытку.", c)
                        : Warn("Не заполнено обязательное поле",
                            "Одно из обязательных полей не заполнено.\n\nЧто делать: заполните все поля, отмеченные звёздочкой, и повторите попытку.");
                }

            case 2628:
            case 8152:
                {
                    string c = ColumnTitle(Grab(m, "table '([^']+)'"), Grab(m, "column '([^']+)'"));
                    return c != null
                        ? Warn("Слишком длинное значение",
                            "Текст в поле «{0}» слишком длинный и не помещается в базу данных.\n\nЧто делать: сократите текст и повторите попытку.", c)
                        : Warn("Слишком длинное значение",
                            "Один из введённых текстов слишком длинный и не помещается в базу данных.\n\nЧто делать: сократите текст и повторите попытку.");
                }

            case 1205:
                return Warn("Конфликт с другим пользователем",
                    "Операция отменена: другой пользователь одновременно менял те же данные.\n\nЧто делать: повторите действие.");

            case -2:
                return Warn("Сервер отвечает слишком долго",
                    "База данных не ответила вовремя.\n\nЧто делать: подождите немного и повторите попытку. Если ошибка повторяется — сообщите администратору.");

            case 4060:
                return Err("База данных недоступна",
                    "База данных «apteka» не найдена или у вас нет к ней доступа.\n\nЧто делать: создайте базу и выполните скрипт apteka_app_setup.sql либо обратитесь к администратору.");

            case 18456:
                return Err("Вход на сервер отклонён",
                    "SQL Server не принял учётные данные.\n\nЧто делать: проверьте права вашей учётной записи Windows на сервере или данные в строке подключения (файл Db.cs).");

            case 208:
                return Warn("Таблица не найдена",
                    "Таблица или представление «{0}» не найдены в базе данных.\n\nЧто делать: проверьте название в запросе. Если это служебный объект приложения — выполните скрипт apteka_app_setup.sql.",
                    Grab(m, "name '([^']+)'") ?? "?");

            case 207:
                return Warn("Столбец не найден",
                    "Столбец «{0}» не найден.\n\nЧто делать: проверьте название столбца в запросе.",
                    Grab(m, "name '([^']+)'") ?? "?");

            case 2812:
                return Err("Процедура не найдена",
                    "Хранимая процедура «{0}» не найдена в базе.\n\nЧто делать: выполните скрипт apteka_app_setup.sql в SSMS.",
                    Grab(m, "procedure '([^']+)'") ?? "?");
        }

        if (Connection.Contains(n))
            return Err("Нет связи с базой данных",
                "Не удалось подключиться к серверу базы данных.\n\nЧто делать: убедитесь, что SQL Server (или LocalDB) запущен и компьютер подключён к сети; при необходимости проверьте строку подключения в файле Db.cs.");

        if (Overflow.Contains(n))
            return Warn("Недопустимое число",
                "Введённое число слишком большое или имеет неподходящий формат.\n\nЧто делать: введите меньшее значение и повторите попытку.");

        if (BadDate.Contains(n))
            return Warn("Недопустимая дата",
                "Дата указана неверно или выходит за допустимый диапазон.\n\nЧто делать: выберите дату в календаре и повторите попытку.");

        if (Syntax.Contains(n))
            return Warn("Ошибка в SQL-запросе",
                "В тексте запроса есть синтаксическая ошибка рядом с «{0}».\n\nЧто делать: проверьте запрос (скобки, запятые, ключевые слова) и выполните его снова.",
                Grab(m, "'([^']*)'") ?? "?");

        if (NoRights.Contains(n))
            return Err("Недостаточно прав",
                "У вас нет прав на это действие в базе данных.\n\nЧто делать: обратитесь к администратору.");

        // Messages raised by the database author via RAISERROR/THROW (>= 50000) are already meant for users.
        if (n >= 50000)
            return new(Loc.T("Операция не выполнена"), m, MessageBoxImage.Warning);

        return Err("Ошибка базы данных",
            "Произошла непредвиденная ошибка (код {0}).\n\nЧто делать: повторите попытку. Если ошибка повторяется — покажите администратору текст ниже.\n\nТехнические подробности:\n{1}",
            n, m);
    }
}
