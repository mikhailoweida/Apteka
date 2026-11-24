using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Apteka;

public class InputError : Exception
{
    public InputError(string msg) : base(msg) { }
}

public class EditWindow : Window
{
    readonly Entity _e;
    readonly int? _id;
    readonly Dictionary<Field, FrameworkElement> _ctl = new();

    public EditWindow(Entity e, int? id)
    {
        _e = e;
        _id = id;
        Title = Loc.T(id == null ? "Добавление: {0}" : "Изменение: {0}", e.Title);
        Width = 480;
        SizeToContent = SizeToContent.Height;
        ResizeMode = ResizeMode.NoResize;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        ThemeManager.Style(this);

        var grid = new Grid { Margin = new Thickness(16) };
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

        int r = 0;
        foreach (var f in e.Editable)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            var lbl = new TextBlock
            {
                Text = f.Header + (f.Required ? " *" : ""),
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 4, 8, 4)
            };
            var c = Make(f);
            c.Margin = new Thickness(0, 4, 0, 4);
            Grid.SetRow(lbl, r);
            Grid.SetRow(c, r);
            Grid.SetColumn(c, 1);
            grid.Children.Add(lbl);
            grid.Children.Add(c);
            _ctl[f] = c;
            r++;
        }

        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var ok = new Button { Content = Loc.T("Сохранить"), Width = 110, Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
        var cancel = new Button { Content = Loc.T("Отмена"), Width = 100, IsCancel = true };
        ok.Click += Save;
        var panel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Margin = new Thickness(0, 16, 0, 0)
        };
        panel.Children.Add(ok);
        panel.Children.Add(cancel);
        Grid.SetRow(panel, r);
        Grid.SetColumnSpan(panel, 2);
        grid.Children.Add(panel);
        Content = grid;

        if (id != null) LoadRow();
    }

    FrameworkElement Make(Field f)
    {
        switch (f.Type)
        {
            case FT.Date:
                return new DatePicker
                {
                    SelectedDate = (_id == null && f.Default == "today") ? DateTime.Today : (DateTime?)null
                };
            case FT.Lookup:
                var dt = Db.Query($"SELECT x.id AS id, {f.LookupText} AS txt FROM {f.LookupTable} x ORDER BY 2");
                return new ComboBox
                {
                    ItemsSource = dt.DefaultView,
                    SelectedValuePath = "id",
                    DisplayMemberPath = "txt"
                };
            case FT.Option:
                return new ComboBox
                {
                    ItemsSource = f.Options,
                    SelectedItem = _id == null ? f.Default : null
                };
            default:
                return new TextBox { Text = _id == null ? (f.Default ?? "") : "" };
        }
    }

    void LoadRow()
    {
        var dt = Db.Query($"SELECT * FROM {_e.Table} WHERE id = @id", ("@id", _id.Value));
        if (dt.Rows.Count == 0) throw new InputError(Loc.T("Запись не найдена"));
        var row = dt.Rows[0];
        foreach (var f in _e.Editable)
        {
            var v = row[f.Col];
            if (v is DBNull) continue;
            var c = _ctl[f];
            switch (f.Type)
            {
                case FT.Date: ((DatePicker)c).SelectedDate = (DateTime)v; break;
                case FT.Lookup: ((ComboBox)c).SelectedValue = v; break;
                case FT.Option: ((ComboBox)c).SelectedItem = v; break;
                case FT.Time: ((TextBox)c).Text = ((TimeSpan)v).ToString(@"hh\:mm\:ss"); break;
                case FT.Decimal: ((TextBox)c).Text = Convert.ToDouble(v).ToString(CultureInfo.InvariantCulture); break;
                default: ((TextBox)c).Text = v.ToString(); break;
            }
        }
    }

    static object Req(Field f)
    {
        if (f.Required) throw new InputError(Loc.T("Заполните поле «{0}»", f.Header));
        return null;
    }

    static object Read(Field f, FrameworkElement c)
    {
        switch (f.Type)
        {
            case FT.Date:
                {
                    var d = ((DatePicker)c).SelectedDate;
                    return d == null ? Req(f) : d.Value.Date;
                }
            case FT.Lookup:
                {
                    var v = ((ComboBox)c).SelectedValue;
                    return v == null ? Req(f) : v;
                }
            case FT.Option:
                {
                    var v = ((ComboBox)c).SelectedItem;
                    return v == null ? Req(f) : v;
                }
        }

        string t = ((TextBox)c).Text.Trim();
        if (t == "") return Req(f);
        switch (f.Type)
        {
            case FT.Int:
                // Parse as long first so "too big" can be told apart from "not a number".
                if (!long.TryParse(t, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out long big))
                    throw new InputError(Loc.T("Поле «{0}» должно быть целым числом", f.Header));
                if (big > int.MaxValue || big < int.MinValue)
                    throw new InputError(Loc.T("Поле «{0}»: число слишком большое (максимум 2 147 483 647)", f.Header));
                return (int)big;
            case FT.Decimal:
                if (!double.TryParse(t.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out double x))
                    throw new InputError(Loc.T("Поле «{0}» должно быть числом", f.Header));
                return x;
            case FT.Time:
                if (!TimeSpan.TryParse(t, CultureInfo.InvariantCulture, out TimeSpan ts))
                    throw new InputError(Loc.T("Поле «{0}»: время в формате ЧЧ:ММ", f.Header));
                return ts;
            default:
                return t;
        }
    }

    void Save(object sender, RoutedEventArgs e)
    {
        try
        {
            var ps = new List<(string Name, object Value)>();
            var cols = new List<string>();
            var names = new List<string>();
            int n = 0;
            foreach (var f in _e.Editable)
            {
                string p = "@p" + n++;
                ps.Add((p, Read(f, _ctl[f])));
                cols.Add($"[{f.Col}]");
                names.Add(p);
            }

            string sql;
            if (_id == null)
                sql = $"INSERT INTO {_e.Table} ({string.Join(", ", cols)}) VALUES ({string.Join(", ", names)})";
            else
            {
                var sets = cols.Select((c, i) => $"{c} = {names[i]}");
                sql = $"UPDATE {_e.Table} SET {string.Join(", ", sets)} WHERE id = @id";
                ps.Add(("@id", _id.Value));
            }

            Db.Exec(sql, ps.ToArray());
            Logger.Write((_id == null ? "Добавление в " : "Изменение в ") + _e.Title);
            DialogResult = true;
        }
        catch (Exception ex) { Util.ShowError(ex); }   // InputError is shown as a "check your input" warning
    }
}
