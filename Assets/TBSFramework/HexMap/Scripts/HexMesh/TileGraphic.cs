using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class TileGraphic : IDatabaseObject
    {
        public enum GraphicType
        {
            UI = 1,
            Terrain = 2
        }

        [SerializeField]
        protected string name;
        public string Name { get {return name; } }

        protected int id;
        public int ID { get { return id; } }

        [SerializeField]
        protected Vector2Int atlasCoord;
        public Vector2Int AtlasCoord { get { return atlasCoord; } }

        //[SerializeField]
        //protected GraphicType type;
        //public GraphicType Type { get { return type; } }

        /*
        [SerializeField]
        protected Vector2 spriteSize;
        public Vector2 SpriteSize { get { return spriteSize; } }

        protected Sprite sprite;
        public Sprite Sprite { get { return sprite; } }
        */

        public TileGraphic(string name, int id, Vector2Int coord)
        {
            this.name = name;
            this.id = id;
            this.atlasCoord = coord;
            //this.type = type;
            //this.sprite = sprite;
            //this.spriteSize = spriteSize;
        }

        public TileGraphic(TileGraphic graphic, int id)
        {
            name = graphic.Name;
            this.id = id;
            atlasCoord = graphic.AtlasCoord;
            //type = graphic.Type;
            //spriteSize = graphic.SpriteSize;
            //sprite = atlas.GenerateSpriteOfType(Type, AtlasCoord, SpriteSize);
        }

        public static TileGraphic clear = new TileGraphic("Clear", -1, Vector2Int.zero);

    }
}
