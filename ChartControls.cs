using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace Apteka;

public record ChartPoint(string Label, double Value);

/// <summary>
/// Base class of the dashboard charts. The charts are drawn by hand (OnRender), so the project
/// needs no extra NuGet package. Colors follow the current theme (AppForeground brush).
/// </summary>
public abstract class ChartBase : FrameworkElement
{
    protected static readonly Color[] Palette =
    {
        Color.FromRgb(0x44, 0x72, 0xC4), Color.FromRgb(0xED, 0x7D, 0x31), Color.FromRgb(0x4E, 0x9A, 0x3A),
        Color.FromRgb(0xD9, 0x9A, 0x00), Color.FromRgb(0x7B, 0x5E, 0xA7), Color.FromRgb(0x2B, 0xA5, 0xB5),
        Color.FromRgb(0xC0, 0x50, 0x4D), Color.FromRgb(0x80, 0x80, 0x80)
    };

    List<ChartPoint> _data = new();
    public IReadOnlyList<ChartPoint> Data => _data;

    protected ChartBase()
    {
        FlowDirection = FlowDirection.LeftToRight;   // drawing is always left-to-right, also in Arabic
        ClipToBounds = true;
        SnapsToDevicePixels = true;
    }

    public void SetData(IEnumerable<ChartPoint> data)
    {
        _data = data.ToList();
        InvalidateVisual();
    }

    protected static Color FgColor =>
        (Application.Current.Resources["AppForeground"] as SolidColorBrush)?.Color ?? Colors.Black;

    protected static Brush FgBrush => new SolidColorBrush(FgColor);

    protected static Pen GridPen()
    {
        var c = FgColor;
        return new Pen(new SolidColorBrush(Color.FromArgb(50, c.R, c.G, c.B)), 1);
    }

    protected static Pen AxisPen()
    {
        var c = FgColor;
        return new Pen(new SolidColorBrush(Color.FromArgb(170, c.R, c.G, c.B)), 1);
    }

    protected FormattedText Txt(string s, double size, Brush brush, double maxWidth = double.PositiveInfinity, bool bold = false)
    {
        var ft = new FormattedText(
            s ?? "",
            CultureInfo.GetCultureInfo(Loc.Current.Culture),
            FlowDirection.LeftToRight,
            new Typeface(new FontFamily("Tahoma, Segoe UI"), FontStyles.Normal,
                         bold ? FontWeights.Bold : FontWeights.Normal, FontStretches.Normal),
            size, brush, VisualTreeHelper.GetDpi(this).PixelsPerDip);
        if (!double.IsInfinity(maxWidth))
        {
            ft.MaxTextWidth = Math.Max(1, maxWidth);
            ft.MaxLineCount = 1;
            ft.Trimming = TextTrimming.CharacterEllipsis;
        }
        return ft;
    }

    protected static string Fmt(double v) =>
        Math.Round(v, 2).ToString("0.##", CultureInfo.CurrentCulture);

    protected void DrawEmpty(DrawingContext dc)
    {
        var ft = Txt(Loc.T("Нет данных"), 13, FgBrush);
        dc.DrawText(ft, new Point((ActualWidth - ft.Width) / 2, (ActualHeight - ft.Height) / 2));
    }

    /// <summary>Rounds the maximum up to a "nice" number (1, 2, 2.5, 5 x 10^n) and returns the step of the grid.</summary>
    protected static double NiceMax(double max, out double step)
    {
        if (max <= 0) { step = 1; return 4; }
        double raw = max / 4;
        double mag = Math.Pow(10, Math.Floor(Math.Log10(raw)));
        double n = raw / mag;
        double nice = n <= 1 ? 1 : n <= 2 ? 2 : n <= 2.5 ? 2.5 : n <= 5 ? 5 : 10;
        step = nice * mag;
        return Math.Ceiling(max / step - 1e-9) * step;
    }

