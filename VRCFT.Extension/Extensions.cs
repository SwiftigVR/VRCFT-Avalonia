namespace VRCFT.Extension;

public static class Extensions
{
    public static float LimitDecimal(this float value, int afterDecimal = 4)
        => (float)Math.Round((double)value, afterDecimal, MidpointRounding.AwayFromZero);

    public static double LimitDecimal(this double value, int afterDecimal = 4)
        => Math.Round(value, afterDecimal, MidpointRounding.AwayFromZero);

    public static float Invert(this float value)
        => -value;

    public static double Invert(this double value)
        => -value;
}