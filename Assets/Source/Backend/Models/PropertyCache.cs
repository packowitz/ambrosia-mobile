using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;

namespace Backend
{
    [Serializable]
    public class PropertyCache
    {
        public PropertyType type;
        public int version;
        public List<Property> props;
    }
}