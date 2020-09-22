using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
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
}