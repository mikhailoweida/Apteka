using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Data.SqlClient;

namespace Apteka;

public partial class MainWindow : Window
{
    Entity _cur;
    DataTable _tbl;
    DataTable _rep;
    readonly DispatcherTimer _auto;

    // 0 = medicines per category (bar), 1 = average price (bar), 2 = orders by status (pie), 3 = orders by day (line)
    static readonly string[] ChartSql =
    {
        "SELECT category AS label, COUNT(*) AS val FROM v_AllMedicines GROUP BY category ORDER BY category",
        "SELECT category AS label, AVG(price) AS val FROM v_AllMedicines GROUP BY category ORDER BY category",
        "SELECT order_status AS label, COUNT(*) AS val FROM [Order] GROUP BY order_status ORDER BY order_status",
        "SELECT CONVERT(NVARCHAR(8), CAST(order_date AS date), 4) AS label, COUNT(*) AS val FROM [Order] " +
        "WHERE CAST(order_date AS date) BETWEEN @from AND @to " +
        "GROUP BY CAST(order_date AS date) ORDER BY CAST(order_date AS date)"
    };

    public MainWindow()
    {
        InitializeComponent();
        ThemeManager.Style(this);

        // Timer must exist before any chart event can fire.
        _auto = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
        _auto.Tick += (_, _) => RunChart();
        Closed += (_, _) => _auto.Stop();

        TxtStatus.Text = Loc.T("Пользователь: {0}   |   Роль: {1}", Session.Login, Loc.T(Session.Role));

        bool guest = Session.Role == "Гость";
        MenuPass.Visibility = guest ? Visibility.Collapsed : Visibility.Visible;
        MenuDocs.Visibility = guest ? Visibility.Collapsed : Visibility.Visible;
        MenuOrder.Visibility = Security.Perm("Order").Contains('C') ? Visibility.Visible : Visibility.Collapsed;

        BuildMenus();

        CmbReport.ItemsSource = Report.All;

        var first = Entity.All.FirstOrDefault(en => Security.Perm(en.Key).Contains('R'));
        if (first != null) ShowTablesPage(first);
    }

    // ---------- Menus built from the role's permissions ----------
    void BuildMenus()
    {
        MenuRefs.Items.Clear();
        MenuDocLists.Items.Clear();

        var allowed = Entity.All.Where(en => Security.Perm(en.Key).Contains('R')).ToList();

        MenuItem Make(Entity en)
        {
            var mi = new MenuItem { Header = en.Title, Tag = en };
            mi.Click += (s, _) => ShowTablesPage((Entity)((MenuItem)s).Tag);
            return mi;
        }

        foreach (var en in allowed.Where(x => x.Group == Entity.GRefs))
            MenuRefs.Items.Add(Make(en));

        var links = allowed.Where(x => x.Group == Entity.GLinks).ToList();
        if (links.Count > 0)
        {
            MenuRefs.Items.Add(new Separator());
            var sub = new MenuItem { Header = Loc.T("Связующие таблицы (M:N)") };
            foreach (var en in links) sub.Items.Add(Make(en));
            MenuRefs.Items.Add(sub);
        }

        var docs = allowed.Where(x => x.Group == Entity.GDocs).ToList();
        foreach (var en in docs) MenuDocLists.Items.Add(Make(en));
        MenuDocLists.Visibility = docs.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
    }

    void Grid_AutoGen(object sender, DataGridAutoGeneratingColumnEventArgs e) => Util.FixColumn(e);

    // ---------- Page switching ----------
    void HidePages()
    {
        PageTables.Visibility = Visibility.Collapsed;
        PageOrder.Visibility = Visibility.Collapsed;
        PageReports.Visibility = Visibility.Collapsed;
        PageAnalytics.Visibility = Visibility.Collapsed;
    }

    void ShowTablesPage(Entity e)
    {
        HidePages();
        PageTables.Visibility = Visibility.Visible;
        _cur = e;
        TxtPageTitle.Text = e.Title;
        TxtSearch.Text = "";
        LoadTable();
    }

    void ShowOrderPage(object s, RoutedEventArgs e)
    {
        HidePages();
        PageOrder.Visibility = Visibility.Visible;
        try
        {
            CmbPatient.ItemsSource = Db.Query(
                "SELECT id, last_name + N' ' + first_name AS txt FROM Patient ORDER BY last_name").DefaultView;
            CmbMedicine.ItemsSource = Db.Query(
                "SELECT id, name AS txt FROM Medicine ORDER BY name").DefaultView;
        }
        catch (Exception ex) { Util.ShowError(ex); }
    }

