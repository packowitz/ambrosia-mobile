using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GearSet
    {
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