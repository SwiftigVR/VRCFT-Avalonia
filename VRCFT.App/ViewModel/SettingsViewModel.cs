using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using VRCFT.App.Service;
using VRCFT.App.Utility;
using VRCFT.App.View;

namespace VRCFT.App.ViewModel;

public class SettingsViewModel : ViewModelBase
{
    #region Window Initialize

    private Window View { get; set; } = null!;

    public void Initialize()
    {
        View = new SettingsView();
        View.DataContext = this;
        View.Closing += OnClosing;

        LoadWindowState();

        View.ShowDialog(GetMainWindow()!);
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        SaveWindowState();
    }

    private void LoadWindowState() => ConfigManager.LoadWindowFromConfig(nameof(SettingsViewModel), View);
    private void SaveWindowState() => ConfigManager.SaveWindowToConfig(nameof(SettingsViewModel), View);

    #endregion

    #region Settings

    public bool SliderTicksEnabled
    {
        get => ConfigManager.Config.SliderTicksEnabled;
        set
        {
            if (ConfigManager.Config.SliderTicksEnabled != value)
            {
                ConfigManager.Config.SliderTicksEnabled = value;
                OnPropertyChanged();
            }
        }
    }

    public int OscSendingPort
    {
        get => ConfigManager.Config.OscSendingPort;
        set
        {
            if (ConfigManager.Config.OscSendingPort != value)
            {
                ConfigManager.Config.OscSendingPort = value;
                OnPropertyChanged();
            }
        }
    }

    public int OscReceivingPort
    {
        get => ConfigManager.Config.OscListeningPort;
        set
        {
            if (ConfigManager.Config.OscListeningPort != value)
            {
                ConfigManager.Config.OscListeningPort = value;
                OnPropertyChanged();
            }
        }
    }

    public bool SyncEyeLook
    {
        get => ConfigManager.Config.OscSyncEyeLook;
        set
        {
            if (ConfigManager.Config.OscSyncEyeLook != value)
            {
                ConfigManager.Config.OscSyncEyeLook = value;
                OnPropertyChanged();
            }
        }
    }

    public bool SimplifiedExpressions
    {
        get => ConfigManager.Config.OscSimplifiedExpressions;
        set
        {
            if (ConfigManager.Config.OscSimplifiedExpressions != value)
            {
                ConfigManager.Config.OscSimplifiedExpressions = value;
                OnPropertyChanged();
            }
        }
    }

    public string ParameterPrefix
    {
        get => ConfigManager.Config.OscParamterPrefix;
        set
        {
            if (ConfigManager.Config.OscParamterPrefix != value)
            {
                ConfigManager.Config.OscParamterPrefix = value;
                OnPropertyChanged();
            }
        }
    }

    #endregion
}