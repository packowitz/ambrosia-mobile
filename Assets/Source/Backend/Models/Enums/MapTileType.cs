using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MapTileType
    {
        NONE,
        ASH,
        DESERT,
        SAND,
        RED_SAND,
        GREEN_SAND,
        MUD,
        ROCK,
        BROWN_ROCK,
        RED_ROCK,
        GREEN_STONE,
        GRASS,
        MOSS,
        FOREST,
        SNOWCOVERED_GRASS,
        SNOW,
        SNOW_MUD,
        LAVA,
        MAGMA,
        WATER
    }
}