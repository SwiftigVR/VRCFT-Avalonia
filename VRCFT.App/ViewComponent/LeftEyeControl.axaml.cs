using Avalonia.Controls;
using Avalonia.Input;
using System;
using VRCFT.App.Utility;
using VRCFT.App.ViewModel;

namespace VRCFT.App.ViewComponent;

public partial class LeftEyeControl : UserControl
{
    public LeftEyeControl()
    {
        InitializeComponent();
    }

    private void LeftEyeDrag(object? sender, VectorEventArgs e)
    {
        if (DataContext is not AppViewModel vm)
            return;

        var canvasWidth = LeftEyeCanvas.Bounds.Width;
        var canvasHeight = LeftEyeCanvas.Bounds.Height;
        if (canvasWidth <= 0 || canvasHeight <= 0)
            return;

        var currentLeft = Canvas.GetLeft(LeftEyeThumb);
        var currentTop = Canvas.GetTop(LeftEyeThumb);

        var newLeft = Math.Clamp(currentLeft + e.Vector.X, 0, canvasWidth);
        var newTop = Math.Clamp(currentTop + e.Vector.Y, 0, canvasHeight);

        var normalizedX = (newLeft / canvasWidth) * 200d - 100d;           // -100 (left) .. 100 (right)
        var normalizedY = (1d - (newTop / canvasHeight)) * 200d - 100d;    // -100 (bottom) .. 100 (top)

        var clampedX = Math.Clamp(normalizedX, -100d, 100d);
        var clampedY = Math.Clamp(normalizedY, -100d, 100d);

        vm.EyeLeftX = clampedX.LimitDecimal(4);
        vm.EyeY = clampedY.LimitDecimal(4);
    }

    private void LeftEyeReset(object? sender, TappedEventArgs e)
    {
        if (DataContext is not AppViewModel vm)
            return;

        vm.EyeLeftX = 0;
        vm.EyeY = 0;
    }
}