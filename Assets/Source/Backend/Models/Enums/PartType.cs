using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PartType
    {
        ENGINE,
        FRAME,
        COMPUTER,
        SMOKE_BOMB,
        RAILGUN,
        SNIPER_SCOPE,
        MEDI_KIT,
        REPAIR_KIT,
        EXTRA_ARMOR,
        NIGHT_VISION,
        MAGNETIC_SHIELD,
        MISSILE_DEFENSE
    }
}