using System;

namespace Backend.Models
{
    [Serializable]
    public class PlayerMapTile
    {
        public int posX;
        public int posY;
        public MapTileType type;
        public bool discovered;
        public bool chestOpened;
        public bool discoverable;
        public MapTileStructure structure;
        public FightIcon fightIcon;
        public long? fightId;
        public bool? fightRepeatable;
        public bool? victoriousFight;
        public long? portalToMapId;
        public BuildingType buildingType;
    }
}