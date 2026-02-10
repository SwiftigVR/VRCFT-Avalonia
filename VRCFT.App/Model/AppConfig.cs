using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace VRCFT.App.Model;

public class AppConfig : ObservableObject
{
    public Dictionary<string, WindowConfig> Windows { get; set; } = [];

    public bool SliderTicksEnabled
    {
        get => field;
        set => SetProperty(ref field, value);
    }

    public int OscSendingPort { get; set; } = 9000;
    public int OscListeningPort { get; set; } = 9001;

    public string OscParamterPrefix { get; set; } = string.Empty;
    public bool OscSyncEyeLook { get; set; } = false;
    public bool OscSimplifiedExpressions
    {
        get => field;
        set => SetProperty(ref field, value);
    }
}

public class WindowConfig
{
    public int Top { get; set; } = 100;
    public int Left { get; set; } = 100;

    public double Width { get; set; } = 1100;
    public double Height { get; set; } = 700;

    public WindowState State { get; set; } = WindowState.Normal;
}