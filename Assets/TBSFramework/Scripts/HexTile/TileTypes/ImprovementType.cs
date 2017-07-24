using System.Collections;
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

            tileModifiers = new Modifier[0];
            ownerModifiers = new Modifier[0];
            occupierModifiers = new Modifier[0];

            buildable = false;
            upgradeable = false;
            razeable = false;
            workable = false;

        }

        public static readonly ImprovementType Null = new ImprovementType();

        public ImprovementType(CSVData data, int id)
        {

            this.id = id;
            name = data["name", id];
            type = data["type", id];
            iconName = data["iconName", id];

            tileModifiers = Modifier.ParseCSVList(data.ParseList("tileModifierList", id));
            ownerModifiers = Modifier.ParseCSVList(data.ParseList("ownerModifierList", id));
            occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            buildable = Boolean.Parse(data["buildable", id]);
            if (buildable)
            {
                buildTime = Int32.Parse(data["buildTime", id]);
                buildCosts = Cost.ParseCSVList(data.ParseList("buildCostList", id));
                validLand = new List<string>(data.ParseList("landList", id));
            }

            upgradeable = Boolean.Parse(data["upgradeable",id]);
            if (upgradeable)
                upgradeName = data["upgradeName", id];

            razeTime = Int32.Parse(data["razeTime", id]);
            razeCosts = Cost.ParseCSVList(data.ParseList("razeCostList", id));

            workable = Boolean.Parse(data["workable", id]);
            if (workable)
            {
                workCosts = Cost.ParseCSVList(data.ParseList("workCostList", id));
                workBonuses = Cost.ParseCSVList(data.ParseList("workBonusList", id));
                workModifiers = Modifier.ParseCSVList(data.ParseList("workModifierList", id));
            }

        }

        #region Tile Stats
        [Header("Tile Stats")]
        [SerializeField]
        protected Modifier[] tileModifiers;
        public virtual Modifier[] TileModifiers { get { return tileModifiers; } }

        [SerializeField]
        protected Modifier[] ownerModifiers;
        public virtual Modifier[] OwnerModifiers { get { return ownerModifiers; } }

        [SerializeField]
        protected Modifier[] occupierModifiers;
        public virtual Modifier[] OccupierModifiers { get { return occupierModifiers; } }

        [Header("Build Stats")]
        [SerializeField]
        protected bool buildable;
        public virtual bool Buildable { get { return buildable; } }

        [SerializeField]
        protected int buildTime;
        public virtual int Builtdime { get { return buildTime; } }

        [SerializeField]
        protected Cost[] buildCosts;
        public virtual Cost[] BuildCosts { get { return buildCosts; } }

        [SerializeField]
        protected List<string> validLand;
        public virtual List<string>ValidLand { get { return validLand; } }

        [Header("Upgrade Stats")]
        [SerializeField]
        protected bool upgradeable = false;
        public virtual bool Upgradeable { get { return upgradeable; } }

        [SerializeField]
        protected string upgradeName;

        [SerializeField]
        protected ImprovementType upgradedImprovement;
        public virtual ImprovementType UpgradedImprovement { get { return upgradedImprovement; } }

        [Header("Raze Stats")]
        [SerializeField]
        protected bool razeable = false;
        public virtual bool Razeable { get { return razeable; } }

        public int razeTime;
        public virtual int RazeTime { get { return razeTime; } }

        protected Cost[] razeCosts;
        public virtual Cost[] RazeCosts { get { return razeCosts; } }

        [Header("Work Stats")]
        [SerializeField]
        protected bool workable = false;
        public virtual bool Workable { get { return workable; } }

        [SerializeField]
        protected Cost[] workCosts;
        public virtual Cost[] WorkCosts { get { return workCosts; } }

        [SerializeField]
        protected Cost[] workBonuses;
        public virtual Cost[] WorkBonuses { get { return workBonuses; } }

        [SerializeField]
        protected Modifier[] workModifiers;
        public virtual Modifier[] WorkModifiers {get { return workModifiers; } }


        #endregion

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {
            base.OnDatabaseInitialization();

            if (upgradeable)
                upgradedImprovement = GameDatabase.Instance.Improvements[upgradeName];
        }

        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Improvements,iconGraphic);

            if (tile.Owner != null)
                tile.Owner.Stats.Add(ownerModifiers);
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

            if (tile.Owner != null)
                tile.Owner.Stats.Remove(ownerModifiers);
        }

        public virtual void OnBuilt (HexTile tile)
        {
            if (tile.Owner != null)
                tile.Owner.Stats.Add(ownerModifiers);
        }

        public override void OnChangeOwner(HexTile tile, Group oldOwner, Group newOwner)
        {
            base.OnChangeOwner(tile, oldOwner, newOwner);

            if (oldOwner != null)
                oldOwner.Stats.Remove(ownerModifiers);

            if (newOwner != null)
                newOwner.Stats.Add(ownerModifiers);

        }

        #endregion


        public virtual bool CanBuildOnTile (HexTile tile)
        {
            if (validLand.Contains(tile.Land.Name) && tile.Improvement == null)
                return true;

            return false;
        }

        public virtual void Upgrade(HexTile tile)
        {
            if (UpgradedImprovement != null && Upgradeable)
                tile.Improvement = UpgradedImprovement;
        }

    }
}