using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillActionTrigger
    {
        ALWAYS,
        SKILL_LVL,
        S1_LVL,
        S2_LVL,
        S3_LVL,
        S4_LVL,
        S5_LVL,
        S6_LVL,
        S7_LVL,
        PREV_ACTION_PROCED,
        PREV_ACTION_NOT_PROCED,
        ANY_CRIT_DMG,
        DMG_OVER,
        ASC_LVL
    }
}