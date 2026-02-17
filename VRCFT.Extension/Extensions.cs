namespace VRCFT.Extension;

public static class Extensions
{
    /// <summary>
    /// Formats the given string by replacing all occurrences of "<br>" with Environment.NewLine.
    /// </summary>
    public static string FormatNewLines(this string text)
    {
        string formattedText = text.Replace(" <br> ", Environment.NewLine);
        formattedText = formattedText.Replace(" <br>", Environment.NewLine);
        formattedText = formattedText.Replace("<br> ", Environment.NewLine);
        return formattedText.Replace("<br>", Environment.NewLine);
    }

    public static float LimitDecimal(this float value, int afterDecimal = 4)
        => (float)Math.Round((double)value, afterDecimal, MidpointRounding.AwayFromZero);

    public static double LimitDecimal(this double value, int afterDecimal = 4)
        => Math.Round(value, afterDecimal, MidpointRounding.AwayFromZero);

    public static float Invert(this float value)
        => -value;

    public static double Invert(this double value)
        => -value;
}