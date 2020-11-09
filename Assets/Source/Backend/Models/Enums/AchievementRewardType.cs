using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AchievementRewardType
    {
        STEAM_USED,
        COGWHEELS_USED,
        TOKENS_USED,
        COINS_USED,
        RUBIES_USED,
        METAL_USED,
        IRON_USED,
        STEEL_USED,
        WOOD_USED,
        BROWN_COAL_USED,
        BLACK_COAL_USED,
        SIMPLE_INCUBATIONS,
        COMMON_INCUBATIONS,
        UNCOMMON_INCUBATIONS,
        RARE_INCUBATIONS,
        EPIC_INCUBATIONS,
        EXPEDITIONS,
        ODD_JOBS,
        DAILY_ACTIVITY,
        ACADEMY_XP,
        ACADEMY_ASC,
        MERCHANT_ITEMS_BOUGHT,
        MAP_TILES_DISCOVERED,
        GEAR_MODIFICATIONS,
        GEAR_BREAKDOWN,
        JEWELS_MERGED,
        BUILDING_UPGRADES,
        VEHICLE_UPGRADES,
        VEHICLE_PART_UPGRADES,
        BUILDING_MIN_LEVEL,
        WOODEN_KEYS_COLLECTED,
        BRONZE_KEYS_COLLECTED,
        SILVER_KEYS_COLLECTED,
        GOLDEN_KEYS_COLLECTED,
        CHESTS_OPENED
    }
}