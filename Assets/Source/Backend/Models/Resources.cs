using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Backend.Models
{
    [Serializable]
    public class Resources
    {
        public long playerId;
        public int resourceGenerationSpeed;
        public int steam;
        public int steamMax;
        public int steamProduceIn;
        public DateTime SteamProductionTime { get; private set; }
        public int cogwheels;
        public int cogwheelsMax;
        public int cogwheelsProduceIn;
        public DateTime CogwheelsProductionTime { get; private set; }
        public int tokens;
        public int tokensMax;
        public int tokensProduceIn;
        public DateTime TokensProductionTime { get; private set; }
        public int premiumSteam;
        public int premiumSteamMax;
        public int premiumCogwheels;
        public int premiumCogwheelsMax;
        public int premiumTokens;
        public int premiumTokensMax;
        public int rubies;
        public int coins;
        public int metal;
        public int metalMax;
        public int iron;
        public int ironMax;
        public int steel;
        public int steelMax;
        public int wood;
        public int woodMax;
        public int brownCoal;
        public int brownCoalMax;
        public int blackCoal;
        public int blackCoalMax;
        public int simpleGenome;
        public int commonGenome;
        public int uncommonGenome;
        public int rareGenome;
        public int epicGenome;
        public int woodenKeys;
        public int bronzeKeys;
        public int silverKeys;
        public int goldenKeys;
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            SteamProductionTime = DateTime.Now + TimeSpan.FromSeconds(steamProduceIn);
            CogwheelsProductionTime = DateTime.Now + TimeSpan.FromSeconds(cogwheelsProduceIn);
            TokensProductionTime = DateTime.Now + TimeSpan.FromSeconds(tokensProduceIn);
        }
    }
}