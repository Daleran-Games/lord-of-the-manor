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
        int ppu = 32;
        public int PPU { get { return ppu; } }

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

            //Debug.Log(SpringAtlas.mainTexture.width);
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

        public Sprite GenerateSpriteOfType (TileGraphic.GraphicType type, Vector2Int coord, Vector2 size)
        {
            switch(type)
            {
                case TileGraphic.GraphicType.Terrain:
                    return GenerateSprite(coord, SpringAtlas, size);
                case TileGraphic.GraphicType.UI:
                    return GenerateSprite(coord, UIAtlas, size);
                default:
                    throw new KeyNotFoundException("No atlas of the type: " + type.ToString());
            }
        }

        public Sprite GenerateSprite(Vector2Int coord, Material mat)
        {
            return GenerateSprite(coord, mat, new Vector2(xSize, ySize));
        }

        public Sprite GenerateSprite(Vector2Int coord, Material mat, Vector2 cropToSize)
        {
            int texWidth = mat.mainTexture.width;
            int texHeight = mat.mainTexture.height;
            Rect bounds = new Rect(coord.x * xSize, coord.y * ySize + ySize, cropToSize.x,cropToSize.y);
            return Sprite.Create((Texture2D)mat.mainTexture, bounds, new Vector2(0.5f, 0.5f), PPU, 1, SpriteMeshType.FullRect);
        }

        public Vector2Int GetCoordFromRect (Rect spriteRect)
        {
            int x = (int)((spriteRect.center.x - ((float)xSize / 2f)) / (float)xSize);
            int y = (int)((spriteRect.center.y - ((float)ySize / 2f)) / (float)ySize);
            return new Vector2Int(x, y);
        }


    }
}
