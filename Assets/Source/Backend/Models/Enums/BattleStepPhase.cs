using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BattleStepPhase
    {
        A_PRE_TURN,
        MAIN,
        PASSIVE,
        Z_COUNTER
    }
}