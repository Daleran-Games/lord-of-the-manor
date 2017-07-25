using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LandType : TileType
    {


        public LandType(CSVData data, int id)
        {
            this.id = id;
            name = data["name", id];
            type = data["type", id];
            iconName = data["iconName", id];


            tileModifiers = Modifier.ParseCSVList(data.ParseList("tileModifierList",id));
            ownerModifiers = Modifier.ParseCSVList(data.ParseList("ownerModifierList", id));
            //occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            clearable = Boolean.Parse(data["clearable", id]);

            if (clearable)
            {
                clearedLandName = data["clearLandName", id];
                clearCosts = Cost.ParseCSVList(data.ParseList("clearCostList", id));
                clearBonuses = Cost.ParseCSVList(data.ParseList("clearBonusList", id));
            }

            workable = Boolean.Parse(data["workable", id]);
            if (workable)
            {
                workCosts = Cost.ParseCSVList(data.ParseList("workCostList", id));
                workBonuses = Cost.ParseCSVList(data.ParseList("workBonusList", id));
                workModifiers = Modifier.ParseCSVList(data.ParseList("workModifierList", id));
            }
        }


        #region TileStats
        [Header("Tile Stats")]
        [SerializeField]
        protected List<Modifier> tileModifiers;
        public virtual ReadOnlyCollection<Modifier> TileModifiers { get { return tileModifiers.AsReadOnly(); } }

        [SerializeField]
        protected List<Modifier> ownerModifiers;
        public virtual ReadOnlyCollection<Modifier> OwnerModifiers { get { return ownerModifiers.AsReadOnly(); } }

        //[SerializeField]
        //protected Modifier[] occupierModifiers;
        //public virtual Modifier[] OccupierModifiers { get { return occupierModifiers; } }



        [Header("Clear Land Stats")]
        [SerializeField]
        protected bool clearable = false;
        public bool Clearable { get { return clearable; } }

        [SerializeField]
        protected string clearedLandName;

        protected LandType clearedLand;
        public LandType ClearedLand { get { return clearedLand; } }

        [SerializeField]
        protected int clearLandTime;
        public virtual int ClearLandTime { get { return clearLandTime; } }

        [SerializeField]
        protected List<Cost> clearCosts;
        public virtual ReadOnlyCollection<Cost> ClearCosts { get { return clearCosts.AsReadOnly(); } }

        [SerializeField]
        protected List<Cost> clearBonuses;
        public virtual ReadOnlyCollection<Cost> ClearBonuses { get { return clearBonuses.AsReadOnly(); } }


        [Header("Work Land Stats")]
        protected bool workable = false;
        public bool Workable { get { return workable; } }

        [SerializeField]
        protected List<Cost> workCosts;
        public virtual ReadOnlyCollection<Cost> WorkCosts { get { return workCosts.AsReadOnly(); } }

        [SerializeField]
        protected List<Cost> workBonuses;
        public virtual ReadOnlyCollection<Cost> WorkBonuses { get { return workBonuses.AsReadOnly(); } }

        [SerializeField]
        protected List<Modifier> workModifiers;
        public virtual ReadOnlyCollection<Modifier> WorkModifiers { get { return workModifiers.AsReadOnly(); } }



        #endregion

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {
            base.OnDatabaseInitialization();


            if (clearable)
                clearedLand = GameDatabase.Instance.Lands[clearedLandName];
        }
        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Land, iconGraphic);

            tile.Stats.Add(tileModifiers);
            tile.OwnerModifiers.Add(ownerModifiers);
        }

        public override void OnDeactivation(HexTile tile)
        {
            base.OnDeactivation(tile);
            tile.TerrainGraphics.Remove(TileLayers.Land);

            tile.Stats.Remove(tileModifiers);
            tile.OwnerModifiers.Remove(ownerModifiers);
        }

        #endregion

    }
}
