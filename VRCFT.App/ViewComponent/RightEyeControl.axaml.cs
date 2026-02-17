using Avalonia.Controls;
using Avalonia.Input;
using System;
using VRCFT.App.ViewModel;
using VRCFT.Extension;

namespace VRCFT.App.ViewComponent;

public partial class RightEyeControl : UserControl
{
    public RightEyeControl()
    {
        InitializeComponent();
    }

    private void RightEyeDrag(object? sender, VectorEventArgs e)
    {
        if (DataContext is not AppViewModel vm)
            return;

        var canvasWidth = RightEyeCanvas.Bounds.Width;
        var canvasHeight = RightEyeCanvas.Bounds.Height;
        if (canvasWidth <= 0 || canvasHeight <= 0)
            return;

        var currentLeft = Canvas.GetLeft(RightEyeThumb);
        var currentTop = Canvas.GetTop(RightEyeThumb);

        var newLeft = Math.Clamp(currentLeft + e.Vector.X, 0, canvasWidth);
        var newTop = Math.Clamp(currentTop + e.Vector.Y, 0, canvasHeight);

        var normalizedX = (newLeft / canvasWidth) * 200d - 100d;           // -100 (left) .. 100 (right)
        var normalizedY = (1d - (newTop / canvasHeight)) * 200d - 100d;    // -100 (bottom) .. 100 (top)

        var clampedX = Math.Clamp(normalizedX, -100d, 100d);
        var clampedY = Math.Clamp(normalizedY, -100d, 100d);

        vm.EyeRightX = clampedX;
        vm.EyeY = clampedY;
    }

    private void RightEyeReset(object? sender, TappedEventArgs e)
    {
        if (DataContext is not AppViewModel vm)
            return;

        vm.EyeRightX = 0;
        vm.EyeY = 0;
    }
}