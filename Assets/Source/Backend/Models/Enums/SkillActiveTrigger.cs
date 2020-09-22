using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SkillActiveTrigger
    {
        ALWAYS,
        ASCENDED,
        NPC_ONLY
    }
}