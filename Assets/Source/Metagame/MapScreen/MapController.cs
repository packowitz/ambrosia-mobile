using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Backend.Signal;
using Configs;
using UnityEngine;
using Zenject;

namespace Metagame.MapScreen
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private MapTileController mapTilePrefab;

        [Inject] private MapService mapService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private SignalBus signalBus;

        private PlayerMap currentMap;

        private void Start()
        {
            currentMap = mapService.CurrentPlayerMap;
            UpdateMap();
            RecenterCamera();
            signalBus.Subscribe<CurrentMapSignal>(data =>
            {
                var mapChanged = data.CurrentMap.mapId != currentMap.mapId;
                currentMap = data.CurrentMap;
                UpdateMap();
                if (mapChanged)
                {
                    RecenterCamera();
                }
            });
        }

        private void UpdateMap()
        {
            transform.DetachChildren();
            var config = configsProvider.Get<MapTileConfig>();
            currentMap.tiles.ForEach(tile =>
            {
                var tileView = Instantiate(mapTilePrefab, transform);
                tileView.SetPlayerTile(tile, config, BottomLeftEmpty(tile), BottomRightEmpty(tile));
            });
        }

        private void RecenterCamera()
        {
            var mainCamera = Camera.main;
            var cameraPosition = new Vector3(0f, 0f, mainCamera.transform.position.z);

            var xMiddle = (currentMap.maxX + currentMap.minX) / 2;
            var yBottom = currentMap.maxY;
            
            var cameraOffsetPosition = HexGridUtils.ConvertOffsetToWorldCoordinates(new Vector2Int(xMiddle, yBottom));
            cameraOffsetPosition.y += mainCamera.orthographicSize - 1.7f;
            mainCamera.transform.position = cameraPosition + cameraOffsetPosition;
        }

        private bool BottomLeftEmpty(PlayerMapTile tile)
        {
            var posY = tile.posY + 1;
            var posX = tile.posY % 2 == 0 ? tile.posX - 1 : tile.posX;
            var neighbourTile = currentMap.tiles.Find(t => t.posY == posY && t.posX == posX);
            return neighbourTile == null || neighbourTile.type == MapTileType.NONE;
        }

        private bool BottomRightEmpty(PlayerMapTile tile)
        {
            var posY = tile.posY + 1;
            var posX = tile.posY % 2 == 0 ? tile.posX : tile.posX + 1;
            var neighbourTile = currentMap.tiles.Find(t => t.posY == posY && t.posX == posX);
            return neighbourTile == null || neighbourTile.type == MapTileType.NONE;
        }
    }
}