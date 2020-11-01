using System;

namespace Utils
{
    public static class NumberUtil
    {
        public static string RoundAmount(long amount)
        {
            if (amount >= 1000000) {
                return $"{Math.Floor(amount / 100000.0) / 10}m";
            }
            if (amount >= 1000) {
                return $"{Math.Floor(amount / 100.0) / 10}k";
            }
            return amount.ToString();
        }
    }
}