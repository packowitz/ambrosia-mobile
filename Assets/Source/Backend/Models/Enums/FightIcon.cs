using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FightIcon
    {
        CYBORG,
        FIGHT,
        GARGOYLE,
        GREEN_GOBLIN,
        LIGHTNING,
        MASK,
        SHADOW,
        SKELETT,
        SWORD_SHIELD
    }
}