    /// <summary>Draws horizontal grid lines, the Y labels and both axes. Returns the top value of the Y axis.</summary>
    protected double DrawYAxis(DrawingContext dc, Rect plot, double dataMax)
    {
        double yMax = NiceMax(dataMax, out double step);
        int ticks = (int)Math.Round(yMax / step);
        var grid = GridPen();
        for (int i = 0; i <= ticks; i++)
        {
            double v = i * step;
            double y = plot.Bottom - v / yMax * plot.Height;
            dc.DrawLine(grid, new Point(plot.Left, y), new Point(plot.Right, y));
            var ft = Txt(Fmt(v), 10, FgBrush);
            dc.DrawText(ft, new Point(plot.Left - 6 - ft.Width, y - ft.Height / 2));
        }
        var axis = AxisPen();
        dc.DrawLine(axis, new Point(plot.Left, plot.Top), new Point(plot.Left, plot.Bottom));
        dc.DrawLine(axis, new Point(plot.Left, plot.Bottom), new Point(plot.Right, plot.Bottom));
        return yMax;
    }

    protected static Color Darken(Color c) =>
        Color.FromRgb((byte)(c.R * 0.7), (byte)(c.G * 0.7), (byte)(c.B * 0.7));
}

/// <summary>Bar (column) chart.</summary>
public sealed class BarChart : ChartBase
{
    /// <summary>true = all bars have the same color.</summary>
    public bool SingleColor { get; set; }

    protected override void OnRender(DrawingContext dc)
    {
        double w = ActualWidth, h = ActualHeight;
        if (w < 80 || h < 80) return;
        if (Data.Count == 0) { DrawEmpty(dc); return; }

        var plot = new Rect(52, 16, w - 52 - 12, h - 16 - 38);
        double yMax = DrawYAxis(dc, plot, Data.Max(d => d.Value));
        double slot = plot.Width / Data.Count;
        double bw = Math.Min(slot * 0.62, 70);

        for (int i = 0; i < Data.Count; i++)
        {
            var d = Data[i];
            double bh = Math.Max(0, d.Value) / yMax * plot.Height;
            double x = plot.Left + i * slot + (slot - bw) / 2;
            var color = SingleColor ? Palette[0] : Palette[i % Palette.Length];
            var rect = new Rect(x, plot.Bottom - bh, bw, bh);
            dc.DrawRectangle(new SolidColorBrush(color), new Pen(new SolidColorBrush(Darken(color)), 1), rect);

            var vt = Txt(Fmt(d.Value), 10, FgBrush);
            dc.DrawText(vt, new Point(x + bw / 2 - vt.Width / 2, rect.Top - vt.Height - 2));

            var lt = Txt(d.Label, 11, FgBrush, slot - 4);
            dc.DrawText(lt, new Point(plot.Left + i * slot + (slot - lt.Width) / 2, plot.Bottom + 5));
        }
    }
}

/// <summary>Line graph with markers and a light filled area.</summary>
public sealed class LineChart : ChartBase
{
    protected override void OnRender(DrawingContext dc)
    {
        double w = ActualWidth, h = ActualHeight;
        if (w < 120 || h < 80) return;
        if (Data.Count == 0) { DrawEmpty(dc); return; }

        var plot = new Rect(52, 16, w - 52 - 14, h - 16 - 36);
        double yMax = DrawYAxis(dc, plot, Data.Max(d => d.Value));
        int n = Data.Count;
        double x0 = plot.Left + 24, x1 = plot.Right - 24;

        var pts = new List<Point>();
        for (int i = 0; i < n; i++)
        {
            double x = n == 1 ? (x0 + x1) / 2 : x0 + i * (x1 - x0) / (n - 1);
            double y = plot.Bottom - Math.Max(0, Data[i].Value) / yMax * plot.Height;
            pts.Add(new Point(x, y));
        }

        var line = Palette[0];
        if (n > 1)
        {
            var area = new StreamGeometry();
            using (var ctx = area.Open())
            {
                ctx.BeginFigure(new Point(pts[0].X, plot.Bottom), true, true);
                ctx.PolyLineTo(pts, true, false);
                ctx.LineTo(new Point(pts[n - 1].X, plot.Bottom), true, false);
            }
            area.Freeze();
            dc.DrawGeometry(new SolidColorBrush(Color.FromArgb(55, line.R, line.G, line.B)), null, area);

            var ln = new StreamGeometry();
            using (var ctx = ln.Open())
            {
                ctx.BeginFigure(pts[0], false, false);
                ctx.PolyLineTo(pts.Skip(1).ToList(), true, true);
            }
            ln.Freeze();
            dc.DrawGeometry(null, new Pen(new SolidColorBrush(line), 2.5) { LineJoin = PenLineJoin.Round }, ln);
        }

        double capacity = (x1 - x0) / 56.0 + 1;                    // how many X labels fit
        int labelStep = Math.Max(1, (int)Math.Ceiling(n / Math.Max(1.0, capacity)));
        for (int i = 0; i < n; i++)
        {
            dc.DrawEllipse(new SolidColorBrush(line), new Pen(Brushes.White, 1.5), pts[i], 4, 4);

            if (n <= 15)
            {
                var vt = Txt(Fmt(Data[i].Value), 10, FgBrush);
                dc.DrawText(vt, new Point(pts[i].X - vt.Width / 2, pts[i].Y - vt.Height - 6));
            }
            if (i % labelStep == 0)
            {
                var lt = Txt(Data[i].Label, 10, FgBrush, 54);
                dc.DrawText(lt, new Point(pts[i].X - lt.Width / 2, plot.Bottom + 6));
            }
        }
    }
}

