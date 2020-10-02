using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class MissionService
    {
        public List<Mission> Missions { get; private set; }

        public MissionService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.missions != null)
            {
                if (Missions == null)
                {
                    Missions = data.missions;
                    SortMissions();
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
                var idx = Missions.FindIndex(m => m.id == data.missionIdFinished);
                if (idx >= 0)
                {
                    Missions.RemoveAt(idx);
                }
            }
        }

        private void Update(Mission mission)
        {
            var idx = Missions.FindIndex(m => m.id == mission.id);
            if (idx >= 0)
            {
                Missions[idx] = mission;
            }
            else
            {
                Missions.Add(mission);
                SortMissions();
            }
        }

        private void SortMissions()
        {
            Missions = Missions.OrderBy(m => m.slotNumber).ToList();
        }
    }
}