    void ShowReportsPage(object s, RoutedEventArgs e)
    {
        HidePages();
        PageReports.Visibility = Visibility.Visible;
        TxtSql.IsReadOnly = Session.Role != "Администратор";
    }

    void ShowAnalyticsPage(object s, RoutedEventArgs e)
    {
        HidePages();
        PageAnalytics.Visibility = Visibility.Visible;
        if (DtpFrom.SelectedDate == null)
        {
            // Start from the first order (at most one month back), so the line graph is not empty on older data.
            DateTime from = DateTime.Today.AddMonths(-1);
            try
            {
                if (Db.Scalar("SELECT MIN(CAST(order_date AS date)) FROM [Order]") is DateTime first && first < from)
                    from = first;
            }
            catch { }
            DtpFrom.SelectedDate = from;
        }
        DtpTo.SelectedDate ??= DateTime.Today;
        RunChart();
    }

    void ShowUserGuide_Click(object s, RoutedEventArgs e) =>
        new HelpWindow("guide") { Owner = this }.ShowDialog();

    void ShowAbout_Click(object s, RoutedEventArgs e) =>
        new HelpWindow("about") { Owner = this }.ShowDialog();

    void ShowSettings_Click(object s, RoutedEventArgs e)
    {
        string oldLang = AppSettings.Current.Language;
        var w = new SettingsWindow { Owner = this };
        if (w.ShowDialog() != true) return;

        ThemeManager.Apply(AppSettings.Current);   // restyles every open window
        if (AppSettings.Current.Language != oldLang)
        {
            // All texts are read when a window is built, so rebuild the main window in the new language.
            _auto.Stop();
            new MainWindow().Show();
            Close();
        }
    }

    void ShowChangePass_Click(object s, RoutedEventArgs e)
    {
        if (Session.Role == "Гость") return;
        new ChangePasswordWindow { Owner = this }.ShowDialog();
    }

    // ---------- Logout ----------
    void Logout_Click(object sender, RoutedEventArgs e)
    {
        Logger.Write("Выход из системы");
        _auto.Stop();
        new LoginWindow().Show();
        Close();
    }

    // ---------- Table CRUD ----------
    void LoadTable()
    {
        try
        {
            _tbl = Db.Query(_cur.SelectSql() + " ORDER BY t.id");
            GridTable.ItemsSource = _tbl.DefaultView;
            string p = Security.Perm(_cur.Key);
            BtnAdd.IsEnabled = p.Contains('C');
            BtnEdit.IsEnabled = p.Contains('U');
            BtnDelete.IsEnabled = p.Contains('D');
            ApplyFilter();
        }
        catch (Exception ex) { Util.ShowError(ex); }
    }

    void Search_Changed(object sender, TextChangedEventArgs e) => ApplyFilter();

    void ApplyFilter()
    {
        if (_tbl == null) return;
        string t = TxtSearch.Text.Trim();
        if (t == "")
            _tbl.DefaultView.RowFilter = "";
        else
        {
            string s = t.Replace("[", "[[]").Replace("'", "''").Replace("%", "[%]").Replace("*", "[*]");
            var parts = _tbl.Columns.Cast<DataColumn>()
                .Select(c => $"Convert([{c.ColumnName}], 'System.String') LIKE '%{s}%'");
            _tbl.DefaultView.RowFilter = string.Join(" OR ", parts);
        }
        TxtTableInfo.Text = Loc.T("Записей: {0}", _tbl.DefaultView.Count);
    }

    int? SelectedId() =>
        GridTable.SelectedItem is DataRowView r ? (int)r["ID"] : null;

    void OpenEdit(int? id)
    {
        try
        {
            var w = new EditWindow(_cur, id) { Owner = this };
            if (w.ShowDialog() == true) LoadTable();
        }
        catch (Exception ex) { Util.ShowError(ex); }
    }

    void Add_Click(object s, RoutedEventArgs e) => OpenEdit(null);