/// <summary>Pie chart with a legend (value and percent of each slice).</summary>
public sealed class PieChart : ChartBase
{
    static Point Pt(Point c, double r, double deg)
    {
        double a = deg * Math.PI / 180;
        return new Point(c.X + r * Math.Cos(a), c.Y + r * Math.Sin(a));
    }

    protected override void OnRender(DrawingContext dc)
    {
        double w = ActualWidth, h = ActualHeight;
        if (w < 120 || h < 80) return;
        var items = Data.Where(d => d.Value > 0).ToList();
        double total = items.Sum(d => d.Value);
        if (items.Count == 0 || total <= 0) { DrawEmpty(dc); return; }

        double r = Math.Min(h - 16, w * 0.5) / 2;
        var c = new Point(8 + r, h / 2);
        double start = -90;

        for (int i = 0; i < items.Count; i++)
        {
            double sweep = items[i].Value / total * 360;
            var brush = new SolidColorBrush(Palette[i % Palette.Length]);
            var pen = new Pen(Brushes.White, 1.5);

            if (sweep >= 359.99)
                dc.DrawEllipse(brush, pen, c, r, r);
            else
            {
                var p1 = Pt(c, r, start);
                var p2 = Pt(c, r, start + sweep);
                var g = new StreamGeometry();
                using (var ctx = g.Open())
                {
                    ctx.BeginFigure(c, true, true);
                    ctx.LineTo(p1, true, false);
                    ctx.ArcTo(p2, new Size(r, r), 0, sweep > 180, SweepDirection.Clockwise, true, false);
                }
                g.Freeze();
                dc.DrawGeometry(brush, pen, g);
            }

            if (sweep >= 25)   // percent inside big slices only
            {
                var mid = Pt(c, r * 0.62, start + sweep / 2);
                var pt = Txt((items[i].Value / total * 100).ToString("0") + "%", 11, Brushes.White, bold: true);
                dc.DrawText(pt, new Point(mid.X - pt.Width / 2, mid.Y - pt.Height / 2));
            }
            start += sweep;
        }

        // legend
        double lx = c.X + r + 24;
        double rowH = 22;
        double ly = Math.Max(8, h / 2 - items.Count * rowH / 2);
        for (int i = 0; i < items.Count; i++)
        {
            double y = ly + i * rowH;
            dc.DrawRectangle(new SolidColorBrush(Palette[i % Palette.Length]), null, new Rect(lx, y + 3, 12, 12));
            string text = items[i].Label + ": " + Fmt(items[i].Value) +
                          " (" + (items[i].Value / total * 100).ToString("0") + "%)";
            var lt = Txt(text, 12, FgBrush, w - (lx + 18) - 6);
            dc.DrawText(lt, new Point(lx + 18, y + 1));
        }
    }
}
