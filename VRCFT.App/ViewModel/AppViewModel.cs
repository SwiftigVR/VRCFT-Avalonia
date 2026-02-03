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
        get => Osc.Enabled;
        set
        {
            if (Osc.Enabled != value)
            {
                Osc.Enabled = value;
                OnPropertyChanged();
            }
        }
    }

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

    private float ToClampedFloat(double value)
    {
        return (float)(Math.Clamp(value, -100d, 100d) / 100d);
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

                Osc.SendParameterMessage(ToClampedFloat(value));
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

                Osc.SendParameterMessage(ToClampedFloat(value));
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

                Osc.SendParameterMessage(ToClampedFloat(value));
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

                Osc.SendParameterMessage(value.LimitDecimal(4));
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

                Osc.SendParameterMessage(value.LimitDecimal(4));
                OnPropertyChanged();
            }
        }
    } = 0.75f;

    #endregion

    #region Eyebrow

    public float BrowExpressionRight
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;

                Osc.SendParameterMessage(value.LimitDecimal(4));
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

                Osc.SendParameterMessage(value.LimitDecimal(4));
                OnPropertyChanged();
            }
        }
    }

    #endregion

    #endregion
}