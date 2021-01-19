using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

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
        
        public DateTime DoneTime { get; private set; }
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            DoneTime = DateTime.Now + TimeSpan.FromSeconds(secondsUntilDone);
        }
    }
}