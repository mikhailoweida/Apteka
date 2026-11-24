using System.IO;
using System.Text.Json;

namespace Apteka;

public class AppSettingsData
{
    public string Theme { get; set; } = "Classic";   // Classic | Light | Dark | Blue
    public double FontSize { get; set; } = 12;
    public string Language { get; set; } = "ru";     // ru | en | fr | ar
}

public static class AppSettings
{
    static readonly string Dir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Apteka");
    static readonly string FilePath = Path.Combine(Dir, "settings.json");

    public static AppSettingsData Current { get; private set; } = new();

    public static void Load()
    {
        try
        {
            if (File.Exists(FilePath))
                Current = JsonSerializer.Deserialize<AppSettingsData>(File.ReadAllText(FilePath)) ?? new();
        }
        catch { Current = new(); }
    }

    public static void Save()
    {
        try
        {
            Directory.CreateDirectory(Dir);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(Current,
                new JsonSerializerOptions { WriteIndented = true }));
        }
        catch { /* settings must never crash the app */ }
    }
}
