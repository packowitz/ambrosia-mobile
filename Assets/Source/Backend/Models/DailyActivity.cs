using System;

namespace Backend.Models
{
    public class DailyActivity
    {
        public long playerId;
        public DateTime? day1;
        public bool day1claimed;
        public DateTime? day2;
        public bool day2claimed;
        public DateTime? day3;
        public bool day3claimed;
        public DateTime? day4;
        public bool day4claimed;
        public DateTime? day5;
        public bool day5claimed;
        public DateTime? day6;
        public bool day6claimed;
        public DateTime? day7;
        public bool day7claimed;
        public DateTime? day8;
        public bool day8claimed;
        public DateTime? day9;
        public bool day9claimed;
        public DateTime? day10;
        public bool day10claimed;

        public int today;

        public bool Claimable(int day)
        {
            switch (day)
            {
                case 1: return day1 != null && !day1claimed;
                case 2: return day2 != null && !day2claimed;
                case 3: return day3 != null && !day3claimed;
                case 4: return day4 != null && !day4claimed;
                case 5: return day5 != null && !day5claimed;
                case 6: return day6 != null && !day6claimed;
                case 7: return day7 != null && !day7claimed;
                case 8: return day8 != null && !day8claimed;
                case 9: return day9 != null && !day9claimed;
                case 10: return day10 != null && !day10claimed;
                default: return false;
            }
        }

        public bool Claimed(int day)
        {
            switch (day)
            {
                case 1: return day1claimed;
                case 2: return day2claimed;
                case 3: return day3claimed;
                case 4: return day4claimed;
                case 5: return day5claimed;
                case 6: return day6claimed;
                case 7: return day7claimed;
                case 8: return day8claimed;
                case 9: return day9claimed;
                case 10: return day10claimed;
                default: return false;
            }
        }
    }
}