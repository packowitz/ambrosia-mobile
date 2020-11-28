using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Backend.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TaskCategory
    {
        RESOURCE_SPENT,
        ACTIVITY,
        LABORATORY,
        BUILDER
    }
}