    void Edit_Click(object s, RoutedEventArgs e)
    {
        var id = SelectedId();
        if (id == null)
        {
            Util.Msg(Loc.T("Выберите запись"), Loc.T("Изменение"), MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        OpenEdit(id);
    }

    void Grid_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var id = SelectedId();
        if (BtnEdit.IsEnabled && id != null) OpenEdit(id);
    }

    void Delete_Click(object s, RoutedEventArgs e)
    {
        var id = SelectedId();
        if (id == null)
        {
            Util.Msg(Loc.T("Выберите запись"), Loc.T("Удаление"), MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        if (Util.Msg(Loc.T("Удалить запись №{0} из «{1}»?", id, _cur.Title),
                Loc.T("Подтверждение"), MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;
        try
        {
            Db.Exec($"DELETE FROM {_cur.Table} WHERE id = @id", ("@id", id.Value));
            Logger.Write($"Удаление из {_cur.Title} №{id}");
            LoadTable();
        }
        catch (Exception ex) { Util.ShowError(ex); }   // a 547 here becomes "record is used in table X"
    }

    void ExportTable_Click(object s, RoutedEventArgs e)
    {
        if (_tbl == null || _cur == null)
        {
            Util.Msg(Loc.T("Сначала откройте таблицу из меню «Справочники» или «Документы»."),
                Loc.T("Экспорт"), MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        Util.ExportCsv(_tbl.DefaultView.ToTable(), _cur.Key);
    }

    // ---------- Order creation ----------
    void OrderWarning(string text, string title = null)
    {
        Util.Msg(text, title ?? Loc.T("Заказ"), MessageBoxButton.OK, MessageBoxImage.Warning);
        TxtQty.Focus();
        TxtQty.SelectAll();
    }

    // Stock of a ready-made medicine; null when the medicine has no stock (compounded to order).
    static long? StockOf(object medicineId)
    {
        var v = Db.Scalar(
            "SELECT SUM(CAST(stock_quantity AS BIGINT)) FROM ReadyMedicine WHERE medicine_id = @m",
            ("@m", medicineId));
        return v == null || v is DBNull ? null : Convert.ToInt64(v);
    }

    void ShowNoStock(string medicine, long have, long wanted)
    {
        string text = have <= 0
            ? Loc.T("Лекарства «{0}» сейчас нет на складе (остаток: 0).\n\nЧто делать: выберите другое лекарство или сначала оформите поставку этого лекарства.", medicine)
            : Loc.T("Недостаточно лекарства «{0}» на складе: вы запросили {1}, а в наличии только {2}.\n\nЧто делать: уменьшите количество до {2} или меньше, либо сначала оформите поставку этого лекарства.",
                    medicine, wanted, have);
        OrderWarning(text, Loc.T("Недостаточно лекарства на складе"));
    }

    void CreateOrder_Click(object s, RoutedEventArgs e)
    {
        if (CmbPatient.SelectedValue == null || CmbMedicine.SelectedValue == null)
        {
            OrderWarning(Loc.T("Выберите пациента и лекарство из списков."));
            return;
        }
        if (!long.TryParse(TxtQty.Text.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out long qty))
        {
            OrderWarning(Loc.T("Введите количество — целое число больше нуля (например, 5)."));
            return;
        }
        if (qty <= 0)
        {
            OrderWarning(Loc.T("Количество должно быть больше нуля."));
            return;
        }

        string medicine = ((DataRowView)CmbMedicine.SelectedItem)["txt"].ToString();
        try
        {
            // 1. Check the stock ourselves, so the user hears "not enough in stock" instead of a database error.
            long? have = StockOf(CmbMedicine.SelectedValue);
            if (have != null && qty > have.Value)
            {
                ShowNoStock(medicine, have.Value, qty);
                return;
            }
            if (qty > int.MaxValue)
            {
                OrderWarning(Loc.T("Это число слишком велико. Введите количество не больше 2 147 483 647."));
                return;
            }

            // 2. One transaction: if anything fails, no half-created empty order is left behind.
            Db.Exec("SET XACT_ABORT ON; BEGIN TRAN; " +
                    "EXEC dbo.sp_CreateOrder @patient_id = @p, @medicine_id = @m, @quantity = @q; " +
                    "COMMIT;",
                ("@p", CmbPatient.SelectedValue), ("@m", CmbMedicine.SelectedValue), ("@q", (int)qty));
            Logger.Write("Оформлен заказ");
            Util.Msg(Loc.T("Заказ успешно создан!"), Loc.T("Заказ"),
                MessageBoxButton.OK, MessageBoxImage.Information);
            TxtQty.Text = "1";
        }
        catch (SqlException ex) when (ex.Number == 547 &&
                                      ex.Message.Contains("CK_ReadyMed_qty", StringComparison.OrdinalIgnoreCase))
        {
            // Safety net: another user took the stock between our check and the order.
            Logger.Write("Ошибка: " + ex.Message);
            long have = 0;
            try { have = StockOf(CmbMedicine.SelectedValue) ?? 0; } catch { }
            ShowNoStock(medicine, have, qty);
        }
        catch (Exception ex) { Util.ShowError(ex); }
    }

    // ---------- Reports ----------
    void Report_Changed(object s, SelectionChangedEventArgs e)
    {
        if (CmbReport.SelectedItem is Report r) TxtSql.Text = r.Sql;
    }

    void RunReport_Click(object s, RoutedEventArgs e)
    {
        string sql = TxtSql.Text.Trim();
        if (sql == "") { TxtReportInfo.Text = Loc.T("Введите запрос"); return; }
        bool isSelect = sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase)
                     || sql.StartsWith("WITH", StringComparison.OrdinalIgnoreCase);
        if (!isSelect || sql.TrimEnd(';', ' ', '\r', '\n').Contains(';'))
        {
            Util.Msg(Loc.T("Разрешён один запрос SELECT."), Loc.T("Запрос отклонён"),
                MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        try
        {
            _rep = Db.Query(sql);
            GridReport.ItemsSource = _rep.DefaultView;
            TxtReportInfo.Text = _rep.Rows.Count == 0 ? Loc.T("Нет данных") : Loc.T("Строк: {0}", _rep.Rows.Count);
            Logger.Write("Выполнен запрос в разделе «Документы»");
        }
        catch (Exception ex)
        {
            GridReport.ItemsSource = null;
            TxtReportInfo.Text = Loc.T("Ошибка запроса");
            Util.ShowError(ex);
        }
    }

    void ExportReport_Click(object s, RoutedEventArgs e) => Util.ExportCsv(_rep, "report");

    // ---------- Analytics (dashboard) ----------
    void Chart_Changed_Button(object s, RoutedEventArgs e) => RunChart();

    void ToggleAuto(object s, RoutedEventArgs e)
    {
        if (_auto.IsEnabled)
        {
            _auto.Stop();
            BtnAuto.Content = Loc.T("Автообновление: ВЫКЛ");
        }
        else
        {
            _auto.Start();
            BtnAuto.Content = Loc.T("Автообновление: ВКЛ");
            RunChart();
        }
    }

    static List<ChartPoint> Points(DataTable dt) =>
        dt.Rows.Cast<DataRow>().Select(r => new ChartPoint(
            r["label"] is DBNull ? "" : Convert.ToString(r["label"]),
            r["val"] is DBNull ? 0 : Convert.ToDouble(r["val"]))).ToList();

    void RunChart()
    {
        // Do nothing unless the page is on screen (the timer may fire on another page).
        if (PageAnalytics.Visibility != Visibility.Visible) return;
        try
        {
            var from = (DtpFrom.SelectedDate ?? DateTime.Today.AddMonths(-1)).Date;
            var to = (DtpTo.SelectedDate ?? DateTime.Today).Date;
            if (from > to) (from, to) = (to, from);

            ChartCategories.SetData(Points(Db.Query(ChartSql[0])));
            ChartPrice.SetData(Points(Db.Query(ChartSql[1])));
            ChartStatus.SetData(Points(Db.Query(ChartSql[2])));
            ChartDays.SetData(Points(Db.Query(ChartSql[3], ("@from", from), ("@to", to))));

            TxtChartInfo.Text = _auto.IsEnabled
                ? Loc.T("Данные обновлены: {0:HH:mm:ss} (авто)", DateTime.Now)
                : Loc.T("Данные обновлены: {0:HH:mm:ss}", DateTime.Now);
        }
        catch (Exception ex)
        {
            _auto.Stop();   // do not spam an error box every 5 seconds
            BtnAuto.Content = Loc.T("Автообновление");
            Util.ShowError(ex);
        }
    }

    void ExportChart_Click(object s, RoutedEventArgs e)
    {
        if (ChartCategories.Data.Count + ChartPrice.Data.Count + ChartStatus.Data.Count + ChartDays.Data.Count == 0)
        {
            Util.Msg(Loc.T("Нет данных для экспорта"), Loc.T("Экспорт"),
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var dlg = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "PNG (*.png)|*.png",
            FileName = "dashboard.png"
        };
        if (dlg.ShowDialog() != true) return;
        try
        {
            DashGrid.UpdateLayout();
            int w = (int)Math.Max(DashGrid.ActualWidth, 500);
            int h = (int)Math.Max(DashGrid.ActualHeight, 300);
            var rtb = new RenderTargetBitmap(w, h, 96, 96, PixelFormats.Pbgra32);

            // Theme background first (so light text stays readable), then the whole dashboard on top of it.
            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                dc.DrawRectangle((Brush)Application.Current.Resources["AppBackground"], null, new Rect(0, 0, w, h));
                dc.DrawRectangle(new VisualBrush(DashGrid), null, new Rect(0, 0, w, h));
            }
            rtb.Render(dv);

            var enc = new PngBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(rtb));
            using var fs = System.IO.File.Create(dlg.FileName);
            enc.Save(fs);
            Logger.Write("Экспорт дашборда в PNG");
        }
        catch (Exception ex) { Util.ShowError(ex); }
    }
}
