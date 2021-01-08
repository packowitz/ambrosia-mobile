using System.Collections.Generic;
using Backend.Models;
using Backend.Models.Enums;
using Backend.Services;
using Backend.Signal;
using Configs;
using Lean.Touch;
using UnityEngine;
using Zenject;

namespace Metagame.MapScreen
{
    public class MapController : MonoBehaviour
    {
        [SerializeField] private MapTileController mapTilePrefab;

        [Inject] private MapService mapService;
        [Inject] private ResourcesService resourcesService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private SignalBus signalBus;

        private PlayerMap currentMap;
        private readonly List<MapTileController> drawnTiles = new List<MapTileController>();

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

        private void OnEnable()
        {
            LeanTouch.OnFingerTap += HandleTap;
        }

        private void OnDisable()
        {
            LeanTouch.OnFingerTap -= HandleTap;
        }

        private void HandleTap(LeanFinger finger)
        {
            if (!finger.IsOverGui)
            {
                var worldPosition = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
                var tileCoords = HexGridUtils.ConvertWorldToOffsetCoordinates(worldPosition);
                var tile = currentMap.tiles.Find(t => t.posX == tileCoords.x && t.posY == tileCoords.y);
                if (tile != null)
                {
                    Debug.Log("Tabbed tile " + tile.posX + "-" + tile.posY);
                    HandleTileTap(tile);
                }
            }
        }

        private void HandleTileTap(PlayerMapTile tile)
        {
            if (tile.discoverable)
            {
                Debug.Log("Discover map tile " + tile.posX + "-" + tile.posY);
                if (resourcesService.EnoughResources(ResourceType.STEAM, currentMap.discoverySteamCost))
                {
                     mapService.DiscoverTile(currentMap.mapId, tile);
                }
                else
                {
                    // show error
                }

                return;
            }

            if (tile.discovered)
            {
                
            }
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