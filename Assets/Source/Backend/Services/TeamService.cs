using System.Collections.Generic;
using Backend.Models;
using Backend.Responses;
using Backend.Signal;
using Zenject;

namespace Backend.Services
{
    public class TeamService
    {
        private List<Team> Teams;

        public TeamService(SignalBus signalBus)
        {
            signalBus.Subscribe<PlayerActionSignal>(signal =>
            {
                Consume(signal.Data);
            });
        }

        public Team Team(string type)
        {
            return Teams.Find(team => team.type == type);
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

        public void Update(Team team)
        {
            var idx = Teams.FindIndex(t => t.type == team.type);
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