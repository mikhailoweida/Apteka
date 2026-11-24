using System.Text;

namespace Apteka;

public enum FT { Text, Int, Decimal, Date, Time, Lookup, Option }

public class Field
{
    public string Col, Default, LookupTable, LookupText;
    string _header;
    /// <summary>Column caption; stored in Russian, returned in the current UI language.</summary>
    public string Header { get => Loc.T(_header); set => _header = value; }
    public FT Type = FT.Text;
    public bool Required = true, ReadOnly;
    public string[] Options;
}

public static class F
{
    public static Field T(string c, string h, bool req = true, string def = null) =>
        new() { Col = c, Header = h, Required = req, Default = def, Type = FT.Text };
    public static Field I(string c, string h, bool req = true, string def = null) =>
        new() { Col = c, Header = h, Required = req, Default = def, Type = FT.Int };
    public static Field D(string c, string h, bool req = true, string def = null) =>
        new() { Col = c, Header = h, Required = req, Default = def, Type = FT.Decimal };
    public static Field Dt(string c, string h, bool req = true, string def = null) =>
        new() { Col = c, Header = h, Required = req, Default = def, Type = FT.Date };
    public static Field Tm(string c, string h, bool req = true) =>
        new() { Col = c, Header = h, Required = req, Type = FT.Time };
    public static Field L(string c, string h, string table, string text) =>
        new() { Col = c, Header = h, Type = FT.Lookup, LookupTable = table, LookupText = text };
    public static Field O(string c, string h, string[] opts, string def = null) =>
        new() { Col = c, Header = h, Type = FT.Option, Options = opts, Default = def };
    public static Field Ro(string c, string h) =>
        new() { Col = c, Header = h, ReadOnly = true, Required = false };
}

public class Entity
{
    public const string GRefs  = "Справочники";
    public const string GDocs  = "Документы";
    public const string GLinks = "Связи (M:N)";

    public string Key { get; set; }
    string _title;
    /// <summary>Table caption; stored in Russian, returned in the current UI language.</summary>
    public string Title { get => Loc.T(_title); set => _title = value; }
    public string Table { get; set; }
    public string Group { get; set; } = GRefs;   // decides which menu the table appears in
    public Field[] Fields { get; set; }

    public Entity(string key, string title, string table, params Field[] fields)
    {
        Key = key; Title = title; Table = table; Fields = fields;
    }

    public IEnumerable<Field> Editable => Fields.Where(f => !f.ReadOnly);

    public string SelectSql()
    {
        var sb = new StringBuilder("SELECT t.id AS [ID]");
        foreach (var f in Fields)
        {
            if (f.Type == FT.Lookup)
                sb.Append($", (SELECT {f.LookupText} FROM {f.LookupTable} x WHERE x.id = t.[{f.Col}]) AS [{f.Header}]");
            else
                sb.Append($", t.[{f.Col}] AS [{f.Header}]");
        }
        sb.Append($" FROM {Table} t");
        return sb.ToString();
    }

    const string PERSON = "x.last_name + N' ' + x.first_name";
    // Language-neutral on purpose (was "№5 от 12.03.2025"): these strings are baked into SQL once at startup.
    const string ORDER_TXT = "N'№' + CAST(x.id AS NVARCHAR(10)) + N' (' + CONVERT(NVARCHAR(10), x.order_date, 104) + N')'";
    const string RX_TXT = "N'RX-' + CAST(x.id AS NVARCHAR(10)) + N' ' + x.prescription_number";

