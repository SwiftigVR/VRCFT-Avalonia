using Avalonia;
using Avalonia.Controls;
using System;
using System.IO;
using System.Text.Json;

namespace VRCFT.App.Service;

public static class ConfigManager
{
    private static string ConfigFileName => "config.json";
    private static string ConfigFolderName => "Swift's VRCFT Debug Tool";

    private static string ConfigDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigFolderName);
    private static string ConfigPath => Path.Combine(ConfigDirectory, ConfigFileName);

    private static AppConfig? LoadedConfig { get; set; } = null;

    public static void LoadConfig(Window view)
    {
        if (!File.Exists(ConfigPath))
            return;

        try
        {
            string configJson = File.ReadAllText(ConfigPath);
            LoadedConfig = JsonSerializer.Deserialize<AppConfig>(configJson);
        }
        catch
        {
            return;
        }

        if (LoadedConfig == null)
            return;

        view.Position = new PixelPoint(LoadedConfig.Left, LoadedConfig.Top);
        view.Width = LoadedConfig.Width;
        view.Height = LoadedConfig.Height;

        if (LoadedConfig.State != WindowState.Minimized)
            view.WindowState = LoadedConfig.State;
        else
            view.WindowState = WindowState.Normal;
    }

    public static void SaveConfig(Window view)
    {
        if (!Directory.Exists(ConfigDirectory))
            Directory.CreateDirectory(ConfigDirectory);

        var newConfig = new AppConfig();

        newConfig.Top = view.Position.Y;
        newConfig.Left = view.Position.X;
        newConfig.Width = view.Width;
        newConfig.Height = view.Height;
        newConfig.State = view.WindowState;

        string configJson = JsonSerializer.Serialize(newConfig);
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