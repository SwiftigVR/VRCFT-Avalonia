using Avalonia;
using Avalonia.Controls;
using System;
using System.IO;
using System.Text.Json;

namespace VRCFT.App.Service;

public static class ConfigManager
{
    private static string ConfigFileName => "config.json";
    private static string ConfigDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Swift's VRCFT Debug Tool");
    private static string ConfigPath => Path.Combine(ConfigDirectory, ConfigFileName);

    public static void LoadPosition(Window view)
    {
        if (!File.Exists(ConfigPath))
        {
            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            return;
        }

        AppConfig? config = null;

        try
        {
            string configJson = File.ReadAllText(ConfigPath);
            config = JsonSerializer.Deserialize<AppConfig>(configJson);

            if (config == null)
                return;
        }
        catch { return; }

        view.Position = new PixelPoint(config.Left, config.Top);
        view.Width = config.Width;
        view.Height = config.Height;

        if (config.State == WindowState.Minimized)
            view.WindowState = WindowState.Normal;
        else
            view.WindowState = config.State;
    }

    public static void SavePosition(Window view)
    {
        if (!Directory.Exists(ConfigDirectory))
            Directory.CreateDirectory(ConfigDirectory);

        AppConfig newConfig = new()
        {
            Top = view.Position.Y,
            Left = view.Position.X,
            Width = view.Width,
            Height = view.Height,
            State = view.WindowState
        };

        string configJson = JsonSerializer.Serialize<AppConfig>(newConfig);
        File.WriteAllText(ConfigPath, configJson);
    }
}

public class AppConfig
{
    public int Top { get; set; }
    public int Left { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }

    public WindowState State { get; set; }
}