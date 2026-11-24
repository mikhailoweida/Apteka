using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Apteka;

/// <summary>Change-password form in the same style as the sign-in form (teacher's sample, Figure 6).</summary>
public class ChangePasswordWindow : Window
{
    readonly PasswordBox _old = new(), _new1 = new(), _new2 = new();
    readonly KeyHeader _header = new();

    public ChangePasswordWindow()
    {
        Title = Loc.T("Смена пароля");
        Width = 480;
        SizeToContent = SizeToContent.Height;
        ResizeMode = ResizeMode.NoResize;
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        ThemeManager.Style(this);

        _header.Line1 = Loc.T("АИС Аптека");
        _header.Line2 = Loc.T("Пользователь: {0}", Session.Login);
        _header.SetHint(Loc.T("Введите прежний и новый пароль"));

        var fields = new Grid { Margin = new Thickness(8, 22, 8, 0) };
        fields.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
        fields.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        void Row(string label, PasswordBox pb, bool last)
        {
            int r = fields.RowDefinitions.Count;
            fields.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            var tb = new TextBlock
            {
                Text = label, VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 8, last ? 0 : 14)
            };
            pb.Padding = new Thickness(2, 3, 2, 3);
            pb.Margin = new Thickness(0, 0, 0, last ? 0 : 14);
            Grid.SetRow(tb, r);
            Grid.SetRow(pb, r);
            Grid.SetColumn(pb, 1);
            fields.Children.Add(tb);
            fields.Children.Add(pb);
        }
        Row(Loc.T("Прежний пароль"), _old, false);
        Row(Loc.T("Новый пароль"), _new1, false);
        Row(Loc.T("Подтвердите"), _new2, true);

        var ok = new Button
        {
            Content = Loc.T("Сменить пароль"), IsDefault = true, HorizontalAlignment = HorizontalAlignment.Left,
            Padding = new Thickness(14, 4, 14, 4), MinWidth = 104
        };
        ok.Click += Save;
        var cancel = new Button
        {
            Content = Loc.T("Отмена"), IsCancel = true, HorizontalAlignment = HorizontalAlignment.Right,
            Padding = new Thickness(14, 4, 14, 4), MinWidth = 104
        };
        var buttons = new Grid { Margin = new Thickness(8, 22, 8, 16) };
        buttons.Children.Add(ok);
        buttons.Children.Add(cancel);

        var root = new StackPanel { Background = new SolidColorBrush(Color.FromRgb(0xB9, 0xD1, 0xEA)) };
        TextElement.SetForeground(root, Brushes.Black);
        root.Children.Add(_header);
        root.Children.Add(fields);
        root.Children.Add(buttons);
        Content = root;

        Loaded += (_, _) => _old.Focus();
    }

    void Save(object sender, RoutedEventArgs e)
    {
        string o = _old.Password, n1 = _new1.Password, n2 = _new2.Password;
        if (o == "" || n1 == "" || n2 == "") { _header.SetHint(Loc.T("Заполните все поля"), true); return; }
        if (n1.Length < 4) { _header.SetHint(Loc.T("Новый пароль ≥ 4 символов"), true); return; }
        if (n1 != n2) { _header.SetHint(Loc.T("Новые пароли не совпадают"), true); return; }
        if (n1 == o) { _header.SetHint(Loc.T("Новый пароль совпадает со старым"), true); return; }
        try
        {
            int ok = Convert.ToInt32(Db.Scalar(
                "SELECT COUNT(*) FROM AppUser WHERE id = @i AND password_hash = @h",
                ("@i", Session.UserId), ("@h", Security.Hash(o))));
            if (ok == 0) { _header.SetHint(Loc.T("Старый пароль неверен"), true); return; }
            Db.Exec("UPDATE AppUser SET password_hash = @h WHERE id = @i",
                    ("@h", Security.Hash(n1)), ("@i", Session.UserId));
            Logger.Write("Смена пароля");
            Util.Msg(Loc.T("Пароль изменён"), Loc.T("Готово"), MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
        }
        catch (Exception ex) { Util.ShowError(ex); }
    }
}
