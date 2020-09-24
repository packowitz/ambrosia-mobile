using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GenomeType
    {
        SIMPLE_GENOME,
        COMMON_GENOME,
        UNCOMMON_GENOME,
        RARE_GENOME,
        EPIC_GENOME
    }
}