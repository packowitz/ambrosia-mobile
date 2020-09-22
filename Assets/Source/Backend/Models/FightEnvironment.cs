using System;

namespace Backend.Models
{
    [Serializable]
    public class FightEnvironment
    {
        public long id;
        public string name;
        public bool defaultEnvironment;
        public int playerHealingDec;
        public bool playerHotBlocked;
        public int playerDotDmgInc;
        public int oppDotDmgDec;
        public int playerGreenDmgInc;
        public int playerRedDmgInc;
        public int playerBlueDmgInc;
        public int oppGreenDmgDec;
        public int oppRedDmgDec;
        public int oppBlueDmgDec;
        public int playerSpeedBarSlowed;
        public int oppSpeedBarFastened;
    }
}