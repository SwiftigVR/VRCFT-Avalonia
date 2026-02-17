using Avalonia;
using Avalonia.Controls;
using System;
using VRCFT.App.Service;
using VRCFT.App.View;
using VRCFT.Base;
using VRCFT.Extension;
using VRCFT.Extension.MessageBox;

namespace VRCFT.App.ViewModel;

public partial class AppViewModel : ViewModelBase
{
    #region Window Initialize

    private Window View { get; set; } = null!;

    public void Initialize()
    {
        View = new AppView();
        View.DataContext = this;
        View.Topmost = ConfigManager.Config.AlwaysOnTop;
        View.Closing += OnClosing;

        LoadWindowState();
        SetMainWindow(View);

        View.Show();
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        SaveWindowState();
    }

    private void LoadWindowState() => ConfigManager.LoadWindowFromConfig(nameof(AppViewModel), View);
    private void SaveWindowState() => ConfigManager.SaveWindowToConfig(nameof(AppViewModel), View);

    #endregion

    #region UI

    public bool AlwaysOnTop { get; set; } = ConfigManager.Config.AlwaysOnTop;

    public RelayCommand ToggleAlwaysOnTop => field ??= new RelayCommand(() =>
    {
        bool newValue = !ConfigManager.Config.AlwaysOnTop;

        ConfigManager.Config.AlwaysOnTop = newValue;
        View.Topmost = newValue;

        AlwaysOnTop = newValue;
        OnPropertyChanged(nameof(AlwaysOnTop));
    });

    public RelayCommand RandomMessage => field ??= new RelayCommand(() =>
    {
        var rng = new Random();

        _ = MessageBox.ShowAsync
        (
            View,
            "Test",
            Globals.LoremIpsum.FormatNewLines(),
            (MessageBoxButtons)rng.Next(0, 3),
            (MessageBoxIcon)rng.Next(0, 5)
        );
    });

    public RelayCommand OpenSettings => field ??= new RelayCommand(() =>
    {
        new SettingsViewModel().Initialize();
    });

    public RelayCommand OpenAppData => field ??= new RelayCommand(() =>
    {
        WindowsUtils.OpenFolderAndSelectItem(ConfigManager.ConfigPath);
    });

    #endregion

    #region Window Controls

    public RelayCommand Minimize => field ??= new RelayCommand(() =>
    {
        View.WindowState = WindowState.Minimized;
    });

    public RelayCommand Maximize => field ??= new RelayCommand(() =>
    {
        if (View.WindowState == WindowState.Maximized)
            View.WindowState = WindowState.Normal;
        else
            View.WindowState = WindowState.Maximized;
    });

    public RelayCommand Close => field ??= new RelayCommand(() =>
    {
        View.Close();
    });

    #endregion

    #region Parameters

    private OscManager Osc => field ??= new OscManager();

    #region Eye Look

    public double EyeLeftX
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                if (ConfigManager.Config.OscSyncEyeLook && EyeRightX != value)
                    EyeRightX = value;

                float converted = (float)(Math.Clamp(value, -100d, 100d) / 100d);
                Osc.SendMessage(converted);

                OnPropertyChanged();
            }
        }
    }

    public double EyeRightX
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                if (ConfigManager.Config.OscSyncEyeLook && EyeLeftX != value)
                    EyeLeftX = value;

                float converted = (float)(Math.Clamp(value, -100d, 100d) / 100d);
                Osc.SendMessage(converted);

                OnPropertyChanged();
            }
        }
    }

    public double EyeY
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                float converted = (float)(Math.Clamp(value, -100d, 100d) / 100d);
                Osc.SendMessage(converted);

                OnPropertyChanged();
            }
        }
    }

    #endregion

    #region Eyelid

    public float EyeLidRight
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value);
                OnPropertyChanged();
            }
        }
    } = 0.75f;

    public float EyeLidLeft
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value);
                OnPropertyChanged();
            }
        }
    } = 0.75f;

    #endregion

    #region Simplified Expressions

    #region Eyebrow

    public float BrowDownRight
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value);
                OnPropertyChanged();
            }
        }
    } = 1f;

    public float BrowDownLeft
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value);
                OnPropertyChanged();
            }
        }
    } = 1f;

    public float BrowOuterUp
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value);
                OnPropertyChanged();
            }
        }
    }

    public float BrowInnerUp
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value);
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #endregion

    #region Regular Expressions



    #endregion

    #endregion
}