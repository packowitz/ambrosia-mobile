using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OddJobType
    {
        SPEND_STEAM,
        SPEND_COGWHEELS,
        SPEND_TOKENS,
        FINISH_MISSIONS,
        OPEN_CHESTS,
        DISCOVER_TILES,
        UPGRADE_BUILDING,
        UPGRADE_VEHICLE,
        UPGRADE_PARTS,
        MERGE_JEWELS,
        MODIFY_GEAR,
        BREAKDOWN_GEAR,
        FINISH_EXPEDITIONS,
        LOOT_GEAR,
        LOOT_PARTS,
        LOOT_COINS,
        LOOT_JEWELS
    }
}