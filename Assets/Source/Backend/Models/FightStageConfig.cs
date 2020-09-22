using System;

namespace Backend.Models
{
    [Serializable]
    public class FightStageConfig
    {
        public long id;
        public string name;
        public bool defaultConfig;
        public bool debuffsRemoved;
        public int debuffDurationChange;
        public bool buffsRemoved;
        public int buffDurationChange;
        public int hpHealing;
        public int armorRepair;
        public SpeedBarChange speedBarChange;
    }
}