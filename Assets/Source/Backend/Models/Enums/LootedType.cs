using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LootedType
    {
        BATTLE,
        STORY,
        CHEST,
        BREAKDOWN,
        LEVEL_UP,
        VIP_LEVEL_UP,
        MERCHANT,
        UPGRADE,
        BLACK_MARKET,
        ODD_JOB,
        DAILY_ACTIVITY,
        TASK
    }
}