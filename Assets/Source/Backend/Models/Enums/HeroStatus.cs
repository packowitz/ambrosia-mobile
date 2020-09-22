using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeroStatus
    {
        ALIVE,
        STUNNED,
        CONFUSED,
        DEAD
    }
}