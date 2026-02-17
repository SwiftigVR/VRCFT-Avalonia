using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using VRCFT.App.Service;

namespace VRCFT.App.Model;

public class AppConfig : ObservableObject
{
    public Dictionary<string, WindowConfig> Windows { get; set; } = [];

    public bool AlwaysOnTop
    {
        get => field;
        set => SetProperty(ref field, value);
    }

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