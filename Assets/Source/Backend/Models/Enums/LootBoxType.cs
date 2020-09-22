using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LootBoxType
    {
        LOOT,
        STORY,
        EXPEDITION,
        MERCHANT,
        ODD_JOB,
        ACHIEVEMENT,
        BLACK_MARKET,
        INBOX
    }
}