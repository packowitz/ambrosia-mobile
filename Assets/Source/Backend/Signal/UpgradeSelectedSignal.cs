using Backend.Models.Enums;

namespace Backend.Signal
{
    public class UpgradeSelectedSignal
    {
        public BuildingType? BuildingType { get; private set; }

        public UpgradeSelectedSignal(BuildingType buildingType)
        {
            BuildingType = buildingType;
        }
    }
}