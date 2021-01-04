using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class MapService
    {
        public PlayerMap CurrentPlayerMap { get; private set; }
        public List<PlayerMap> PlayerMaps{ get; private set; }

        private readonly SignalBus signalBus;

        public MapService(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.playerMaps != null)
            {
                if (PlayerMaps == null)
                {
                    PlayerMaps = data.playerMaps;
                }
                else
                {
                    foreach (var playerMap in data.playerMaps)
                    {
                        Update(playerMap, false);
                    }
                }
            }

            if (data.currentMap != null)
            {
                Update(data.currentMap, true);
            }
        }

        private void Update(PlayerMap playerMap, bool currentMap)
        {
            var idx = PlayerMaps.FindIndex(m => m.mapId == playerMap.mapId);
            if (idx >= 0)
            {
                PlayerMaps[idx] = playerMap;
            }
            else
            {
                PlayerMaps.Add(playerMap);
            }

            if (currentMap)
            {
                CurrentPlayerMap = playerMap;
                signalBus.Fire(new CurrentMapSignal(CurrentPlayerMap));
            }
        }
    }
    
    
}