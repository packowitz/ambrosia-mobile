using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeroPosition
    {
        NONE,
        HERO1,
        HERO2,
        HERO3,
        HERO4,
        OPP1,
        OPP2,
        OPP3,
        OPP4
    }
}