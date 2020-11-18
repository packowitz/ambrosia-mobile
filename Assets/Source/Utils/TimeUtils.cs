using System;

namespace Utils
{
    public static class TimeUtils
    {
        public static string Timer(this TimeSpan time)
        {
            if (time.Days > 0)
            {
                return $"{time.Days}:{time.Hours:D2}";
            }
            if (time.Hours > 0)
            {
                return $"{time.Hours}:{time.Minutes:D2}";
            }

            if (time.TotalSeconds > 0)
            {
                return $"{time.Minutes}:{time.Seconds:D2}";
            }

            return "0:00";
        }
        
        public static string TimerWithUnit(this TimeSpan time)
        {
            if (time.Days > 0)
            {
                return $"{time.Days}:{time.Hours:D2}d";
            }
            if (time.Hours > 0)
            {
                return $"{time.Hours}:{time.Minutes:D2}h";
            }

            if (time.TotalSeconds > 0)
            {
                return $"{time.Minutes}:{time.Seconds:D2}m";
            }

            return "0:00m";
        }
    }
}