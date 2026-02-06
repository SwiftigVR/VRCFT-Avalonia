using System;

namespace VRCFT.App.Utility;

public static class Extensions
{
    public static float LimitDecimal(this float value, int afterDecimal = 4)
    {
        return (float)Math.Round((double)value, afterDecimal, MidpointRounding.AwayFromZero);
    }

    public static double LimitDecimal(this double value, int afterDecimal = 4)
    {
        return Math.Round(value, afterDecimal, MidpointRounding.AwayFromZero);
    }
}