using System;

namespace Backend.Models
{
    [Serializable]
    public class Team
    {
        public long id;
        public string type;
        public long hero1Id;
        public long hero2Id;
        public long hero3Id;
        public long hero4Id;
        public long vehicleId;
    }
}