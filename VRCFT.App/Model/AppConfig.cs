using Avalonia.Controls;
using Avalonia.Styling;

namespace VRCFT.App.Model;

public class AppConfig
{
    public int Top { get; set; }
    public int Left { get; set; }

    public double Width { get; set; }
    public double Height { get; set; }

    public WindowState State { get; set; }

    public ThemeVariant Theme { get; set; } = ThemeVariant.Default;
}