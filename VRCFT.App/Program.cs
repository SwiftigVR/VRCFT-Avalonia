using System;
using Avalonia;

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
#if DEBUG
            BuildAvaloniaApp().WithDeveloperTools().StartWithClassicDesktopLifetime(args);
#else
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
#endif
            return 0;
        }
        catch //(Exception ex)
        {
            //Console.WriteLine(ex.ToString());
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
}
