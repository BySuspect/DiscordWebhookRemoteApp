using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordWebhookRemoteApp.Helpers
{
    public static class MathP
    {
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0)
                return min;
            else if (value.CompareTo(max) > 0)
                return max;
            else
                return value;
        }
    }
}
