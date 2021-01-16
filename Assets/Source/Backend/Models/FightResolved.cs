using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class FightResolved
    {
        public long id;
        public string name;
        public string description;
        public Player serviceAccount;
        public ResourceType resourceType;
        public int costs;
        public int xp;
        public int level;
        public int ascPoints;
        public int travelDuration;
        public int timePerTurn;
        public int maxTurnsPerStage;
        public List<FightStageResolved> stages;
        public FightStageConfig stageConfig;
        public FightEnvironment environment;
        public LootBox lootBox;
    }
}