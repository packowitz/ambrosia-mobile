using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models
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