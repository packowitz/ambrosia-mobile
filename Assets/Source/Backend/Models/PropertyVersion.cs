using System;

namespace Backend.Models
{
    [Serializable]
    public class PropertyVersion
    {
        public PropertyType propertyType;
        public int version;
    }
}