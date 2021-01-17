using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Backend.Models;
using Backend.Requests;
using Backend.Responses;
using Backend.Signal;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Backend.Services
{
    public class MissionService
    {
        [Inject] private ServerAPI serverAPI;
        private readonly SignalBus signalBus;
        public List<Mission> Missions { get; private set; }

        public MissionService(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public void StartMission(StartMissionRequest request, Action<PlayerActionResponse> onSuccess = null)
        {
            serverAPI.DoPost("/battle/mission", request, onSuccess);
        }

        [CanBeNull]
        public Mission GetMission(long mapId, int posX, int posY)
        {
            return Missions.Find(m => m.mapId == mapId && m.posX == posX && m.posY == posY);
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.missions != null)
            {
                if (Missions == null)
                {
                    Missions = data.missions;
                    SortMissions();
                    data.missions.ForEach(mission =>
                    {
                        if (!mission.missionFinished)
                        {
                            ReloadMission(mission).SuppressCancellationThrow();
                        }
                    });
                }
                else
                {
                    foreach (var mission in data.missions)
                    {
                        Update(mission);
                    }
                }
            }

            if (data.missionIdFinished != null)
            {
                var missionFinished = Missions.Find(m => m.id == data.missionIdFinished);
                if (missionFinished != null)
                {
                    Missions.Remove(missionFinished);
                    signalBus.Fire(new MissionSignal(missionFinished, true));
                }
            }
        }

        private void Update(Mission mission)
        {
            var idx = Missions.FindIndex(m => m.id == mission.id);
            if (idx >= 0)
            {
                if (Missions[idx].CancellationTokenSource != null)
                {
                    Missions[idx].CancellationTokenSource.Cancel();
                }
                Missions[idx] = mission;
            }
            else
            {
                Missions.Add(mission);
                SortMissions();
            }
            
            signalBus.Fire(new MissionSignal(mission, false));

            if (!mission.missionFinished)
            {
                ReloadMission(mission).SuppressCancellationThrow();
            }
        }

        private void SortMissions()
        {
            Missions = Missions.OrderBy(m => m.slotNumber).ToList();
        }

        private async UniTask ReloadMission(Mission mission)
        {
            mission.CancellationTokenSource = new CancellationTokenSource();
            await UniTask.Delay(
                mission.NextUpdateTime - DateTime.Now,
                cancellationToken: mission.CancellationTokenSource.Token
            );
            mission.CancellationTokenSource = null;
            Debug.Log($"Updating mission {mission.id}");
            serverAPI.DoGet<Mission>($"/battle/mission/{mission.id}", Update);
        }
    }
}