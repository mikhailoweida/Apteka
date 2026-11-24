using System.Windows.Controls;
using System.Windows.Media;

namespace Apteka;

public partial class KeyHeader : UserControl
{
    public KeyHeader() { InitializeComponent(); }

    public string Line1 { set => T1.Text = value; }
    public string Line2 { set => T2.Text = value; }

    /// <summary>Text of the white strip: a prompt, or a red error message.</summary>
    public void SetHint(string text, bool error = false)
    {
        T3.Text = text;
        T3.Foreground = error ? Brushes.Red : Brushes.Black;
    }
}
