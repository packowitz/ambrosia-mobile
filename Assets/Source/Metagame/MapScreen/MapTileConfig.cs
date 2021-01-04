using System;
using System.Collections.Generic;
using Backend.Models.Enums;
using UnityEngine;

namespace Metagame.MapScreen
{
    [CreateAssetMenu(fileName = "MapTileConfig", menuName = "Ambrosia/Metagame/MapScreen/MapTileConfig")]
    public class MapTileConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [Serializable]
        private class MapTileTypeToSprite
        {
            public MapTileType type;
            public Sprite sprite;
        }
        
        [Serializable]
        public class MapStructureToSprite
        {
            public MapTileStructure structure;
            public Sprite sprite;

            public float scale = 1f;
            public Vector2 offset = Vector2.zero;
        }
        
        [Serializable]
        public class MapFightToSprite
        {
            public FightIcon fightIcon;
            public Sprite sprite;

            public float scale = 1f;
            public Vector2 offset = Vector2.zero;
        }

        [SerializeField] private List<MapTileTypeToSprite> mapTiles;
        [SerializeField] public Sprite whiteTile;
        [SerializeField] public Sprite bottomLeftEmpty;
        [SerializeField] public Sprite bottomRightEmpty;
        [SerializeField] public Sprite bottomLeftAndRightEmpty;
        [SerializeField] private List<MapStructureToSprite> mapStructures;
        [SerializeField] private List<MapFightToSprite> mapFights;

        public Dictionary<MapTileType, Sprite> MapTileTypeConfig { get; private set; }
        public Dictionary<MapTileStructure, MapStructureToSprite> MapStructureConfig { get; private set; }
        public Dictionary<FightIcon, MapFightToSprite> MapFightConfig { get; private set; }

        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize()
        {
            if (mapTiles != null)
            {
                MapTileTypeConfig = new Dictionary<MapTileType, Sprite>();
                mapTiles.ForEach(tile =>
                {
                    MapTileTypeConfig.Add(tile.type, tile.sprite);
                });
            }

            if (mapStructures != null)
            {
                MapStructureConfig = new Dictionary<MapTileStructure, MapStructureToSprite>();
                mapStructures.ForEach(structure =>
                {
                    MapStructureConfig.Add(structure.structure, structure);
                });
            }

            if (mapFights != null)
            {
                MapFightConfig = new Dictionary<FightIcon, MapFightToSprite>();
                mapFights.ForEach(fight =>
                {
                    MapFightConfig.Add(fight.fightIcon, fight);
                });
            }
        }
    }
}