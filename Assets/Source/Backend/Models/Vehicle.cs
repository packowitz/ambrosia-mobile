using System;

namespace Backend.Models
{
    [Serializable]
    public class Vehicle
    {
        public long id;
        public long playerId;
        public long? missionId;
        public long? playerExpeditionId;
        public long baseVehicleId;
        public string name;
        public string avatar;
        public int level;
        public bool upgradeTriggered;
        public int? slot;
        public VehiclePart engine;
        public VehiclePart frame;
        public VehiclePart computer;
        public VehiclePart specialPart1;
        public VehiclePart specialPart2;
        public VehiclePart specialPart3;
    }
}