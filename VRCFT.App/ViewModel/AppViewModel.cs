using Avalonia.Controls;
using System;
using VRCFT.App.Service;
using VRCFT.App.Utility;
using VRCFT.App.View;

namespace VRCFT.App.ViewModel;

public partial class AppViewModel : ViewModelBase
{
    #region Window Initialize

    private Window View { get; set; } = null!;

    public void Initialize()
    {
        View = new AppView();
        View.DataContext = this;
        View.Closing += OnClosing;

        LoadWindowState();

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

    public bool SliderTicksEnabled
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
            }
        }
    }

    public RelayCommand OpenAppData => field ??= new RelayCommand(() =>
    {
        WindowsUtils.OpenFolderAndSelectItem(ConfigManager.ConfigPath);
    });

    #endregion

    #region Window Controls

    public RelayCommand Minimize => field ??= new RelayCommand(() =>
    {
        // minimizes the Window
        View.WindowState = WindowState.Minimized;
    });

    public RelayCommand Maximize => field ??= new RelayCommand(() =>
    {
        // toggles between maximized and normal state
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

    public bool SendingEnabled
    {
        get => Osc.IsEnabled;
        set
        {
            if (Osc.IsEnabled != value)
            {
                Osc.IsEnabled = value;
                OnPropertyChanged();
            }
        }
    }

    public string ParameterPrefix
    {
        get => Osc.ParameterPrefix;
        set
        {
            if (Osc.ParameterPrefix != value)
            {
                Osc.ParameterPrefix = value;
                ConfigManager.Config.OscParamterPrefix = value;
                OnPropertyChanged();
            }
        }
    }

    #region Eye Look

    public bool SyncEyeLook
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
            }
        }
    }

    public double EyeLeftX
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                if (SyncEyeLook)
                    EyeRightX = value;

                float converted = (float)(Math.Clamp(value, -100d, 100d) / 100d);
                Osc.SendMessage(converted.LimitDecimal());

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

                if (SyncEyeLook)
                    EyeLeftX = value;

                float converted = (float)(Math.Clamp(value, -100d, 100d) / 100d);
                Osc.SendMessage(converted.LimitDecimal());

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
                Osc.SendMessage(converted.LimitDecimal());

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

                Osc.SendMessage(value.LimitDecimal());
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

                Osc.SendMessage(value.LimitDecimal());
                OnPropertyChanged();
            }
        }
    } = 0.75f;

    #endregion

    #region Simplified Expressions

    public bool SimplifiedExpressions
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
            }
        }
    }

    #region Eyebrow

    public float BrowExpressionRight
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value.LimitDecimal());
                OnPropertyChanged();
            }
        }
    }

    public float BrowExpressionLeft
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendMessage(value.LimitDecimal());
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