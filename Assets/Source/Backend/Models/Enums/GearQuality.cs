using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GearQuality
    {
        SHABBY,
        RUSTY,
        ORDINARY,
        USEFUL,
        GOOD,
        AWESOME,
        FLAWLESS,
        PERFECT,
        GODLIKE
    }
}