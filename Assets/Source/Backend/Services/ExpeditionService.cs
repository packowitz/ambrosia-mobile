using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class ExpeditionService
    {
        private List<Expedition> expeditions;
        private List<PlayerExpedition> playerExpeditions;

        public ExpeditionService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public List<PlayerExpedition> PlayerExpeditions()
        {
            return playerExpeditions.Where(e => !e.completed).ToList();
        }

        public List<Expedition> AvailableExpeditions()
        {
            return expeditions.Where(e => playerExpeditions.FindIndex(p => p.expeditionId == e.id) == -1).ToList();
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