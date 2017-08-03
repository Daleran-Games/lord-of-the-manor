using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LandType : IDatabaseObject
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

        [SerializeField]
        protected string iconName;

        [SerializeField]
        protected TileGraphic iconGraphic = TileGraphic.Clear;
        public virtual TileGraphic IconGraphic { get { return iconGraphic; } }

        public LandType(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];
            iconName = entry["iconName"];

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));
            //occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.FoodYield, Int32.Parse(entry["foodYield"]), name));
            tileModifiers.Add(new Modifier(StatType.WoodYield, Int32.Parse(entry["woodYield"]), name));
            tileModifiers.Add(new Modifier(StatType.StoneYield, Int32.Parse(entry["stoneYield"]), name));
            tileModifiers.Add(new Modifier(StatType.GoldYield, Int32.Parse(entry["goldYield"]), name));
        }


        #region TileStats
        [SerializeField]
        List<Modifier> tileModifiers;
        public virtual List<Modifier> TileModifiers { get { return new List<Modifier>(tileModifiers); } }
        [SerializeField]
        List<Modifier> ownerModifiers;
        public virtual List<Modifier> OwnerModifiers { get { return new List<Modifier>(ownerModifiers); } }
        //public virtual List<Modifier> OccupierModifiers { get { return null; } }

        #endregion

        #region Tile Callbacks
        public virtual void OnDatabaseInitialization()
        {
            if (iconName != null)
                iconGraphic = GameDatabase.Instance.TileGraphics[iconName];
        }
        public virtual void OnActivation(HexTile tile)
        {
            tile.TerrainGraphics.Add(TileLayers.Land, iconGraphic);

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);
        }

        public virtual void OnDeactivation(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Land);

            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);
        }

        #endregion

    }
}
