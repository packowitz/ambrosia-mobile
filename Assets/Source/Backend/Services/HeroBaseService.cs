using System.Collections.Generic;
using Backend.Models;
using UnityEngine;

namespace Backend.Services
{
    public class HeroBaseService
    {
        private readonly ServerAPI serverAPI;

        private List<HeroBase> baseHeroes;

        public bool HeroesInitialized => baseHeroes != null && baseHeroes.Count > 0;

        public HeroBaseService(ServerAPI serverAPI)
        {
            this.serverAPI = serverAPI;
        }

        public void LoadBaseHeroes()
        {
            serverAPI.DoGet<List<HeroBase>>("/hero_base", data =>
            {
                baseHeroes = data;
                Debug.Log($"Loaded {baseHeroes.Count} base heroes");
            });
        }

        public HeroBase GetHeroBase(long id)
        {
            return baseHeroes.Find(hero => hero.id == id);
        }
    }
}