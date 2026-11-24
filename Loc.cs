using System.Windows;
using System.Windows.Markup;

namespace Apteka;

public sealed record Lang(string Code, string Native, string Culture, bool Rtl);

/// <summary>
/// Tiny localization layer. Russian is the source language: the Russian text itself is the key,
/// e.g. Loc.T("Сохранить"). Other languages are looked up in Translations.cs.
/// A missing translation falls back to the Russian text, so nothing can ever break or come out empty.
/// To add a language: add it to Languages below and add one more column to every Add(...) line.
/// </summary>
public static partial class Loc
{
    public static readonly Lang[] Languages =
    {
        new("ru", "Русский",  "ru-RU", false),
        new("en", "English",  "en-US", false),
        new("fr", "Français", "fr-FR", false),
        new("ar", "العربية",  "ar-AE", true),   // ar-AE = Gregorian calendar (ar-SA would show Hijri dates)
    };

    // key = Russian text, value = [en, fr, ar]
    static readonly Dictionary<string, string[]> Map = new();
    static int _idx = -1;   // -1 = Russian (source text)

    public static Lang Current { get; private set; } = Languages[0];
    public static bool IsRtl => Current.Rtl;
    public static FlowDirection Flow => IsRtl ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
    public static XmlLanguage XmlLang => XmlLanguage.GetLanguage(Current.Culture);

    public static void Set(string code)
    {
        Current = Languages.FirstOrDefault(l => l.Code == code) ?? Languages[0];
        _idx = Current.Code switch { "en" => 0, "fr" => 1, "ar" => 2, _ => -1 };
    }

    public static string T(string ru)
    {
        if (_idx >= 0 && ru != null && Map.TryGetValue(ru, out var t) && _idx < t.Length
            && !string.IsNullOrEmpty(t[_idx]))
            return t[_idx];
        return ru;
    }

    public static string T(string ru, params object[] args) => string.Format(T(ru), args);

    static void Add(string ru, string en, string fr, string ar) => Map[ru] = new[] { en, fr, ar };
}

/// <summary>XAML: Text="{l:Tr 'Сохранить'}"  (xmlns:l="clr-namespace:Apteka")</summary>
[MarkupExtensionReturnType(typeof(string))]
public class TrExtension : MarkupExtension
{
    public string Text { get; set; }
    public TrExtension() { }
    public TrExtension(string text) { Text = text; }
    public override object ProvideValue(IServiceProvider serviceProvider) => Loc.T(Text);
}
