using System;
using Backend.Models;

namespace Backend
{
    [Serializable]
    public class PropertyCache
    {
        public PropertyType type;
        public int version;
        public Property[] props;
    }
}