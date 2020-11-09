using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InboxMessageType
    {
        GOODS,
        DIRECT_MESSAGE,
        SERVICE,
        ANNOUNCEMENT
    }
}