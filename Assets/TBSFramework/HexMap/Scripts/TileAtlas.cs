using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewTileAtlas", menuName = "DaleranGames/TBS/Tile Atlas", order = 0)]
    public class TileAtlas : ScriptableObject
    {
        [Header("Season Atlases")]
        [SerializeField]
        Material springAtlas;
        public Material SpringAtlas { get { return springAtlas; } }

        [SerializeField]
        Material summerAtlas;
        public Material SummerAtlas { get { return summerAtlas; } }

        [SerializeField]
        Material fallAtlas;
        public Material FallAtlas { get { return fallAtlas; } }

        [SerializeField]
        Material winterAtlas;
        public Material WinterAtlas { get { return winterAtlas; } }


        [Header("UV Settings")]
        [SerializeField]
        int xSize = 48;
        public int XSize { get { return xSize; } }

        [SerializeField]
        int ySize = 48;
        public int YSize { get { return ySize; } }

        [SerializeField]
        float uvX = 0.1f;
        public float UVX { get { return uvX; } }

        [SerializeField]
        float uvY = 0.1f;
        public float UVY { get { return uvY; } }

        [SerializeField]
        float uvXError = 0.01f;
        public float UVXError { get { return uvXError; } }

        [SerializeField]
        float uvYError = 0.01f;
        public float UVYError { get { return uvYError; } }

        [SerializeField]
        float errorMultiplier = 20f;
        public float ErrorMultiplier { get { return errorMultiplier; } }

        float singliePixelinUnity = 0f;
        public float SinglePixelInUnity { get { return singliePixelinUnity; } }

        
        [Header("UI Atlas")]
        [SerializeField]
        Material uiAtlas;
        public Material UIAtlas { get { return uiAtlas; } }

        private void Awake()
        {
            Calculate();
        }

        [ContextMenu("Recalculate")]
        public void Calculate ()
        {

            Debug.Log(SpringAtlas.mainTexture.width);
            uvX = (float)XSize / (float)SpringAtlas.mainTexture.width;
            uvY = (float)YSize / (float)SpringAtlas.mainTexture.height;

            uvXError = UVX / (errorMultiplier * XSize);
            uvYError = UVY / (errorMultiplier * YSize);

            singliePixelinUnity = HexMetrics.xTileSize / xSize;
        }

        public Vector2[] CalculateUVs (Vector2Int coord)
        {
            Vector2[] uvs =
            {
                new Vector2(coord.x * UVX + UVXError, coord.y * UVY + UVYError),
                new Vector2(coord.x * UVX + UVXError, coord.y * UVY + UVY - UVYError),
                new Vector2(coord.x * UVX + UVX - UVXError, coord.y * UVY + UVY - UVYError),
                new Vector2(coord.x * UVX + UVX - UVXError, coord.y * UVY + UVYError)
        };

            return uvs;
        }

    }
}
