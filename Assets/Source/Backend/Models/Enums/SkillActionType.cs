using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillActionType
    {
        ADD_BASE_DMG,
        DEAL_DAMAGE,
        BUFF,
        DEBUFF,
        SPEEDBAR,
        HEAL,
        PASSIVE_STAT,
        SPECIAL
    }
}