using System.IO;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Apteka;

public static class Util
{
    /// <summary>MessageBox that follows the UI language direction (right-to-left for Arabic).</summary>
    public static MessageBoxResult Msg(string text, string caption,
        MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None)
    {
        var opt = Loc.IsRtl ? MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign : MessageBoxOptions.None;
        return MessageBox.Show(text, caption, buttons, icon, MessageBoxResult.None, opt);
    }

    /// <summary>Shows a human-readable explanation of any error; the raw text goes to ActionLog only.</summary>
    public static void ShowError(Exception ex)
    {
        var f = DbErrors.Describe(ex);
        Msg(f.Message, f.Title, MessageBoxButton.OK, f.Icon);
        Logger.Write("Ошибка: " + ex.Message);
    }

    public static void FixColumn(DataGridAutoGeneratingColumnEventArgs e)
    {
        e.Column.Header = e.PropertyName.Replace("_", "__");
        if (e.PropertyType == typeof(DateTime) && e.Column is DataGridTextColumn tc)
            tc.Binding = new Binding("[" + e.PropertyName + "]") { StringFormat = "dd.MM.yyyy" };
    }

    static string Q(string s) => "\"" + s.Replace("\"", "\"\"") + "\"";

    static string Fmt(object v)
    {
        if (v is DBNull) return "";
        if (v is DateTime d) return d.ToString("dd.MM.yyyy");
        return Convert.ToString(v, CultureInfo.CurrentCulture);
    }

    public static void ExportCsv(DataTable dt, string name)
    {
        if (dt == null || dt.Rows.Count == 0)
        {
            Msg(Loc.T("Нет данных для экспорта"), Loc.T("Экспорт"),
                MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var dlg = new Microsoft.Win32.SaveFileDialog { Filter = "CSV (*.csv)|*.csv", FileName = name + ".csv" };
        if (dlg.ShowDialog() != true) return;
        try
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Join(";", dt.Columns.Cast<DataColumn>().Select(c => Q(c.ColumnName))));
            foreach (DataRow r in dt.Rows)
                sb.AppendLine(string.Join(";", r.ItemArray.Select(v => Q(Fmt(v)))));
            File.WriteAllText(dlg.FileName, sb.ToString(), new UTF8Encoding(true));
            Logger.Write("Экспорт в CSV: " + name);
        }
        catch (Exception ex)   // most common cause: the file is open in Excel
        {
            Msg(ex.Message, Loc.T("Ошибка экспорта"), MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
