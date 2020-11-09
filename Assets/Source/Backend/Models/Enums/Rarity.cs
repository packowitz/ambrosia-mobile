using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Rarity
    {
        SIMPLE,
        COMMON,
        UNCOMMON,
        RARE,
        EPIC,
        LEGENDARY
    }

    public static class RarityExtension
    {
        public static int Stars(this Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.SIMPLE:
                    return 1;
                case Rarity.COMMON:
                    return 2;
                case Rarity.UNCOMMON:
                    return 3;
                case Rarity.RARE:
                    return 4;
                case Rarity.EPIC:
                    return 5;
                case Rarity.LEGENDARY:
                    return 6;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null);
            }
        } 
    }
}