using System;
using System.Collections.Generic;

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
    }
}