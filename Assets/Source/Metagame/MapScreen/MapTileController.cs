using Backend.Models;
using Backend.Models.Enums;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Metagame.MapScreen
{
    public class MapTileController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer background;

        private const int LAYER_WHITE = 1;
        private const int LAYER_FAKE_3D = 2;
        private const int LAYER_STRUCTURE = 3;
        private const int LAYER_FIGHT = 4;
        public void SetPlayerTile(PlayerMapTile tile, MapTileConfig mapTileConfig, bool bottomLeftEmpty, bool bottomRightEmpty)
        {
            transform.localPosition = HexGridUtils.ConvertOffsetToWorldCoordinates(new Vector2Int(tile.posX, tile.posY));
            gameObject.name = $"X{tile.posX} Y{tile.posY} {tile.type}";

            background.sprite = mapTileConfig.MapTileTypeConfig[tile.type];
            var baseLayer = 10 * tile.posY;
            background.sortingOrder = baseLayer;

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

            if (tile.structure != null || (tile.fightIcon != null &&
                                           (tile.fightRepeatable == true || tile.victoriousFight == false)))
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

        private void AddIcon(Sprite sprite, int layer, float scale, Vector2 offset, Color? color = null)
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
        }
    }
}