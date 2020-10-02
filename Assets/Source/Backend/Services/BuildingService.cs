using System.Collections.Generic;
using Backend.Models;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class BuildingService
    {
        private Dictionary<BuildingType, Building> buildings = new Dictionary<BuildingType, Building>();
        
        public BuildingService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.buildings != null)
                {
                    foreach (var building in signal.Data.buildings)
                    {
                        buildings[building.type] = building;
                    }
                }
            });
        }

        public Building Building(BuildingType type)
        {
            return buildings[type];
        }
    }
}