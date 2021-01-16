using System;
using System.Collections.Generic;
using Backend.Models;
using Zenject;

namespace Backend.Services
{
    public class FightService
    {
        [Inject] private ServerAPI serverAPI;

        private Dictionary<long, FightResolved> cache = new Dictionary<long, FightResolved>();

        public void GetFight(long fightId, Action<FightResolved> callback)
        {
            if (cache.ContainsKey(fightId))
            {
                callback.Invoke(cache[fightId]);
            }
            else
            {
                LoadFight(fightId, callback);
            }
        }

        private void LoadFight(long fightId, Action<FightResolved> onSuccess)
        {
            serverAPI.DoGet<FightResolved>($"/fight/{fightId}", data =>
            {
                cache.Add(data.id, data);
                onSuccess.Invoke(data);
            });
        }
        
        
    }
}