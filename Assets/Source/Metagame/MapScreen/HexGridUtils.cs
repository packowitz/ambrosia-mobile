using UnityEngine;

namespace Metagame.MapScreen
{

    public static class HexGridUtils
    {
        private static Vector2 hexTileSize = new Vector2(1.92f / 2.5f, 1.92f / 2.5f);
        
        public static Vector3Int ConvertToCubeCoordinates(Vector2Int gridCoordinates)
        {
            Vector3Int hexGridCubeCoords = Vector3Int.zero;

            // formula for converting offset to cube coordinates
            hexGridCubeCoords.x = gridCoordinates.x - (gridCoordinates.y - (gridCoordinates.y & 1)) / 2;
            hexGridCubeCoords.z = gridCoordinates.y;
            hexGridCubeCoords.y = -hexGridCubeCoords.x - hexGridCubeCoords.z;

            return hexGridCubeCoords;
        }
        
        public static Vector2Int ConvertToOffsetCoordinates(Vector3Int hexGridCubeCoords)
        {
            Vector2Int gridCoords = Vector2Int.zero;

            // formula for converting cube coordinates to offset coordinates
            gridCoords.x = hexGridCubeCoords.x + (hexGridCubeCoords.z - (hexGridCubeCoords.z & 1)) / 2;
            gridCoords.y = hexGridCubeCoords.z;

            return gridCoords;
        }
        
        public static int Distance(Vector3Int positionFrom, Vector3Int positionTo)
        {
            var offset = positionTo - positionFrom;
            //the distance of the cube coord from the 0,0,0 is the maximum of the vector's x,y or z values
            return Mathf.Max(Mathf.Abs(offset.x), Mathf.Abs(offset.y), Mathf.Abs(offset.z));
        }
        
        public static int Distance(Vector2Int positionFrom, Vector2Int positionTo)
        {
            return Distance(ConvertToCubeCoordinates(positionFrom), ConvertToCubeCoordinates(positionTo));
        }
        
        public static Vector3 ConvertOffsetToWorldCoordinates(Vector2Int offsetCoordinates)
        {
            return new Vector3(
                hexTileSize.x * offsetCoordinates.x + ((offsetCoordinates.y % 2 != 0) ? hexTileSize.x / 2f : 0),
                -(hexTileSize.y * 3f / 4f) * offsetCoordinates.y,
                0
            );
        }

        public static Vector2Int ConvertWorldToOffsetCoordinates(Vector3 worldCoordinates)
        {
            Vector2Int gridCoordinates = Vector2Int.zero;

            gridCoordinates.y = Mathf.RoundToInt(-(worldCoordinates.y * 4f) / (hexTileSize.y * 3f));
            gridCoordinates.x = Mathf.RoundToInt((worldCoordinates.x - ((gridCoordinates.y % 2 != 0) ? hexTileSize.x / 2f : 0)) / hexTileSize.x);

            return gridCoordinates;
        }
    }
}