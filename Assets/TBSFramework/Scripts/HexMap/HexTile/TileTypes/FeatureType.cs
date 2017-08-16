using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class FeatureType : IDatabaseObject
    {
        [Header("Tile Type Info")]
        [SerializeField]
        protected string name;
        public virtual string Name { get { return name; } }

        [SerializeField]
        protected int id;
        public virtual int ID { get { return id; } }

        [SerializeField]
        [HideInInspector]
        protected string type = "TileType";
        public virtual string Type { get { return type; } }

        public static event Action<HexTile, TileGraphic> WorkIconChanged;

        public abstract TileGraphic GetMainGraphic(HexTile tile);

        public static readonly NullFeature Null = new NullFeature();

        #region Tile Callbacks
        public virtual void OnDatabaseInitialization()
        {

        }

        public virtual void OnActivation(HexTile tile)
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
        #endregion
        public virtual TileGraphic GetWorkIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        protected virtual void RaiseWorkIconChangeEvent(HexTile tile, TileGraphic newIcon)
        {
            WorkIconChanged(tile, newIcon);
        }
    }
}