using Avalonia;
using Avalonia.Controls;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using VRCFT.App.Model;
using VRCFT.App.ViewModel;

namespace VRCFT.App.Service;

public static class ConfigManager
{
    private static string ConfigFileName => "config.json";
    private static string ConfigFolderName => "Swift's VRCFT Debug Tool";

    public static string ConfigDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ConfigFolderName);
    public static string ConfigPath => Path.Combine(ConfigDirectory, ConfigFileName);

    public static AppConfig Config { get; private set; } = new();

    private static JsonSerializerOptions _options = null!;

    public static void LoadConfig()
    {
        if (!File.Exists(ConfigPath))
            return;

        try
        {
            string configJson = File.ReadAllText(ConfigPath);
            var savedConfig = JsonSerializer.Deserialize<AppConfig>(configJson);

            if (savedConfig != null)
                Config = savedConfig;
        }
        catch { }
    }

    public static void SaveConfig()
    {
        if (!Directory.Exists(ConfigDirectory))
            Directory.CreateDirectory(ConfigDirectory);

        try
        {
            _options ??= new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            string configJson = JsonSerializer.Serialize(Config, _options);
            File.WriteAllText(ConfigPath, configJson);
        }
        catch { }
    }

    #region Windows

    public static void LoadWindowFromConfig(string viewModelName, Window view)
    {
        if (!Config.Windows.ContainsKey(viewModelName))
            return;

        var config = Config.Windows[viewModelName];
        if (config == null)
            return;

        view.Position = new PixelPoint(config.Left, config.Top);
        view.Width = config.Width;
        view.Height = config.Height;

        if (config.State == WindowState.Minimized)
            view.WindowState = WindowState.Normal;
    }

    public static void SaveWindowToConfig(string viewModelName, Window view)
    {
        bool normalState = view.WindowState == WindowState.Normal;

        var config = new WindowConfig()
        {
            Top = normalState ? view.Position.Y : 100,
            Left = normalState ? view.Position.X : 100,
            Width = normalState ? view.Width : 1100,
            Height = normalState ? view.Height : 700,
            State = view.WindowState
        };

        Config.Windows[viewModelName] = config;
    }

    #endregion
}