using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MapTileStructure
    {
        ACADEMY,
        ARENA,
        BAZAAR,
        FORGE,
        GARAGE,
        JEWELRY,
        LABORATORY,

        ENTRANCE,
        EXIT,
        HOUSE_ENTRANCE,
        LADDER,
        PORTAL,
        TUBE,
        WELL,

        DARK_CHEST,
        EPIC_CHEST,
        GOLDEN_CHEST,
        MYTHICAL_CHEST,
        SIMPLE_CHEST,
        VIOLET_CHEST,
        WOODEN_CHEST,
        WOODEN_CRATE
    }
}