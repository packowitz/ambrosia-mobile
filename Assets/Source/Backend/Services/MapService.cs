using System;
using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class MapService
    {
        public PlayerMap CurrentPlayerMap { get; private set; }
        public List<PlayerMap> PlayerMaps{ get; private set; }

        [Inject] private ServerAPI serverAPI;
        private readonly SignalBus signalBus;

        public MapService(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public bool HasUnvisitedMineCloseToReset()
        {
            var oneDayAhead = DateTime.Now + TimeSpan.FromHours(24);
            var closeToReset = PlayerMaps.Find(map => map.type == MapType.MINE && map.ResetTime != null && map.unvisited && map.ResetTime < oneDayAhead);
            return closeToReset != null;
        }

        public void ChangeMapTo(long mapId, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost($"/map/{mapId}/current", null, onSuccess);
        }

        public void ToggleFavorite(Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost($"/map/{CurrentPlayerMap.mapId}/favorite/{!CurrentPlayerMap.favorite}", null, onSuccess);
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