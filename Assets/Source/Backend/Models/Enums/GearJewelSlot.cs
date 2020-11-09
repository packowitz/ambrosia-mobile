using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GearJewelSlot
    {
        STRENGTH,
        HP,
        ARMOR,
        BUFFING,
        SPEED,
        CRIT,
        SPECIAL
    }
}