using System.Collections.Generic;
using Backend.Models;
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
        private List<MapTileController> drawnTiles = new List<MapTileController>();

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
            drawnTiles.ForEach(drawnTile =>
            {
                drawnTile.Remove();
            });
            drawnTiles.Clear();
            var config = configsProvider.Get<MapTileConfig>();
            currentMap.tiles.ForEach(tile =>
            {
                if (tile.discovered || tile.discoverable)
                {
                    var tileView = Instantiate(mapTilePrefab, transform);
                    tileView.SetPlayerTile(currentMap, tile, config);
                    drawnTiles.Add(tileView);
                }
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
    }
}