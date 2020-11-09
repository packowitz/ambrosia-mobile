using System;
using Backend.Models.Enums;
using Backend.Models.Enums.ObjectShape;

namespace Backend.Models
{
    [Serializable]
    public class Upgrade
    {
        public long id;
        public long playerId;
        public int position;
        public BuildingType buildingType;
        public long? vehicleId;
        public long? vehiclePartId;
        public long? gearId;
        public Modification gearModification;
        public JewelTypeObj jewelType;
        public int? jewelLevel;
        public bool finished;
        public bool inProgress;
        public int duration;
        public int origDuration;
        public int secondsUntilDone;

        // transient
        public bool updating;
        public bool updateFailed;
    }
}