using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillActionEffect
    {
        STRENGTH_SCALING,
        ARMOR_SCALING,
        ARMOR_MAX_SCALING,
        HP_SCALING,
        HP_MAX_SCALING,
        DEXTERITY_SCALING,
        RESISTANCE_SCALING,
        HERO_LVL_SCALING,
        DMG_MULTIPLIER,
        FIXED_DMG,

        DEAL_PERCENTAGE,

        ARMOR_BUFF,
        COUNTERATTACK_BUFF,
        CRIT_BUFF,
        CRIT_RESIST_BUFF,
        CRIT_MULT_BUFF,
        DAMAGE_REDUCTION_BUFF,
        DEATHSHIELD_BUFF,
        DEXTERITY_BUFF,
        DODGE_BUFF,
        HASTE_BUFF,
        HOT_BUFF,
        RESISTANCE_BUFF,
        STRENGTH_BUFF,
        TAUNT_BUFF,

        CONFUSE_DEBUFF,
        CRIT_DEBUFF,
        CRIT_MULT_DEBUFF,
        DEXTERITY_DEBUFF,
        DOT_DEBUFF,
        MARKED_DEBUFF,
        HEAL_BLOCK_DEBUFF,
        RESISTANCE_DEBUFF,
        SLOW_DEBUFF,
        STUN_DEBUFF,
        WEAK_DEBUFF,

        PERCENTAGE,
        PERCENTAGE_MAX,

        TARGET_MAX_HP,
        OWN_MAX_HP,

        STRENGTH_PASSIVE,
        ARMOR_PASSIVE,
        CRIT_PASSIVE,
        CRIT_MULT_PASSIVE,
        DEXTERITY_MULT_PASSIVE,
        RESISTANCE_PASSIVE,
        LIFESTEAL_PASSIVE,
        COUNTER_PASSIVE,
        REFLECT_PASSIVE,
        DODGE_PASSIVE,
        SPEED_PASSIVE,
        ARMOR_PIERCING_PASSIVE,
        ARMOR_EXTRA_DMG_PASSIVE,
        HEALTH_EXTRA_DMG_PASSIVE,
        RED_DMG_INC_PASSIVE,
        GREEN_DMG_INC_PASSIVE,
        BLUE_DMG_INC_PASSIVE,
        HEALING_INC_PASSIVE,
        SUPER_CRIT_PASSIVE,
        BUFF_INTENSITY_PASSIVE,
        DEBUFF_INTENSITY_PASSIVE,
        BUFF_DURATION_PASSIVE,
        DEBUFF_DURATION_PASSIVE,
        HEAL_PER_TURN_PASSIVE,
        DMG_PER_TURN_PASSIVE,
        DAMAGE_REDUCTION_PASSIVE,

        COOLDOWN,
        INIT_COOLDOWN,
        RESURRECT,
        REMOVE_BUFF,
        REMOVE_ALL_BUFFS,
        REMOVE_DEBUFF,
        REMOVE_ALL_DEBUFFS,
        SMALL_SHIELD,
        MEDIUM_SHIELD,
        LARGE_SHIELD
    }
}