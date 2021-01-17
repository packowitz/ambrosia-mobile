using System;
using Backend.Models;
using Backend.Models.Enums;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Metagame.MapScreen
{
    public class MapTileController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private TMP_Text someText;
        [SerializeField] private SpriteRenderer progress;
        [SerializeField] private SpriteRenderer progressBackground;
        [SerializeField] private SpriteRenderer progressBar;

        private Mission mission;
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        private const int LAYER_WHITE = 1;
        private const int LAYER_FAKE_3D = 2;
        private const int LAYER_STRUCTURE = 3;
        private const int LAYER_STRUCTURE_TEXT = 4;
        private const int LAYER_FIGHT = 5;
        private const int LAYER_PROGRESS = 6;
        private const int LAYER_PROGRESS_BACKGROUND = 7;
        private const int LAYER_PROGRESS_BAR = 8;
        public void SetPlayerTile(PlayerMap map, PlayerMapTile tile, MapTileConfig mapTileConfig)
        {
            PosX = tile.posX;
            PosY = tile.posY;
            
            transform.localPosition = HexGridUtils.ConvertOffsetToWorldCoordinates(new Vector2Int(tile.posX, tile.posY));
            gameObject.name = $"X{tile.posX} Y{tile.posY} {tile.type}";

            background.sprite = mapTileConfig.MapTileTypeConfig[tile.type];
            var baseLayer = 10 * tile.posY;
            background.sortingOrder = baseLayer;

            var bottomLeftEmpty = tile.discovered && BottomLeftEmpty(map, tile);
            var bottomRightEmpty = tile.discovered && BottomRightEmpty(map, tile);
            if (bottomLeftEmpty && bottomRightEmpty)
            {
                AddIcon(mapTileConfig.bottomLeftAndRightEmpty, baseLayer + LAYER_FAKE_3D, 1f, Vector2.zero);
            }
            else if (bottomLeftEmpty)
            {
                AddIcon(mapTileConfig.bottomLeftEmpty, baseLayer + LAYER_FAKE_3D, 1f, Vector2.zero);
            }
            else if (bottomRightEmpty)
            {
                AddIcon(mapTileConfig.bottomRightEmpty, baseLayer + LAYER_FAKE_3D, 1f, Vector2.zero);
            }

            if (tile.discovered)
            {
                if (tile.structure != null || (tile.fightIcon != null && (tile.fightRepeatable == true || tile.victoriousFight == false)))
                {
                    AddIcon(mapTileConfig.whiteTile, baseLayer + LAYER_WHITE, 1f, Vector2.zero, new Color{a = 0.35f, b = 1f, g = 1f, r = 1f});
                }
    
                if (tile.structure != null)
                {
                    var tileToSprite = mapTileConfig.MapStructureConfig[(MapTileStructure) tile.structure];
                    AddIcon(tileToSprite.sprite, baseLayer + LAYER_STRUCTURE, tileToSprite.scale, tileToSprite.offset);
                }
    
                if (tile.fightIcon != null && (tile.fightRepeatable == true || tile.victoriousFight == false))
                {
                    var fightToSprite = mapTileConfig.MapFightConfig[(FightIcon) tile.fightIcon];
                    AddIcon(fightToSprite.sprite, baseLayer + LAYER_FIGHT, fightToSprite.scale, fightToSprite.offset);
                }
            }
            else if (tile.discoverable)
            {
                AddIcon(mapTileConfig.whiteTile, baseLayer + LAYER_WHITE, 1f, Vector2.zero, new Color{a = 0.75f, b = 1f, g = 1f, r = 1f});
                AddIcon(mapTileConfig.discoverIcon, baseLayer + LAYER_STRUCTURE, mapTileConfig.discoverScale, mapTileConfig.discoverOffset, text: map.discoverySteamCost.ToString());
                someText.text = map.discoverySteamCost.ToString();
                someText.gameObject.SetActive(true);
                someText.gameObject.GetComponent<MeshRenderer>().sortingOrder = baseLayer + LAYER_STRUCTURE_TEXT;
                var locPos = someText.transform.localPosition;
                locPos.y += mapTileConfig.discoverOffset.y;
                someText.transform.localPosition = locPos;
            }

            progress.sortingOrder = baseLayer + LAYER_PROGRESS;
            progressBackground.sortingOrder = baseLayer + LAYER_PROGRESS_BACKGROUND;
            progressBar.sortingOrder = baseLayer + LAYER_PROGRESS_BAR;
        }

        public void SetMission(Mission ongoingMission)
        {
            mission = ongoingMission;
            progress.gameObject.SetActive(ongoingMission != null);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (mission != null)
            {
                if (mission.DoneTime < DateTime.Now)
                {
                    var progressTransform = progressBar.transform;
                    var scale = progressTransform.localScale;
                    scale.x = 1f;
                    progressTransform.localScale = scale;
                }
                else
                {
                    var secondsDone = mission.duration - Convert.ToSingle((mission.DoneTime - DateTime.Now).TotalSeconds);
                    
                    var progressTransform = progressBar.transform;
                    var scale = progressTransform.localScale;
                    scale.x = secondsDone <= 0f ? 0f : secondsDone / mission.duration;
                    progressTransform.localScale = scale;
                }
            }
        }

        private void AddIcon(Sprite sprite, int layer, float scale, Vector2 offset, Color? color = null, [CanBeNull] string text = null)
        {
            var iconObject = new GameObject(sprite.name);
            iconObject.transform.parent = transform;
            iconObject.transform.localPosition = offset;

            var spriteRenderer = iconObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            if (color != null)
            {
                spriteRenderer.color = (Color) color;
            }
            spriteRenderer.sortingOrder = layer;

            iconObject.transform.localScale = new Vector3(scale, scale, 1);

            if (text != null)
            {
                var textObject = new GameObject($"{sprite.name} text");
                textObject.transform.parent = iconObject.transform;
                textObject.transform.localPosition = offset;

                var textRenderer = textObject.AddComponent<TextMeshProUGUI>();
                textRenderer.text = text;
            }
        }

        private static bool BottomLeftEmpty(PlayerMap map, PlayerMapTile tile)
        {
            var posY = tile.posY + 1;
            var posX = tile.posY % 2 == 0 ? tile.posX - 1 : tile.posX;
            var neighbourTile = map.tiles.Find(t => t.posY == posY && t.posX == posX);
            return neighbourTile == null || neighbourTile.type == MapTileType.NONE;
        }

        private static bool BottomRightEmpty(PlayerMap map, PlayerMapTile tile)
        {
            var posY = tile.posY + 1;
            var posX = tile.posY % 2 == 0 ? tile.posX : tile.posX + 1;
            var neighbourTile = map.tiles.Find(t => t.posY == posY && t.posX == posX);
            return neighbourTile == null || neighbourTile.type == MapTileType.NONE;
        }
    }
}