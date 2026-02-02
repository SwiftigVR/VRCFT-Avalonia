using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;

namespace VRCFT.App.ViewComponent.Converter
{
    public sealed class MinDoubleConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count < 2 || values[0] is not double w || values[1] is not double h)
                return 0d;

            return Math.Min(w, h);
        }
    }
}