using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Buff
    {
        ARMOR_BUFF,
        COUNTERATTACK,
        CRIT_BUFF,
        CRIT_RESIST_BUFF,
        CRIT_MULT_BUFF,
        DAMAGE_REDUCTION_BUFF,
        DEXTERITY_BUFF,
        DEATHSHIELD,
        DODGE_BUFF,
        HASTE,
        HEAL_OVER_TIME,
        RESISTANCE_BUFF,
        SHIELD,
        STRENGTH_BUFF,
        TAUNT_BUFF,

        CONFUSE,
        CRIT_DEBUFF,
        CRIT_MULT_DEBUFF,
        DEXTERITY_DEBUFF,
        DAMAGE_OVER_TIME,
        HEAL_BLOCK,
        MARKED,
        RESISTANCE_DEBUFF,
        SLOW,
        STUN,
        WEAK
    }
}