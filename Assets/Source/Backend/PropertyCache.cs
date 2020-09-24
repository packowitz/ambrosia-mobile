using System;
using Backend.Models;

namespace Backend
{
    [Serializable]
    public class PropertyCache
    {
        public string type;
        public int version;
        public Property[] props;
    }
}