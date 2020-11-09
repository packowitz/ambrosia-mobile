using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
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