using System;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class Building
    {
        public long id;
        public long playerId;
        public BuildingType type;
        public int level;
        public bool upgradeTriggered;
    }
}