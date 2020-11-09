using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BattleStepActionType
    {
        DAMAGE,
        DODGED,
        HEALING,
        BUFF,
        BLOCKED,
        BUFF_CLEANED,
        DOT,
        HOT,
        DEAD,
        RESURRECTED,
        SPEEDBAR
    }
}