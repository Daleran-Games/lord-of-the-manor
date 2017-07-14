﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class TileType : IDatabaseObject, IType<HexTile>
    {
        [Header("Tile Type Info")]
        [SerializeField]
        protected string name;
        public  virtual string Name { get { return name; } }

        [System.NonSerialized]
        protected int id;
        public virtual int ID { get { return id; } }

        [SerializeField]
        [HideInInspector]
        protected string type = "TileType";
        public string Type { get { return type; } }

        [SerializeField]
        protected string iconName;
        public string IconName { get { return iconName; } }

        [System.NonSerialized]
        protected TileGraphic iconGraphic;
        public TileGraphic IconGraphic { get { return iconGraphic; } }

        public virtual void OnDatabaseInitialization()
        {
            //Debug.Log("Create tile icon " + iconName);
            iconGraphic = GameDatabase.Instance.TileGraphics[iconName];
        }

        public virtual void OnActivation(HexTile tile)
        {
            if (GameManager.Instance.CurrentState is PlayState)
            {
                OnGameStart(tile);
            }
        }

        public virtual void OnGameStart(HexTile tile)
        {

        }

        public virtual void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnDeactivation(HexTile tile)
        {

        }

        public virtual void OnChangeOwner (HexTile tile, Group oldOwner, Group newOwner)
        {

        }

        public abstract string ToJson();
    
    }
}
