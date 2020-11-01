using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class TeamService
    {
        public List<Team> Teams { get; private set; }

        public TeamService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        private void Consume(PlayerActionResponse data)
        {
            if (data.teams != null)
            {
                if (Teams == null)
                {
                    Teams = data.teams;
                }
                else
                {
                    foreach (var team in data.teams)
                    {
                        Update(team);
                    }
                }
            }
        }

        private void Update(Team team)
        {
            var idx = Teams.FindIndex(t => t.id == team.id);
            if (idx >= 0)
            {
                Teams[idx] = team;
            }
            else
            {
                Teams.Add(team);
            }
        }
    }
}