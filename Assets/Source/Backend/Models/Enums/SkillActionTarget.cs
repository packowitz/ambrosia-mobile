using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillActionTarget
    {
        TARGET,
        ALL_OPP,
        ALL_OTHER_OPP,
        RANDOM_OPP,
        RANDOM_OTHER_OPP,
        RANDOM_OPP_WITH_BUFF,
        SELF,
        ALL_ALLIES,
        ALL_OTHER_ALLIES,
        RANDOM_ALLY,
        RANDOM_OTHER_ALLY,
        ALLY_LOWEST_HP
    }
}