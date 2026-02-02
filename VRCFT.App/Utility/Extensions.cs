using System;
using System.Collections.Generic;
using System.Text;

namespace VRCFT.App.Utility;

public static class Extensions
{
    public static float ToClampedFloat(this double value)
    {
        return (float)(Math.Clamp(value, -100d, 100d) / 100d);
    }
}
