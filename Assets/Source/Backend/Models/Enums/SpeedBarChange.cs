using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
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