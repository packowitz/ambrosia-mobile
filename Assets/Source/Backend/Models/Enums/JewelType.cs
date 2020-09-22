using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum JewelType
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
        STONE_SKIN,
        VITAL_AURA,
        POWER_FIST,
        BUFFS_BLESSING,
        BERSERKERS_AXE,
        MYTHICAL_MIRROR,
        WARHORN,
        REVERSED_REALITY,
        TERRIBLE_FATE
    }
}