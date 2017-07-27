using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LandType : TileType
    {


        public LandType(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];
            iconName = entry["iconName"];

            
            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifierList",id));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("ownerModifierList", id));
            //occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            clearable = Boolean.Parse(entry["clearable"]);

            if (clearable)
            {

            }

            workable = Boolean.Parse(entry["workable", id]);
            if (workable)
            {

            }
            
        }


        #region TileStats
        List<Modifier> tileModifiers;
        public virtual List<Modifier> TileModifiers { get { return new List<Modifier>(tileModifiers); } }
        List<Modifier> ownerModifiers;
        public virtual List<Modifier> OwnerModifiers { get { return new List<Modifier>(ownerModifiers); } }
        //public virtual List<Modifier> OccupierModifiers { get { return null; } }
        CostCollection costs;
        public virtual CostCollection Costs { get { return costs; } }


        [Header("Clear Land Stats")]
        [SerializeField]
        protected bool clearable = false;
        public bool Clearable { get { return clearable; } }

        [SerializeField]
        protected string clearedLandName;

        protected LandType clearedLand;
        public LandType ClearedLand { get { return clearedLand; } }


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

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);
        }

        public override void OnDeactivation(HexTile tile)
        {
            base.OnDeactivation(tile);
            tile.TerrainGraphics.Remove(TileLayers.Land);

            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);
        }

        #endregion

    }
}
