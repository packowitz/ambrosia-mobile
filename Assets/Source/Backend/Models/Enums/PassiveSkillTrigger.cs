using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PassiveSkillTrigger
    {
        STAT_CALC,
        START_STAGE_BUFFING,
        START_STAGE_DEBUFFING,
        START_STAGE_DAMAGING,
        OWN_HEALTH_UNDER,
        ALLY_HEALTH_UNDER,
        ALLY_DIED,
        SELF_DIED,
        ANY_OPP_DIED,
        KILLED_OPP,
        ALLY_DEBUFF,
        SELF_DEBUFF,
        OPP_BUFF
    }
}