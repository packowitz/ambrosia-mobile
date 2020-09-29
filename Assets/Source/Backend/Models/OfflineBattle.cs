using System;
using System.Collections.Generic;

namespace Backend.Models
{
    [Serializable]
    public class OfflineBattle
    {
        public long battleId;
        public bool battleFinished;
        public bool battleStarted;

        public int duration;
        public int secondsUntilDone;
        public bool battleSuccess;
        public bool cancelled;

        public List<LootedItem> lootedItems;
    }
}