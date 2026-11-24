using System.Windows;

namespace Apteka;

public partial class SettingsWindow : Window
{
    static readonly string[] Themes = { "Classic", "Light", "Dark", "Blue" };

    public SettingsWindow()
    {
        InitializeComponent();
        ThemeManager.Style(this);
        var s = AppSettings.Current;
        int idx = Array.IndexOf(Themes, s.Theme);
        CmbTheme.SelectedIndex = idx < 0 ? 0 : idx;
        SldFont.Value = s.FontSize;
        CmbLang.ItemsSource = Loc.Languages;
        CmbLang.SelectedItem = Loc.Current;
        TxtFont.Text = ((int)s.FontSize).ToString();
    }

    void Font_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (TxtFont != null && SldFont != null) TxtFont.Text = ((int)SldFont.Value).ToString();
    }

    void Save_Click(object sender, RoutedEventArgs e)
    {
        var s = AppSettings.Current;
        s.Theme = Themes[Math.Max(0, CmbTheme.SelectedIndex)];
        s.FontSize = SldFont.Value;
        if (CmbLang.SelectedItem is Lang l)
        {
            s.Language = l.Code;
            Loc.Set(l.Code);
        }
        AppSettings.Save();
        DialogResult = true;
    }
}
