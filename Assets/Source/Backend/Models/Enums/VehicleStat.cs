using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VehicleStat
    {
        OFFLINE_BATTLE_SPEED,
        BATTLE_XP,
        BATTLE_ASC_POINTS,
        BATTLE_RESSOURCE_LOOT,
        ARMOR,
        DEXTERITY,
        DODGE,
        INITIATIVE,
        REFLECTION,
        RESISTANCE,
        STRENGTH,
        CRIT_CHANCE,
        STAGE_HEAL,
        STAGE_ARMOR,
    }
}