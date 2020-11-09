using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class PlayerExpedition
    {
        public long id;
        public long playerId;
        public long expeditionId;
        public long vehicleId;
        public long? hero1Id;
        public long? hero2Id;
        public long? hero3Id;
        public long? hero4Id;
        public bool completed;
        public string name;
        public string description;
        public int level;
        public Rarity rarity;

        public List<LootedItem> lootedItems;
        public int duration;
        public int secondsUntilDone;
        public DateTime DoneTime { get; private set; }
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            DoneTime = DateTime.Now + TimeSpan.FromSeconds(secondsUntilDone);
        }
    }
}