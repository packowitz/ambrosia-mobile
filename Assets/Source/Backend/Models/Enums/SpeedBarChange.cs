using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpeedBarChange
    {
        NONE,
        INITIATIVE,
        REMOVE,
        HALF
    }
}