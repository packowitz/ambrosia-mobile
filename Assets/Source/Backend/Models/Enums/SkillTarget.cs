using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillTarget
    {
        OPPONENT,
        SELF,
        ALL_OWN,
        OPP_IGNORE_TAUNT,
        DEAD
    }
}