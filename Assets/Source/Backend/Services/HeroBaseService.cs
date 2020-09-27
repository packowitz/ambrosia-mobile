using System;
using Backend.Models;
using UnityEngine;

namespace Backend.Services
{
    public class HeroBaseService
    {
        private readonly ServerAPI serverAPI;

        private HeroBase[] baseHeroes;

        public bool HeroesInitialized => baseHeroes != null && baseHeroes.Length > 0;

        public HeroBaseService(ServerAPI serverAPI)
        {
            this.serverAPI = serverAPI;
        }

        public void LoadBaseHeroes()
        {
            serverAPI.DoGet<HeroBase[]>("/hero_base", data =>
            {
                baseHeroes = data;
                Debug.Log($"Loaded {baseHeroes.Length} base heroes");
            });
        }

        public HeroBase GetHeroBase(long id)
        {
            return Array.Find(baseHeroes, hero => hero.id == id);
        }
    }
}