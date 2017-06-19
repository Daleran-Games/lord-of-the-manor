using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public static class HexMetrics
    {

        public const float innerRadius = 0.5f;
        public const float outerRadius = innerRadius;

        public const float xTileSize = 1f;
        public const float xHalfTileSize = xTileSize / 2f;

        public const float yTileSize = 1.5f;
        public const float yHalfTileSize = yTileSize / 2f;

        public const float pivotOffset = 0.25f;

        public const float zSeperation = 0.1f;
        public const int tileLayers = 18;

        public static Vector3[] CalculateVerticies(Vector3 pos)
        {
            Vector3[] verts = {
                new Vector3(pos.x - xHalfTileSize, pos.y - yHalfTileSize + pivotOffset, pos.z),
                new Vector3(pos.x - xHalfTileSize, pos.y + yHalfTileSize + pivotOffset, pos.z),
                new Vector3(pos.x + xHalfTileSize, pos.y + yHalfTileSize + pivotOffset, pos.z),
                new Vector3(pos.x + xHalfTileSize, pos.y - yHalfTileSize + pivotOffset, pos.z),
            };
            return verts;
        }

        public static int[] CalculateTriangles(int initial)
        {
            int[] tris = {
                initial,
                initial + 1,
                initial + 2,
                initial,
                initial + 2,
                initial + 3,
            };

            return tris;
        }

    } 
}
