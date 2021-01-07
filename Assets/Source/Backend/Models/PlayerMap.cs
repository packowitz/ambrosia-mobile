using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using Backend.Models.Enums;

namespace Backend.Models
{
    [Serializable]
    public class PlayerMap
    {
        public long mapId;
        public string name;
        public MapType type;
        public string background;
        public int discoverySteamCost;
        public StoryTrigger storyTrigger;
        public bool favorite;
        public int minX;
        public int maxX;
        public int minY;
        public int maxY;
        public int? secondsToReset;
        public bool unvisited;
        
        // transient
        public DateTime ResetTime;
        public CancellationTokenSource CancellationTokenSource;
 
        public List<PlayerMapTile> tiles;
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (secondsToReset != null)
            {
                ResetTime = DateTime.Now + TimeSpan.FromSeconds((int) secondsToReset);
            }
        }
    }
}