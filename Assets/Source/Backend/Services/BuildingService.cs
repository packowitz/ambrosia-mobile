using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Signal;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Zenject;

namespace Backend.Services
{
    public class BuildingService
    {
        private SignalBus signalBus;
        
        private readonly Dictionary<BuildingType, Building> buildings = new Dictionary<BuildingType, Building>();
        
        public BuildingService(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                if (signal.Data.buildings != null)
                {
                    foreach (var building in signal.Data.buildings)
                    {
                        buildings[building.type] = building;
                    }
                    SendSignal();
                }
            });
        }

        public Building Building(BuildingType type)
        {
            return buildings.ContainsKey(type) ? buildings[type] : null;
        }

        private async void SendSignal()
        {
            await UniTask.Delay(TimeSpan.FromMilliseconds(50));
            signalBus.Fire<BuildingSignal>();
        }
    }
}