using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class Fight
    {
        public long id;
        public string name;
        public string description;
        public long serviceAccountId;
        public ResourceType resourceType;
        public int costs;
        public int xp;
        public int level;
        public int ascPoints;
        public int travelDuration;
        public int timePerTurn;
        public int maxTurnsPerStage;
        public FightStageConfig stageConfig;
        public FightEnvironment environment;
        public LootBox lootBox;
        public List<FightStage> stages;
    }
}