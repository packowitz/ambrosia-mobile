using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeroStat
    {
        HP_ABS,
        HP_PERC,
        ARMOR_ABS,
        ARMOR_PERC,
        STRENGTH_ABS,
        STRENGTH_PERC,
        CRIT,
        CRIT_MULT,
        RESISTANCE,
        DEXTERITY,
        INITIATIVE,
        SPEED,
        LIFESTEAL,
        COUNTER_CHANCE,
        REFLECT,
        DODGE_CHANCE,
        ARMOR_PIERCING,
        ARMOR_EXTRA_DMG,
        HEALTH_EXTRA_DMG,
        RED_DMG_INC,
        GREEN_DMG_INC,
        BLUE_DMG_INC,
        HEALING_INC,
        SUPER_CRIT_CHANCE,
        BUFF_INTENSITY_INC,
        DEBUFF_INTENSITY_INC,
        BUFF_DURATION_INC,
        DEBUFF_DURATION_INC,
        HEAL_PER_TURN,
        DMG_PER_TURN,
        CONFUSE_CHANCE,
        DAMAGE_REDUCTION,
        CRIT_RESIST,
        BUFF_RESISTANCE,
        INIT_SPEEDBAR_GAIN
    }
}