using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum LootItemType
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