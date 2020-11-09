using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BattleStatus
    {
        INIT,
        PLAYER_TURN,
        OPP_TURN,
        WON,
        LOST,
        STAGE_PASSED
    }
}