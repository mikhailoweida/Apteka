using System.Windows;
using System.Windows.Threading;

namespace Apteka;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        DispatcherUnhandledException += OnUnhandled;

        AppSettings.Load();
        Loc.Set(AppSettings.Current.Language);     // must happen before the first window is built
        ThemeManager.Apply(AppSettings.Current);

        new LoginWindow().Show();
    }

    static void OnUnhandled(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        var f = DbErrors.Describe(e.Exception);
        Util.Msg(f.Message, f.Title, MessageBoxButton.OK, f.Icon);
        e.Handled = true;
    }
}
