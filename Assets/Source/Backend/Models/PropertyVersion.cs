using System;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class PropertyVersion
    {
        public PropertyType propertyType;
        public int version;
    }
}