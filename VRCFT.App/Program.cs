using Avalonia;
using System;
using VRCFT.App.Service;

namespace VRCFT.App;

internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static int Main(string[] args)
    {
        try
        {
            OnAppStartup();
            int exitCode = BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
            OnAppExit();

            return exitCode;
        }
        catch //(Exception ex)
        {
            //Debug.WriteLine(ex.ToString());
            return -1;
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        var builder = AppBuilder.Configure<App>()
                                .UsePlatformDetect()
                                .WithInterFont()
                                .LogToTrace();

        return builder;
    }

    private static void OnAppStartup()
    {
        ConfigManager.LoadConfig();
    }

    private static void OnAppExit()
    {
        ConfigManager.SaveConfig();
    }
}
