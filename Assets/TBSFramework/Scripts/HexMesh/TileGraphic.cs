using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

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

        [SerializeField]
        protected string type = "TileGraphic";
        public string Type { get { return type; } }

        [SerializeField]
        protected int id;
        public int ID { get { return id; } }

        [SerializeField]
        protected Vector2Int atlasCoord;
        public Vector2Int AtlasCoord { get { return atlasCoord; } }

        public TileGraphic(string name, int id, Vector2Int coord)
        {
            this.name = name;
            this.id = id;
            this.atlasCoord = coord;

        }

        public TileGraphic(TileGraphic graphic, int id)
        {
            name = graphic.Name;
            this.id = id;
            atlasCoord = graphic.AtlasCoord;
        }

        public static TileGraphic clear = new TileGraphic("Clear", -1, Vector2Int.zero);


        public void OnDatabaseInitialization()
        {

        }

        public string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }
    }
}
