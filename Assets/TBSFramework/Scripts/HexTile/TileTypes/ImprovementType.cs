using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ImprovementType : TileType
    {

        protected ImprovementType()
        {
            id = -1;
            name = "Null Improvment Type";
            type = "ImprovementType";
            iconName = "null";

            tileModifiers = new List<Modifier>(0);
            ownerModifiers = new List<Modifier>(0);
            //occupierModifiers = new Modifier[0];

            buildable = false;
            upgradeable = false;
            razeable = false;
            //workable = false;

        }

        public static readonly ImprovementType Null = new ImprovementType();

        #region Tile Stats
        [Header("Tile Stats")]
        [SerializeField]
        protected List<Modifier> tileModifiers;
        public virtual List<Modifier> TileModifiers { get { return new List<Modifier>(tileModifiers); } }

        [SerializeField]
        protected List<Modifier> ownerModifiers;
        public virtual List<Modifier> OwnerModifiers { get { return new List<Modifier>(ownerModifiers); } }

        //[SerializeField]
        // protected Modifier[] occupierModifiers;
        // public virtual Modifier[] OccupierModifiers { get { return occupierModifiers; } }

        [SerializeField]
        protected CostCollection costs;
        public CostCollection Costs { get { return costs; } } 

        [Header("Build Stats")]
        [SerializeField]
        protected bool buildable;
        public virtual bool Buildable { get { return buildable; } }

        [SerializeField]
        protected string buildGraphicName;

        protected TileGraphic buildGraphic;
        public TileGraphic BuildGraphic { get { return buildGraphic; } }

        [Header("Upgrade Stats")]
        [SerializeField]
        protected bool upgradeable = false;
        public virtual bool Upgradeable { get { return upgradeable; } }

        [SerializeField]
        protected string upgradeName;

        [SerializeField]
        protected ImprovementType upgradedImprovement;
        public virtual ImprovementType UpgradedImprovement { get { return upgradedImprovement; } }

        [SerializeField]
        protected bool razeable = false;
        public virtual bool Razeable { get { return razeable; } }

        [SerializeField]
        protected bool workable = false;
        public virtual bool Workable { get { return workable; } }     

        #endregion

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {
            base.OnDatabaseInitialization();

            if (upgradeable)
                upgradedImprovement = GameDatabase.Instance.Improvements[upgradeName];

            if (buildable)
                buildGraphic = GameDatabase.Instance.TileGraphics[buildGraphicName];
        }

        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Improvements,iconGraphic);

            tile.Stats.Add(tileModifiers);
            tile.OwnerModifiers.Add(ownerModifiers);
        }

        public override void OnGameStart(HexTile tile)
        {
 
        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Improvements);

            tile.Stats.Remove(tileModifiers);
            tile.OwnerModifiers.Remove(ownerModifiers);
        }

        #endregion

    }
}