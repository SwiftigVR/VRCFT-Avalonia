using Avalonia.Controls;
using System.Collections.Generic;

namespace VRCFT.App.Model;

public class AppConfig
{
    public Dictionary<string, WindowConfig> Windows { get; set; } = [];

    public bool SliderTicksEnabled { get; set; } = false;

    public string OscParamterPrefix { get; set; } = string.Empty;
    public bool OscSyncEyeLook { get; set; } = false;
    public bool OscSimplifiedExpressions { get; set; } = false;
}

public class WindowConfig
{
    public int Top { get; set; } = 100;
    public int Left { get; set; } = 100;

    public double Width { get; set; } = 1100;
    public double Height { get; set; } = 700;

    public WindowState State { get; set; } = WindowState.Normal;
}