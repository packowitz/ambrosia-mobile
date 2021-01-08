using Backend.Services;
using UnityEngine;
using Zenject;

namespace Metagame.MapScreen
{
    public class MapDragBoundary : MonoBehaviour
    {

        [Inject] private MapService mapService;

        public float topOffset;
        public float bottomOffset;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            var ortho = mainCamera.orthographicSize;
            var cameraWorldHalfSize = new Vector3(mainCamera.aspect * ortho, ortho, 0);
            var minVector =
                HexGridUtils.ConvertOffsetToWorldCoordinates(new Vector2Int(mapService.CurrentPlayerMap.visibleMinX - 1,
                    mapService.CurrentPlayerMap.visibleMaxY + 1)) + cameraWorldHalfSize;
            minVector.y -= bottomOffset;
            var maxVector =
                HexGridUtils.ConvertOffsetToWorldCoordinates(new Vector2Int(mapService.CurrentPlayerMap.visibleMaxX + 1,
                    mapService.CurrentPlayerMap.visibleMinY - 1)) - cameraWorldHalfSize;
            maxVector.y += topOffset;
            var position = transform.position;

            if (minVector.x > maxVector.x)
            {
                var xMiddle = (mapService.CurrentPlayerMap.visibleMaxX + mapService.CurrentPlayerMap.visibleMinX) / 2;
                var cameraOffsetPosition = HexGridUtils.ConvertOffsetToWorldCoordinates(new Vector2Int(xMiddle, 0));
                minVector.x = cameraOffsetPosition.x;
                maxVector.x = cameraOffsetPosition.x;
            }

            if (minVector.y > maxVector.y)
            {
                var yMiddle = (mapService.CurrentPlayerMap.visibleMaxY + mapService.CurrentPlayerMap.visibleMinY) / 2;
                var cameraOffsetPosition = HexGridUtils.ConvertOffsetToWorldCoordinates(new Vector2Int(0, yMiddle));
                minVector.y = cameraOffsetPosition.y;
                maxVector.y = cameraOffsetPosition.y;
            }
            
            
            if (position.x < minVector.x)
            {
                position.x = minVector.x;
            }

            if (position.y < minVector.y)
            {
                position.y = minVector.y;
            }
            if (position.x > maxVector.x)
            {
                position.x = maxVector.x;
            }

            if (position.y > maxVector.y)
            {
                position.y = maxVector.y;
            }

            transform.position = position;
        }
    }
}