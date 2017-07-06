using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public static class HexMetrics
    {

        public const float innerRadius = 0.5f;
        public const float outerRadius = innerRadius;

        public const float gridWidth = innerRadius * 2f;
        public const float gridHeight = outerRadius * 1.5f;
        public const float hexPointHeight = outerRadius / 2;
        public const float hexSlope = hexPointHeight /innerRadius;


        public const float xTileSize = 1.5f;
        public const float xHalfTileSize = xTileSize / 2f;

        public const float yTileSize = 1.5f;
        public const float yHalfTileSize = yTileSize / 2f;

        public const float pivotOffset = 0f;

        public const float tileSperation = 0.1f;
        public const float startingZ = 1;
        public const float layerSeperation = tileSperation / maxLayers;
        public const float maxLayers = 100f;

        public const int xChunkSize = 8;
        public const int yChunkSize = 8;


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

        public static float ToFloat(this TileLayers layer)
        {
            return (int)layer * -layerSeperation;
        }

        public static Vector3 ToVector3 (this TileLayers layer)
        {
            return new Vector3(0f, 0f, layer.ToFloat());
        }

        public static bool IsUILayer (this TileLayers layer)
        {
            switch(layer)
            {
                case TileLayers.Fog:
                    return true;
                case TileLayers.OverlayIcon:
                    return true;
                case TileLayers.OverlayDigit1:
                    return true;
                case TileLayers.OverlayDigit2:
                    return true;
                case TileLayers.OverlayDigit3:
                    return true;
                default:
                    return false;
            }
        }

        public static Vector3 ToVector3WithOffset (this TileLayers layer, Vector3 position)
        {
            Vector3 zOffset = layer.ToVector3();
            switch (layer)
            {
                case TileLayers.OverlayIcon:
                    return position + new Vector3(0f, 0.375f, 0f) + zOffset;
                case TileLayers.OverlayDigit1:
                    return position + new Vector3(-0.3125f, -0.0625f, 0f) + zOffset;
                case TileLayers.OverlayDigit2:
                    return position + new Vector3(0f, -0.0625f, 0f) + zOffset;
                case TileLayers.OverlayDigit3:
                    return position + new Vector3(0.3125f, -0.0625f, 0f) + zOffset;
                default:
                    return position + zOffset;
            }
        }


    } 
}
