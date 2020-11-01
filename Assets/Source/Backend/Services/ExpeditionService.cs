using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Backend.Services
{
    public class ExpeditionService
    {
        private List<Expedition> expeditions;
        private List<PlayerExpedition> playerExpeditions;

        [Inject] private ServerAPI serverAPI;

        public ExpeditionService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
            signalBus.Subscribe<ExpeditionLevelSignal>(ReloadExpeditions);
            ScheduledExpeditionReload();
        }

        public IEnumerable<PlayerExpedition> PlayerExpeditions()
        {
            return playerExpeditions.Where(e => !e.completed);
        }

        public List<Expedition> AvailableExpeditions()
        {
            return expeditions.Where(e => playerExpeditions.FindIndex(p => p.expeditionId == e.id) == -1).ToList();
        }

        private async void ScheduledExpeditionReload()
        {
            await UniTask.Delay(TimeSpan.FromMinutes(5));
            ReloadExpeditions();
            ScheduledExpeditionReload();
        }

        private void ReloadExpeditions()
        {
            Debug.Log("Refreshing expeditions");
            serverAPI.DoGet<List<Expedition>>("/expedition/active", data =>
            {
                expeditions = data;
            });
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.expeditions != null)
            {
                expeditions = data.expeditions;
            }

            if (data.playerExpeditions != null)
            {
                if (playerExpeditions == null)
                {
                    playerExpeditions = data.playerExpeditions;
                }
                else
                {
                    foreach (var playerExpedition in data.playerExpeditions)
                    {
                        Update(playerExpedition);
                    }
                }
            }

            if (data.playerExpeditionCancelled != null)
            {
                var idx = playerExpeditions.FindIndex(e => e.id == data.playerExpeditionCancelled);
                if (idx >= 0)
                {
                    playerExpeditions.RemoveAt(idx);
                }
            }
        }

        private void Update(PlayerExpedition playerExpedition)
        {
            var idx = playerExpeditions.FindIndex(e => e.id == playerExpedition.id);
            if (idx >= 0)
            {
                playerExpeditions[idx] = playerExpedition;
            }
            else
            {
                playerExpeditions.Add(playerExpedition);
            }
        }
    }
}