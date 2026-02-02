using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace VRCFT.App.ViewComponent.Converter;

public sealed class EyeXToCanvasConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 2 || values[0] is not double eyeX || values[1] is not double width || width <= 0d)
            return 0d;

        var clamped = Math.Clamp(eyeX, -100d, 100d);

        return (clamped + 100d) / 200d * width;
    }
}

public sealed class EyeYToCanvasConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (values.Count < 2 || values[0] is not double eyeY || values[1] is not double height || height <= 0d)
            return 0d;

        var clamped = Math.Clamp(eyeY, -100d, 100d);
        // eyeY = 100 -> top, eyeY = -100 -> bottom
        return (100d - clamped) / 200d * height;
    }
}