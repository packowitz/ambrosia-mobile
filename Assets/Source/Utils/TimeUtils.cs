using System;

namespace Utils
{
    public static class TimeUtils
    {
        public static string Timer(this TimeSpan time)
        {
            if (time.Hours > 0)
            {
                return $"{time.Hours}:{time.Minutes:D2}";
            }
            else
            {
                return $"{time.Minutes}:{time.Seconds:D2}";
            }
        }
    }
}