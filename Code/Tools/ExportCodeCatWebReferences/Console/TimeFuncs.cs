using System;

namespace ExportCodeCatWebReferences
{
    public static class TimeFuncs
    {
        public static string ZeroTimeDisplay = "00:00:00";

        public static string DisplayTimeFromSeconds(int seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.ToString(@"hh\:mm\:ss");
        }

        public static string DisplayTimeFromSeconds(int? seconds)
        {
            if (seconds.HasValue)
                return DisplayTimeFromSeconds(seconds.Value);
            else
                return ZeroTimeDisplay;
        }

        public static string DisplayTimeFromSeconds(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return t.ToString(@"hh\:mm\:ss");
        }

        public static string DisplayTimeFromSeconds(double? seconds)
        {
            if (seconds.HasValue)
                return DisplayTimeFromSeconds(seconds.Value);
            else
                return ZeroTimeDisplay;
        }
    }
}
