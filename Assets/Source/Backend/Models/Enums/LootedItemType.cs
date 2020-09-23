using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LootedItemType
    {
        RESOURCE,
        HERO,
        GEAR,
        JEWEL,
        VEHICLE,
        VEHICLE_PART,
        PROGRESS
    }
}