using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Modification
    {
        REROLL_QUALITY,
        REROLL_STAT,
        INC_RARITY,
        ADD_JEWEL,
        REROLL_JEWEL_1,
        REROLL_JEWEL_2,
        REROLL_JEWEL_3,
        REROLL_JEWEL_4,
        ADD_SPECIAL_JEWEL
    }
}