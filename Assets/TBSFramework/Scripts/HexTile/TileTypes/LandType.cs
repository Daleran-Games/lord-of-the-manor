using System.Collections;
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


            tileModifiers = Modifier.ParseCSVList(data.ParseList("tileModifierList", id));
            ownerModifiers = Modifier.ParseCSVList(data.ParseList("ownerModifierList", id));
            occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            clearable = Boolean.Parse(data["clearable", id]);

            if (clearable)
            {
                clearedLandName = data["clearLandName", id];

            }
        }


        #region TileStats
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
        protected Cost[] workCosts;
        public virtual Cost[] WorkCosts { get { return workCosts; } }

        [SerializeField]
        protected Cost[] workBonuses;
        public virtual Cost[] WorkBonuses { get { return workBonuses; } }


        [Header("Work Land Stats")]
        protected bool workable = false;
        public bool Workable (HexTile tile)
        {
            return false;
        }



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

            if (tile.Owner != null)
                tile.Owner.Stats.Add(ownerModifiers);
        }

        public override void OnDeactivation(HexTile tile)
        {
            base.OnDeactivation(tile);

            tile.TerrainGraphics.Remove(TileLayers.Land);

            if (tile.Owner != null)
                tile.Owner.Stats.Remove(ownerModifiers);
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

    }
}
