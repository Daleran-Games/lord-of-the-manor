using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewTileAtlas", menuName = "DaleranGames/TBS/Tile Atlas", order = 0)]
    public class TileAtlas : ScriptableObject
    {

        [SerializeField]
        Material atlasMaterial;
        public Material AtlasMaterial { get { return atlasMaterial; } }

        [SerializeField]
        int xSize = 32;
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

        private void Awake()
        {
            Calculate();
        }

        [ContextMenu("Recalculate")]
        public void Calculate ()
        {

            Debug.Log(AtlasMaterial.mainTexture.width);
            uvX = (float)XSize / (float)AtlasMaterial.mainTexture.width;
            uvY = (float)YSize / (float)AtlasMaterial.mainTexture.height;

            uvXError = UVX / (20 * XSize);
            uvYError = UVY / (20 * YSize);
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
