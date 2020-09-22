using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BuildingType
    {
        ACADEMY,
        ARENA,
        BARRACKS,
        BAZAAR,
        FORGE,
        GARAGE,
        JEWELRY,
        LABORATORY,
        STORAGE
    }
}