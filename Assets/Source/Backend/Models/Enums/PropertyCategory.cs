using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PropertyCategory
    {
        PLAYER,
        HERO,
        GEAR,
        JEWEL,
        BATTLE,
        SET,
        BUFF,
        RESOURCES,
        BUILDING,
        UPGRADE_TIME,
        UPGRADE_COST,
        VEHICLE
    }
}