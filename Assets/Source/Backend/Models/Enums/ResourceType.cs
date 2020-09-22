using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResourceType
    {
        STEAM,
        STEAM_MAX,
        PREMIUM_STEAM,
        PREMIUM_STEAM_MAX,
        COGWHEELS,
        COGWHEELS_MAX,
        PREMIUM_COGWHEELS,
        PREMIUM_COGWHEELS_MAX,
        TOKENS,
        TOKENS_MAX,
        PREMIUM_TOKENS,
        PREMIUM_TOKENS_MAX,
        COINS,
        RUBIES,
        METAL,
        METAL_MAX,
        IRON,
        IRON_MAX,
        STEEL,
        STEEL_MAX,
        WOOD,
        WOOD_MAX,
        BROWN_COAL,
        BROWN_COAL_MAX,
        BLACK_COAL,
        BLACK_COAL_MAX,
        SIMPLE_GENOME,
        COMMON_GENOME,
        UNCOMMON_GENOME,
        RARE_GENOME,
        EPIC_GENOME,
        ODD_JOB,
        RESOURCE_GENERATION_SPEED,
        WOODEN_KEYS,
        BRONZE_KEYS,
        SILVER_KEYS,
        GOLDEN_KEYS
    }
}