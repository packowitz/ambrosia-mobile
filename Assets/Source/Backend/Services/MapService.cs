using System;
using System.Collections.Generic;
using System.Threading;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Requests;
using Backend.Responses;
using Backend.Signal;
using Cysharp.Threading.Tasks;
using UnityEngine;
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
            var closeToReset = PlayerMaps.Find(map => map.type == MapType.MINE && map.secondsToReset != null && map.unvisited && map.ResetTime < oneDayAhead);
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

        public void DiscoverTile(long mapId, PlayerMapTile tile, Action<PlayerActionResponse> onSuccess = null)
        {
            var request = new DiscoverTileRequest {mapId = mapId, posX = tile.posX, posY = tile.posY};
            serverAPI.DoPost("/map/discover", request, onSuccess);
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
                if (PlayerMaps[idx].CancellationTokenSource != null)
                {
                    PlayerMaps[idx].CancellationTokenSource.Cancel();
                }
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

            if (playerMap.secondsToReset != null)
            {
                ReloadMap(playerMap).SuppressCancellationThrow();
            }
        }
        
        private async UniTask ReloadMap(PlayerMap playerMap)
        {
            playerMap.CancellationTokenSource = new CancellationTokenSource();
            await UniTask.Delay(
                playerMap.ResetTime - DateTime.Now,
                cancellationToken: playerMap.CancellationTokenSource.Token
            );
            playerMap.CancellationTokenSource = null;
            Debug.Log($"Updating mission {playerMap.mapId}");
            serverAPI.DoGet<PlayerMap>($"/map/{playerMap.mapId}", data => Update(data, data.mapId == CurrentPlayerMap.mapId));
        }
    }
    
    
}