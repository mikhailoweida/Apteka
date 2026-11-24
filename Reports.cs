namespace Apteka;

public class Report
{
    readonly string _title;
    /// <summary>Stored in Russian, shown in the current UI language.</summary>
    public string Title => Loc.T(_title);
    public string Sql { get; set; }
    public Report(string title, string sql) { _title = title; Sql = sql; }

    public static readonly List<Report> All = new()
    {
        new("1. Все данные из таблицы лекарств", "SELECT * FROM Medicine;"),
        new("2. Заказчики: «А» в фамилии и возраст старше 40",
            "SELECT id, last_name, first_name, middle_name, age\nFROM Patient\nWHERE last_name LIKE N'%А%' AND age > 40;"),
        new("3. Цена 100-500 или название «Аналь...» / «...зол»",
            "SELECT name, category, price\nFROM v_AllMedicines\nWHERE price BETWEEN 100 AND 500 OR name LIKE N'Аналь%' OR name LIKE N'%зол';"),
        new("4. Заказы с информацией о лекарствах и покупателях",
            "SELECT o.id, o.order_date, o.order_status, p.last_name, p.first_name, m.name, oc.quantity\nFROM [Order] o\nJOIN Patient p ON p.id = o.patient_id\nJOIN OrderComposition oc ON oc.order_id = o.id\nJOIN Medicine m ON m.id = oc.medicine_id;"),
        new("5. Технологии и лекарства (LEFT JOIN)",
            "SELECT t.id, t.technology_name, m.name, cm.type\nFROM PreparationTechnology t\nLEFT JOIN CompoundedMedicine cm ON cm.technology_id = t.id\nLEFT JOIN Medicine m ON m.id = cm.medicine_id;"),
        new("6. ФИО врачей одним столбцом",
            "SELECT CONCAT_WS(N' ', last_name, first_name, middle_name) AS [ФИО]\nFROM Doctor;"),
        new("7. Лекарства дешевле средней цены",
            "SELECT name, category, price\nFROM v_AllMedicines\nWHERE price < (SELECT AVG(price) FROM v_AllMedicines);"),
        new("8. Статистика по медикаментам",
            "SELECT COUNT(*) AS total_items, AVG(price) AS avg_price, MIN(price) AS min_price, MAX(price) AS max_price\nFROM v_AllMedicines;"),
        new("9. Категории > 10 наименований",
            "SELECT category, COUNT(*) AS items_count\nFROM v_AllMedicines\nGROUP BY category\nHAVING COUNT(*) > 10;"),
        new("10. Уникальные диагнозы", "SELECT DISTINCT name FROM Diagnosis;"),
        new("11. Заказы: дата ↓, стоимость ↑",
            "SELECT o.id, o.order_date, SUM(oc.quantity * m.price) AS total_price\nFROM [Order] o\nJOIN OrderComposition oc ON oc.order_id = o.id\nJOIN v_AllMedicines m ON m.id = oc.medicine_id\nGROUP BY o.id, o.order_date\nORDER BY o.order_date DESC, total_price ASC;"),
        new("12. Топ-5 самых дорогих со 2-го места",
            "SELECT name, category, price\nFROM v_AllMedicines\nORDER BY price DESC\nOFFSET 1 ROWS FETCH NEXT 5 ROWS ONLY;"),
        new("13. Покупатели с > 3 заказов",
            "SELECT p.id, p.last_name, p.first_name, COUNT(*) AS orders_count\nFROM Patient p\nJOIN [Order] o ON o.patient_id = p.id\nGROUP BY p.id, p.last_name, p.first_name\nHAVING COUNT(*) > 3\nORDER BY orders_count DESC;"),
        new("14. Топ-3 самых частых лекарств",
            "SELECT TOP 3 m.name, COUNT(*) AS times_ordered\nFROM OrderComposition oc\nJOIN Medicine m ON m.id = oc.medicine_id\nGROUP BY m.name\nORDER BY COUNT(*) DESC;"),
        new("15. Лекарства с остатком < 10", "SELECT name, stock_quantity FROM v_AllMedicines WHERE stock_quantity < 10;"),
        new("16. Компоненты, используемые < 5 раз",
            "SELECT c.id, c.name, COUNT(cmc.id) AS used_times\nFROM Component c\nLEFT JOIN CompoundedMedicineComposition cmc ON cmc.component_id = c.id\nGROUP BY c.id, c.name\nHAVING COUNT(cmc.id) < 5;"),
        new("17. Категоризация цены (CASE)",
            "SELECT name, price,\n CASE WHEN price < 200 THEN N'бюджетное'\n      WHEN price BETWEEN 200 AND 800 THEN N'среднее'\n      ELSE N'дорогое' END AS price_class\nFROM v_AllMedicines;"),
        new("18. Заказы в производстве / в обработке",
            "SELECT id, order_date, order_status FROM [Order] WHERE order_status IN (N'processing', N'in_production');"),
        new("19. Статистика по категориям",
            "SELECT category, COUNT(*) AS items_count, AVG(price) AS avg_price, MIN(price) AS min_price, MAX(price) AS max_price\nFROM v_AllMedicines\nGROUP BY category;"),
        new("20. Лекарства с макс. и мин. сроком годности",
            "SELECT name FROM v_AllMedicines\nWHERE expiry_date = (SELECT MAX(expiry_date) FROM v_AllMedicines)\n   OR expiry_date = (SELECT MIN(expiry_date) FROM v_AllMedicines);"),
    };
}
