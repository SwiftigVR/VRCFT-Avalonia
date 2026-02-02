using Avalonia.Controls;
using System;
using System.Runtime.CompilerServices;
using VRCFT.App.Service;
using VRCFT.App.Utility;
using VRCFT.App.View;

namespace VRCFT.App.ViewModel;

public partial class AppViewModel : ViewModelBase
{
    #region Window Initialize

    public Window View { get; private set; } = null!;

    public void Initialize()
    {
        View = new AppView();
        View.DataContext = this;
        View.Closing += OnClosing;

        ConfigManager.LoadPosition(View);

        View.Show();
    }

    private void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        ConfigManager.SavePosition(View);
    }

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

    #endregion

    #region Window Controls

    public RelayCommand Minimize
    {
        get => field ??= new RelayCommand(() =>
        {
            // minimizes the Window
            View.WindowState = WindowState.Minimized;
        });
    }

    public RelayCommand Maximize
    {
        get => field ??= new RelayCommand(() =>
        {
            // toggles between maximized and normal state
            if (View.WindowState == WindowState.Maximized)
                View.WindowState = WindowState.Normal;
            else
                View.WindowState = WindowState.Maximized;
        });
    }

    public RelayCommand Close
    {
        get => field ??= new RelayCommand(() =>
        {
            View.Close();
        });
    }

    #endregion

    #region Paramters

    private OscManager Osc
    {
        get => field ??= new OscManager();
    }

    public bool SendingEnabled
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

    private void SendParameterMessage(object value, [CallerMemberName] string parameterName = null!)
    {
        if (!SendingEnabled || string.IsNullOrEmpty(parameterName))
            return;

        Osc.SendMessage(parameterName, value);
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
            var rounded = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            if (field != rounded)
            {
                field = rounded;

                if (SyncEyeLook)
                    EyeRightX = rounded;

                SendParameterMessage(field.ToClampedFloat());
                OnPropertyChanged();
            }
        }
    }

    public double EyeRightX
    {
        get => field;
        set
        {
            var rounded = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            if (field != rounded)
            {
                field = rounded;

                if (SyncEyeLook)
                    EyeLeftX = rounded;

                SendParameterMessage(field.ToClampedFloat());
                OnPropertyChanged();
            }
        }
    }

    public double EyeY
    {
        get => field;
        set
        {
            var rounded = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            if (field != rounded)
            {
                field = rounded;
                SendParameterMessage(field.ToClampedFloat());
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #endregion
}