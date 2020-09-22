using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GearType
    {
        WEAPON,
        SHIELD,
        HELMET,
        ARMOR,
        GLOVES,
        BOOTS
    }
}