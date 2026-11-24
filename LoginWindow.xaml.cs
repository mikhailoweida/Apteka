using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Apteka;

public partial class LoginWindow : Window
{
    readonly bool _ready;

    public LoginWindow()
    {
        InitializeComponent();
        ThemeManager.Style(this);

        var ver = typeof(LoginWindow).Assembly.GetName().Version?.ToString() ?? "1.0.0.0";
        Header.Line1 = Loc.T("АИС Аптека");
        Header.Line2 = Loc.T("Версия {0}", ver);
        Header.SetHint(Loc.T("Введите имя пользователя и пароль"));

        CmbLang.ItemsSource = Loc.Languages;
        CmbLang.SelectedItem = Loc.Current;
        _ready = true;                       // from now on a selection change is a real user action

        // Status line: keyboard layout and CapsLock
        InputLanguageManager.Current.InputLanguageChanged += InputLang_Changed;
        Closed += (_, _) => InputLanguageManager.Current.InputLanguageChanged -= InputLang_Changed;
        PreviewKeyUp += (_, _) => UpdateStatus();
        Activated += (_, _) => UpdateStatus();
        UpdateStatus();

        TxtLogin.Focus();
    }

    void InputLang_Changed(object sender, InputLanguageEventArgs e) => UpdateStatus();

    void UpdateStatus()
    {
        string name = "";
        try
        {
            var c = InputLanguageManager.Current.CurrentInputLanguage;
            if (!c.IsNeutralCulture && c.Parent != null && !Equals(c.Parent, CultureInfo.InvariantCulture)) c = c.Parent;
            name = c.DisplayName;
            if (name.Length > 0) name = char.ToUpper(name[0]) + name.Substring(1);
        }
        catch { }
        TxtLang.Text = Loc.T("Язык ввода: {0}", name);
        TxtCaps.Text = Keyboard.IsKeyToggled(Key.CapsLock) ? Loc.T("Клавиша CapsLock нажата") : "";
    }

    void Lang_Changed(object sender, SelectionChangedEventArgs e)
    {
        if (!_ready || CmbLang.SelectedItem is not Lang l || l.Code == Loc.Current.Code) return;
        Loc.Set(l.Code);
        AppSettings.Current.Language = l.Code;
        AppSettings.Save();
        new LoginWindow().Show();            // rebuild the window so every text is read again
        Close();
    }

    void Cancel_Click(object sender, RoutedEventArgs e) => Close();

    void Login_Click(object sender, RoutedEventArgs e)
    {
        string login = TxtLogin.Text.Trim();
        string pass = TxtPass.Password;
        if (login == "" || pass == "")
        {
            Header.SetHint(Loc.T("Введите логин и пароль"), true);
            return;
        }
        try
        {
            var dt = Db.Query(
                "SELECT u.id, u.login, r.role_name FROM AppUser u JOIN AppRole r ON r.id = u.role_id " +
                "WHERE u.login = @l AND u.password_hash = @h",
                ("@l", login), ("@h", Security.Hash(pass)));
            if (dt.Rows.Count == 0)
            {
                Header.SetHint(Loc.T("Неверный логин или пароль"), true);
                Logger.Write("Неудачная попытка входа: " + login);
                return;
            }
            Session.UserId = (int)dt.Rows[0]["id"];
            Session.Login = (string)dt.Rows[0]["login"];
            Session.Role = (string)dt.Rows[0]["role_name"];
            Logger.Write("Вход в систему");
            OpenMain();
        }
        catch (Exception ex)
        {
            var f = DbErrors.Describe(ex);
            Header.SetHint(f.Title + ".\n" + f.Message, true);
        }
    }

    void Guest_Click(object sender, RoutedEventArgs e)
    {
        Session.UserId = 0;
        Session.Login = "guest";
        Session.Role = "Гость";
        Logger.Write("Вход гостя");
        OpenMain();
    }

    void OpenMain()
    {
        new MainWindow().Show();
        Close();
    }
}
