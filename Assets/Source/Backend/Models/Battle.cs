using System;
using System.Collections.Generic;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class Battle
    {
        public long id;
        public BattleType type;
        public BattleStatus status;
        public long previousBattleId;
        public long nextBattleId;
        public Fight fight;
        public int fightStage;
        public long? mapId;
        public int? mapPosX;
        public int? mapPosY;
        public long playerId;
        public string playerName;
        public long? opponentId;
        public string opponentName;
        public HeroPosition activeHero;
        public int turnsDone;
        public BattleHero hero1;
        public BattleHero hero2;
        public BattleHero hero3;
        public BattleHero hero4;
        public BattleHero oppHero1;
        public BattleHero oppHero2;
        public BattleHero oppHero3;
        public BattleHero oppHero4;
        public List<BattleStep> steps;
    }
}