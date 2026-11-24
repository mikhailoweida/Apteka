# Apteka (WPF, .NET 10, SQL Server) — Classic 3D edition

## Run
1. Run `apteka_app_setup.sql` in SSMS (creates AppRole, AppUser, ActionLog, sp_CreateOrder
   and, if missing, the `v_AllMedicines` view). The `apteka` database from Labs 2-4 must already exist.
2. `Db.cs` connects to `(localdb)\MSSQLLocalDB`. If you use another server, change `ConnStr`
   (e.g. `Server=localhost;` or `Server=.\SQLEXPRESS;`).
3. Open `Apteka.sln` in Visual Studio (workload ".NET desktop development") and press F5,
   or run `dotnet run --project Apteka.csproj`.
   The project targets .NET 10; with only .NET 8 installed change `TargetFramework` to `net8.0-windows`.

Test logins: `admin` / `admin123`, `pharm` / `pharm123`, or "Войти как гость".

## Look
Sign-in and change-password windows follow the teacher's sample: key header, blue body, status line with
input language and CapsLock (`LoginWindow.xaml`, `KeyHeader.xaml`, `ChangePasswordWindow.cs`).

Default theme is **Classic**: the Windows 9x/2000 3D look (raised buttons, sunken text boxes,
gray window, Tahoma font) from WPF's built-in `PresentationFramework.Classic`.
Switch in Разное → Настройка (Classic / Light / Dark / Blue, font size); saved in `%AppData%\Apteka\settings.json`.

## Friendly error messages
Database errors are no longer shown raw. `DbErrors.cs` turns them into "what happened + what to do",
e.g. ordering more than is in stock -> "Not enough "Analgin" in stock: you requested 200000000, but only 40 are available."
The order page checks the stock itself first; the database rule `CK_ReadyMed_qty` is only a safety net.
Covered: stock/CHECK rules, delete blocked by references, duplicates, required/too-long/too-big values,
bad dates, no connection / no database / login failed, SQL syntax mistakes in reports, missing objects,
timeouts, deadlocks. Anything unknown shows the raw text at the bottom so it can be shown to the administrator.
To add a rule for a specific constraint, add a branch in `DbErrors.Describe` (see `CK_ReadyMed_qty`).
Order creation now runs in one transaction, so a failed order never leaves an empty `Order` row.

## Languages
Russian (default), English, French, Arabic (right-to-left). Choose on the login window or in Разное -> Настройка.
All texts live in `Translations.cs`: one line per text `Add(russian, english, french, arabic)`. Russian text is the key,
and a missing translation falls back to Russian. To add a language: add it to `Loc.Languages` and a column to each `Add`.
Note: the Yes/No/OK button captions of message boxes come from Windows itself, and ActionLog entries stay in Russian.

## Menu
- Справочники — dictionary tables + submenu "Связующие таблицы (M:N)"
- Документы — Оформление заказа, Журналы документов (orders, prescriptions, requests, supply), Отчёты и запросы, Экспорт CSV
- Аналитика — dashboard of 4 charts on one page: bar (medicines per category), pie (orders by status),
  line (orders by day, date filter), bar (average price); auto refresh (5 s), PNG export of the whole dashboard.
  The charts are drawn in `ChartControls.cs` (no NuGet package needed)
- Справка — Руководство пользователя, О программе
- Разное — Настройка, Сменить пароль

## Roles
| Table group | Администратор | Фармацевт | Гость |
|---|---|---|---|
| Medicine, ReadyMedicine, CompoundedMedicine, Manufacturer, PreparationTechnology, City, Street | CRUD | R | R |
| Patient, Order, OrderComposition, Prescription(+Composition), OrderPrescription, OrderSubstanceRequest, OrderComponentRequest | CRUD | CRUD | - |
| All other tables | CRUD | R | - |
| Документы menu (orders, reports) | yes | yes (SQL read-only) | hidden |
| Analytics | yes | yes | yes |
