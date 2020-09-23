using System;

namespace Backend.Models
{
    [Serializable]
    public class VehiclePart
    {
        public long id;
        public long playerId;
        public long? equippedTo;
        public PartType type;
        public PartQuality quality;
        public int level;
        public bool upgradeTriggered;
    }
}