using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Color
    {
        NEUTRAL,
        RED,
        GREEN,
        BLUE
    }
}