    public static readonly List<Entity> All = new()
    {
        // ---------- Core dictionary data ----------
        new("City", "Города", "City", F.T("city_name", "Город")),
        new("Street", "Улицы", "Street", F.T("street_name", "Улица")),
        new("Position", "Должности", "Position", F.T("position_name", "Должность")),
        new("Diagnosis", "Диагнозы", "Diagnosis",
            F.T("name", "Название"),
            F.L("doctor_id", "Врач", "Doctor", PERSON)),

        new("PreparationTechnology", "Технологии приготовления", "PreparationTechnology",
            F.T("technology_name", "Технология"),
            F.T("preparation_method", "Способ приготовления", false)),

        new("Substance", "Вещества", "Substance",
            F.T("substance_name", "Вещество"),
            F.T("description", "Описание", false),
            F.I("stock_quantity", "Остаток", true, "0")),

        new("Component", "Компоненты", "Component",
            F.T("name", "Компонент"),
            F.I("quantity", "Количество", true, "0")),

        // ---------- Medicines (subtypes) ----------
        new("Medicine", "Лекарства", "Medicine",
            F.T("name", "Название"),
            F.I("critical_norm", "Критическая норма", true, "0")),

        new("ReadyMedicine", "Готовые лекарства", "ReadyMedicine",
            F.L("medicine_id", "Лекарство", "Medicine", "x.name"),
            F.O("type", "Тип", new[] { "tablet", "ointment", "tincture" }),
            F.T("dosage", "Дозировка", false),
            F.O("release_form", "Форма выпуска", new[] { "blister", "tube", "bottle", "pack" }),
            F.T("shelf_life", "Срок текстом", false),
            F.Dt("manufacture_date", "Дата производства", true, "today"),
            F.I("shelf_life_days", "Дней годности", true, "365"),
            F.Ro("expiry_date", "Годен до"),
            F.I("stock_quantity", "Остаток", true, "0"),
            F.D("price", "Цена")),

        new("CompoundedMedicine", "Изготовляемые лекарства", "CompoundedMedicine",
            F.L("medicine_id", "Лекарство", "Medicine", "x.name"),
            F.L("technology_id", "Технология", "PreparationTechnology", "x.technology_name"),
            F.O("type", "Тип", new[] { "mixture", "ointment", "solution", "tincture", "powder" }),
            F.T("usage_method", "Способ применения", false),
            F.Tm("preparation_time", "Время изготовления", false),
            F.D("price", "Цена")),

        // ---------- People ----------
        new("Patient", "Пациенты", "Patient",
            F.T("last_name", "Фамилия"),
            F.T("first_name", "Имя"),
            F.T("middle_name", "Отчество", false),
            F.I("age", "Возраст", false),
            F.T("house_number", "Дом", false),
            F.L("city_id", "Город", "City", "x.city_name"),
            F.L("street_id", "Улица", "Street", "x.street_name")),

        new("Doctor", "Врачи", "Doctor",
            F.T("last_name", "Фамилия"),
            F.T("first_name", "Имя"),
            F.T("middle_name", "Отчество", false),
            F.T("house_number", "Дом", false),
            F.L("position_id", "Должность", "Position", "x.position_name"),
            F.L("city_id", "Город", "City", "x.city_name"),
            F.L("street_id", "Улица", "Street", "x.street_name")),

        new("Manufacturer", "Производители", "Manufacturer",
            F.T("name", "Название"),
            F.T("house_number", "Дом"),
            F.L("city_id", "Город", "City", "x.city_name"),
            F.L("street_id", "Улица", "Street", "x.street_name")),

        // ---------- Documents (orders / prescriptions / requests / supply) ----------
        new("Order", "Заказы", "[Order]",
            F.Dt("order_date", "Дата заказа", true, "today"),
            F.T("contact_phone", "Телефон", false),
            F.L("patient_id", "Пациент", "Patient", PERSON),
            F.O("order_status", "Статус",
                new[] { "new", "processing", "in_production", "waiting_components", "ready", "completed" }, "new"))
            { Group = GDocs },

        new("OrderComposition", "Состав заказов", "OrderComposition",
            F.L("order_id", "Заказ", "[Order]", ORDER_TXT),
            F.L("medicine_id", "Лекарство", "Medicine", "x.name"),
            F.I("quantity", "Количество", true, "1")) { Group = GDocs },

        new("Prescription", "Рецепты", "Prescription",
            F.T("prescription_number", "Номер рецепта"),
            F.Dt("issue_date", "Дата выписки", true, "today"),
            F.L("doctor_id", "Врач", "Doctor", PERSON),
            F.L("patient_id", "Пациент", "Patient", PERSON)) { Group = GDocs },

        new("PrescriptionComposition", "Состав рецепта", "PrescriptionComposition",
            F.L("prescription_id", "Рецепт", "Prescription", RX_TXT),
            F.L("medicine_id", "Лекарство", "Medicine", "x.name")) { Group = GDocs },

        new("SubstanceRequest", "Заявки на вещества", "SubstanceRequest",
            F.Dt("request_date", "Дата заявки", true, "today"),
            F.I("quantity", "Количество", true, "1"),
            F.L("substance_id", "Вещество", "Substance", "x.substance_name")) { Group = GDocs },

        new("ComponentRequest", "Заявки на компоненты", "ComponentRequest",
            F.Dt("request_date", "Дата заявки", true, "today"),
            F.I("quantity", "Количество", true, "1"),
            F.L("component_id", "Компонент", "Component", "x.name")) { Group = GDocs },

        new("Supply", "Поставки", "Supply",
            F.L("manufacturer_id", "Производитель", "Manufacturer", "x.name"),
            F.L("medicine_id", "Лекарство", "Medicine", "x.name"),
            F.Dt("supply_date", "Дата поставки", true, "today"),
            F.I("quantity", "Количество", true, "1")) { Group = GDocs },

        // ---------- M:N junction tables ----------
        new("MedicineComposition", "Состав готового (M:N)", "MedicineComposition",
            F.L("medicine_id", "Лекарство", "Medicine", "x.name"),
            F.L("substance_id", "Вещество", "Substance", "x.substance_name")) { Group = GLinks },

        new("CompoundedMedicineComposition", "Состав изготовляемого (M:N)", "CompoundedMedicineComposition",
            F.L("compounded_medicine_id", "Изготовляемое", "CompoundedMedicine",
                "(SELECT name FROM Medicine WHERE id = x.medicine_id)"),
            F.L("component_id", "Компонент", "Component", "x.name")) { Group = GLinks },

        new("PrescriptionDiagnosis", "Диагнозы рецепта (M:N)", "PrescriptionDiagnosis",
            F.L("prescription_id", "Рецепт", "Prescription", RX_TXT),
            F.L("diagnosis_id", "Диагноз", "Diagnosis", "x.name")) { Group = GLinks },

        new("PatientDiagnosis", "Диагнозы пациента (M:N)", "PatientDiagnosis",
            F.L("patient_id", "Пациент", "Patient", PERSON),
            F.L("diagnosis_id", "Диагноз", "Diagnosis", "x.name")) { Group = GLinks },

        new("OrderPrescription", "Рецепты заказа (M:N)", "OrderPrescription",
            F.L("order_id", "Заказ", "[Order]", ORDER_TXT),
            F.L("prescription_id", "Рецепт", "Prescription", RX_TXT)) { Group = GLinks },

        new("OrderSubstanceRequest", "Заявки в-в по заказу (M:N)", "OrderSubstanceRequest",
            F.L("order_id", "Заказ", "[Order]", ORDER_TXT),
            F.L("substance_request_id", "Заявка", "SubstanceRequest",
                "N'№' + CAST(x.id AS NVARCHAR(10))")) { Group = GLinks },

        new("OrderComponentRequest", "Заявки комп. по заказу (M:N)", "OrderComponentRequest",
            F.L("order_id", "Заказ", "[Order]", ORDER_TXT),
            F.L("component_request_id", "Заявка", "ComponentRequest",
                "N'№' + CAST(x.id AS NVARCHAR(10))")) { Group = GLinks },
    };
}
