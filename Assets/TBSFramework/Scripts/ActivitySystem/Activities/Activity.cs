using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class Activity : IDatabaseObject
    {
        [SerializeField]
        protected string name;
        public virtual string Name { get { return name; } }

        [SerializeField]
        protected string type = "Activity";
        public string Type { get { return type; } }

        [SerializeField]
        protected string workIconName;
        public virtual string WorkIconName { get { return workIconName; } }

        [SerializeField]
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


    }
}

