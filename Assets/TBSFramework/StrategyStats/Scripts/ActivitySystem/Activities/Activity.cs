using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class Activity : IDatabaseObject
    {
        [SerializeField]
        protected string name;
        public virtual string Name { get { return name; } }

        [System.NonSerialized]
        protected int id;
        public virtual int ID { get { return id; } }

        public abstract void DoActivityOnTile(HexTile tile);
        public abstract bool IsActivityValid(HexTile tile);
        public abstract TileGraphic GetUIIcon(HexTile tile);
        public abstract TileGraphic GetTerrainIcon(HexTile tile);

        protected virtual void Awake()
        {
            GameDatabase.Instance.DatabasesInitialized += OnDatabaseInitialization;
        }

        public virtual void OnDatabaseInitialization()
        {

        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}

