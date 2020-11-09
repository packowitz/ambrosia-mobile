using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeroType
    {
        HEALER,
        DEFENDER,
        SUPPORTER,
        ATTACKER
    }
}