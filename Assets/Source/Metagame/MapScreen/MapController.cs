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
        [SerializeField] private ErrorPopupController errorPopupPrefab;
        [SerializeField] private LootedController lootedPrefab;
        [SerializeField] private StartFightController startFightPrefab;
        [SerializeField] private MissionDetailController missionDetailPrefab;

        [Inject] private MapService mapService;
        [Inject] private ResourcesService resourcesService;
        [Inject] private MissionService missionService;
        [Inject] private ConfigsProvider configsProvider;
        [Inject] private SignalBus signalBus;
        [Inject] private PopupCanvasController popupCanvasController;

        private PlayerMap currentMap;
        private readonly List<MapTileController> drawnTiles = new List<MapTileController>();

        private void Start()
        {
            currentMap = mapService.CurrentPlayerMap;
            UpdateMap();
            RecenterCamera();
            signalBus.Subscribe<CurrentMapSignal>(ConsumeCurrentMapSignal);
            signalBus.Subscribe<MissionSignal>(ConsumeMissionSignal);
        }

        private void OnDestroy()
        {
            signalBus.TryUnsubscribe<CurrentMapSignal>(ConsumeCurrentMapSignal);
            signalBus.TryUnsubscribe<MissionSignal>(ConsumeMissionSignal);
        }

        private void ConsumeMissionSignal(MissionSignal signal)
        {
            if (signal.Data.mapId == currentMap.mapId)
            {
                drawnTiles.ForEach(tile =>
                {
                    if (tile.PosX == signal.Data.posX && tile.PosY == signal.Data.posY)
                    {
                        tile.SetMission(signal.Finished ? null : missionService.GetMission(currentMap.mapId, tile.PosX, tile.PosY));
                    }
                });
            }
        }

        private void ConsumeCurrentMapSignal(CurrentMapSignal signal)
        {
            var mapChanged = signal.CurrentMap.mapId != currentMap.mapId;
            currentMap = signal.CurrentMap;
            UpdateMap();
            if (mapChanged)
            {
                RecenterCamera();
            }
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
                    HandleTileTap(tile);
                }
            }
        }

        private void HandleTileTap(PlayerMapTile tile)
        {
            if (tile.discoverable)
            {
                if (resourcesService.EnoughResources(ResourceType.STEAM, currentMap.discoverySteamCost))
                {
                     mapService.DiscoverTile(currentMap.mapId, tile);
                }
                else
                {
                    var error = popupCanvasController.OpenPopup(errorPopupPrefab);
                    error.SetError("Insufficient resources", "Not enough steam to discover map tile");
                }

                return;
            }

            if (tile.discovered)
            {
                if (tile.fightId != null && (tile.victoriousFight == false || tile.fightRepeatable == true))
                {
                    var mission = missionService.GetMission(currentMap.mapId, tile.posX, tile.posY);
                    if (mission != null)
                    {
                        var missionPopup = popupCanvasController.OpenPopup(missionDetailPrefab);
                        missionPopup.SetMission(mission);
                    }
                    else
                    {
                        var fightPopup = popupCanvasController.OpenPopup(startFightPrefab);
                        fightPopup.SetMapTile(currentMap, tile);
                    }
                }
                else if (tile.structure != null)
                {
                    if (tile.portalToMapId != null)
                    {
                        mapService.ChangeMapTo((long) tile.portalToMapId);
                    }
                    else if (tile.buildingType != null)
                    {
                        Debug.Log($"goto building {tile.buildingType}");
                    }
                    else if (!tile.chestOpened)
                    {
                        mapService.OpenChest(currentMap.mapId, tile, data =>
                        {
                            if (data.looted != null)
                            {
                                var lootedPopup = popupCanvasController.OpenPopup(lootedPrefab);
                                lootedPopup.SetLooted(data.looted);
                            }
                        });
                    }
                }
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
                    tileView.SetMission(missionService.GetMission(currentMap.mapId, tile.posX, tile.posY));
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