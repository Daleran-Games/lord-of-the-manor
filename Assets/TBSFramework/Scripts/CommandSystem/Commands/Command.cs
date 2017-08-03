using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class Command : IDatabaseObject
    {
        [SerializeField]
        protected string name;
        public virtual string Name { get { return name; } }

        [SerializeField]
        protected string type = "Command";
        public string Type { get { return type; } }

        [SerializeField]
        protected CommandType activity = CommandType.None;
        public virtual CommandType Activity { get { return activity; } } 

        [SerializeField]
        protected string commandIconName;

        [SerializeField]
        protected TileGraphic commandIcon;

        [SerializeField]
        protected int id;
        public virtual int ID { get { return id; } }

        public static readonly Command Null = new NullCommand("Null Command", "NullCommand", -1);

        public virtual void PreformCommand(HexTile tile)
        {

        }
        public virtual bool IsValidCommand(HexTile tile)
        {
            return false;
        }

        public virtual TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public virtual TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public virtual TileGraphic GetWorkIcon(HexTile tile)
        {
            return commandIcon;
        }

        protected virtual void Awake()
        {
            GameDatabase.Instance.DatabasesInitialized += OnDatabaseInitialization;
        }

        public virtual void OnDatabaseInitialization()
        {
            commandIcon = GameDatabase.Instance.TileGraphics[commandIconName];
        }

    }
}

