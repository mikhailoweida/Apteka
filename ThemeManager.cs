using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Apteka;

public static class ThemeManager
{
    // WPF ships the Windows 9x/2000 "Classic" control templates (raised/sunken 3D bevels)
    // in PresentationFramework.Classic.dll. Merging this dictionary gives the old 3D look.
    const string ClassicUri =
        "/PresentationFramework.Classic, Version=4.0.0.0, Culture=neutral, " +
        "PublicKeyToken=31bf3856ad364e35, ProcessorArchitecture=MSIL;component/themes/classic.xaml";

    static ResourceDictionary _classic;
    static ResourceDictionary _inputFix;

    public static bool IsClassic => AppSettings.Current.Theme == "Classic";

    // For the non-classic themes: keep text inside inputs readable even when the
    // window Foreground is light (Dark theme), because inputs keep a white background.
    static ResourceDictionary BuildInputFix()
    {
        var d = new ResourceDictionary();
        foreach (var t in new[] { typeof(TextBox), typeof(PasswordBox), typeof(ComboBox),
                                  typeof(DatePicker), typeof(DataGrid), typeof(ListBox) })
        {
            var st = new Style(t);
            st.Setters.Add(new Setter(Control.ForegroundProperty, SystemColors.ControlTextBrush));
            d.Add(t, st);
        }
        return d;
    }

    static void SetControlStyle(bool classic)
    {
        var merged = Application.Current.Resources.MergedDictionaries;
        _inputFix ??= BuildInputFix();

        if (_classic != null) merged.Remove(_classic);
        merged.Remove(_inputFix);

        if (classic)
        {
            try
            {
                _classic ??= new ResourceDictionary { Source = new Uri(ClassicUri, UriKind.Relative) };
                merged.Add(_classic);
                return;
            }
            catch
            {
                _classic = null; // classic assembly not available -> fall back to modern controls
            }
        }
        merged.Add(_inputFix);
    }

    public static void Apply(AppSettingsData s)
    {
        SetControlStyle(s.Theme == "Classic");

        var res = Application.Current.Resources;

        (Color bg, Color fg, Color ctrl) = s.Theme switch
        {
            "Dark"  => (Color.FromRgb(30, 30, 30), Colors.White, Color.FromRgb(60, 60, 60)),
            "Blue"  => (Color.FromRgb(230, 240, 250), Color.FromRgb(10, 30, 60), Colors.White),
            "Light" => (Colors.White, Color.FromRgb(20, 20, 20), Color.FromRgb(240, 240, 240)),
            _       => (SystemColors.ControlColor, SystemColors.ControlTextColor, SystemColors.ControlColor),
        };

        res["AppBackground"] = new SolidColorBrush(bg);
        res["AppForeground"] = new SolidColorBrush(fg);
        res["AppControlBg"]  = new SolidColorBrush(ctrl);

        foreach (var w in Application.Current.Windows.OfType<Window>().ToList())
            Style(w);
    }

    /// <summary>Applies colors, font family and size to one window.</summary>
    public static void Style(Window w)
    {
        var res = Application.Current.Resources;
        w.Background = (Brush)res["AppBackground"];
        w.Foreground = (Brush)res["AppForeground"];
        w.FontSize = AppSettings.Current.FontSize;
        w.FontFamily = new FontFamily(IsClassic ? "Tahoma, Microsoft Sans Serif, Segoe UI" : "Segoe UI");

        // Language: text direction (Arabic is right-to-left) and culture used by the calendar / date picker.
        w.FlowDirection = Loc.Flow;
        w.Language = Loc.XmlLang;
    }
}
