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

        [SerializeField]
        protected TileGraphic workIcon;

        [SerializeField]
        protected int id;
        public virtual int ID { get { return id; } }

        public static readonly Activity Null = new NullActivity("Null Activity", "NullActivity", -1);

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

        public virtual TileGraphic GetWorkIcon(HexTile tile)
        {
            return workIcon;
        }

        protected virtual void Awake()
        {
            GameDatabase.Instance.DatabasesInitialized += OnDatabaseInitialization;
        }

        public virtual void OnDatabaseInitialization()
        {
            workIcon = GameDatabase.Instance.TileGraphics[workIconName];
        }

    }
}

