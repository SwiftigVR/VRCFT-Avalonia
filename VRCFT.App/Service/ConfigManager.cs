using Avalonia;
using Avalonia.Controls;
using System;
using System.IO;
using System.Text.Json;
using VRCFT.App.Model;

namespace VRCFT.App.Service;

public static class ConfigManager
{
    private static string ConfigFileName => "config.json";
    private static string ConfigFolderName => "Swift's VRCFT Debug Tool";

    private static string ConfigDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigFolderName);
    private static string ConfigPath => Path.Combine(ConfigDirectory, ConfigFileName);

    private static AppConfig? LoadedConfig { get; set; }

    public static void LoadConfig(Window view)
    {
        if (!File.Exists(ConfigPath))
            return;

        try
        {
            string configJson = File.ReadAllText(ConfigPath);
            LoadedConfig = JsonSerializer.Deserialize<AppConfig>(configJson);
        }
        catch { return; }

        if (LoadedConfig != null)
        {
            view.Position = new PixelPoint(LoadedConfig.Left, LoadedConfig.Top);
            view.Width = LoadedConfig.Width;
            view.Height = LoadedConfig.Height;

            if (LoadedConfig.State != WindowState.Minimized)
                view.WindowState = LoadedConfig.State;
            else
                view.WindowState = WindowState.Normal;

            //App.Current!.RequestedThemeVariant = LoadedConfig.Theme;
        }
    }

    public static void SaveConfig(Window view)
    {
        if (!Directory.Exists(ConfigDirectory))
            Directory.CreateDirectory(ConfigDirectory);

        var newConfig = new AppConfig();

        if (view.WindowState == WindowState.Normal)
        {
            newConfig.Top = view.Position.Y;
            newConfig.Left = view.Position.X;
            newConfig.Width = view.Width;
            newConfig.Height = view.Height;
        }
        else
        {
            newConfig.Top = LoadedConfig != null ? LoadedConfig.Top : 100;
            newConfig.Left = LoadedConfig != null ? LoadedConfig.Left : 100;
            newConfig.Width = LoadedConfig != null ? LoadedConfig.Width : 1100;
            newConfig.Height = LoadedConfig != null ? LoadedConfig.Height : 700;
        }

        newConfig.State = view.WindowState;
        //newConfig.Theme = App.Current!.ActualThemeVariant;

        try
        {
            string configJson = JsonSerializer.Serialize(newConfig);
            File.WriteAllText(ConfigPath, configJson);
        }
        catch { }
    }
}