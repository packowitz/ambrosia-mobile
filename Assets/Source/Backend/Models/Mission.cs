using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;

namespace Backend.Models
{
    [Serializable]
    public class Mission
    {
        public long id;
        public long playerId;
        public Fight fight;
        public long mapId;
        public int posX;
        public int posY;
        public long vehicleId;
        public int slotNumber;
        public long? hero1Id;
        public long? hero2Id;
        public long? hero3Id;
        public long? hero4Id;
        public int totalCount;
        public int wonCount;
        public int lostCount;

        public bool lootCollected;
        public bool missionFinished;
        public int nextUpdateSeconds;
        public int duration;
        public int secondsUntilDone;
        public DateTime NextUpdateTime { get; private set; }
        public DateTime DoneTime { get; private set; }

        public List<OfflineBattle> battles;

        // transient
        public CancellationTokenSource CancellationTokenSource;
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            NextUpdateTime = DateTime.Now + TimeSpan.FromSeconds(nextUpdateSeconds);
            DoneTime = DateTime.Now + TimeSpan.FromSeconds(secondsUntilDone);
        }
    }
}