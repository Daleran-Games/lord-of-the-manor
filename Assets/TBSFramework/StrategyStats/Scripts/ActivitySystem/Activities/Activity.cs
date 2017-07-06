using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Activity : IDatabaseObject
    {
        [SerializeField]
        protected string name;
        public virtual string Name { get { return name; } }

        [SerializeField]
        [HideInInspector]
        protected string type = "Activity";
        public string Type { get { return type; } }

        [System.NonSerialized]
        protected int id;
        public virtual int ID { get { return id; } }

        public virtual void DoActivityOnTile(HexTile tile)
        {

        }
        public virtual bool IsActivityValid(HexTile tile)
        {
            return false;
        }
        public virtual TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }
        public virtual TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        protected virtual void Awake()
        {
            GameDatabase.Instance.DatabasesInitialized += OnDatabaseInitialization;
        }

        public virtual void OnDatabaseInitialization()
        {

        }

        public virtual string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }
    }
}

