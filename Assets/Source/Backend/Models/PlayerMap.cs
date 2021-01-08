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
        
        public List<PlayerMapTile> tiles;
        
        // transient
        public DateTime ResetTime;
        public CancellationTokenSource CancellationTokenSource;
        public int visibleMinX = int.MaxValue;
        public int visibleMaxX = int.MinValue;
        public int visibleMinY = int.MaxValue;
        public int visibleMaxY = int.MinValue;
        
        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            if (secondsToReset != null)
            {
                ResetTime = DateTime.Now + TimeSpan.FromSeconds((int) secondsToReset);
            }
            
            tiles?.ForEach(tile =>
            {
                if (tile.discovered || tile.discoverable)
                {
                    visibleMinX = tile.posX < visibleMinX ? tile.posX : visibleMinX;
                    visibleMaxX = tile.posX > visibleMaxX ? tile.posX : visibleMaxX;
                    visibleMinY = tile.posY < visibleMinY ? tile.posY : visibleMinY;
                    visibleMaxY = tile.posY > visibleMaxY ? tile.posY : visibleMaxY;
                }
            });
        }
    }
}