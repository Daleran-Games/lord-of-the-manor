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


        public LandType(string[] csv)
        {
            id = Int32.Parse(csv[0]);
            name = csv[1];
            type = csv[2];
            iconName = csv[3];
            defenseBonus = Int32.Parse(csv[4]);
            movementCost = Int32.Parse(csv[5]);
            foodYield = Int32.Parse(csv[6]);
            woodYield = Int32.Parse(csv[7]);
            stoneYield = Int32.Parse(csv[8]);
            goldYield = Int32.Parse(csv[9]);
            clearable = Boolean.Parse(csv[10]);

            if (clearable)
            {
                clearedLandName = csv[11];
                clearLandTurns = Int32.Parse(csv[12]);
                clearLandWork = Int32.Parse(csv[13]);
                clearFoodBonus = Int32.Parse(csv[14]);
                clearWoodBonus = Int32.Parse(csv[15]);
                clearStoneBonus = Int32.Parse(csv[16]);
                clearGoldBonus = Int32.Parse(csv[17]);
            } 
            ownerModifiers = Modifier.ParseCSVList(CSVUtility.ParseList(csv, "landModifiers"));
        }


        #region TileStats
        [Header("Tile Stats")]
        [SerializeField]
        protected int defenseBonus = 0;
        public Stat BaseDefenseBonus { get { return new Stat(StatType.DefenseBonus, defenseBonus); } }

        [SerializeField]
        protected int movementCost = 1;
        public Stat BaseMovementCost { get { return new Stat(StatType.MovementCost, movementCost); } }

        [Header("Tile Yields")]
        [SerializeField]
        protected int foodYield = 0;
        public Stat BaseFoodYield { get { return new Stat(StatType.FoodYield, foodYield); } }
        public Stat FoodYield (HexTile tile)
        {
            return new Stat(StatType.FoodYield, foodYield + tile.Owner.Modifiers[StatType.FoodYield]);
        }

        [SerializeField]
        protected int woodYield = 0;
        public Stat BaseWoodYield { get { return new Stat(StatType.WoodYield, woodYield); } }
        public Stat WoodYield(HexTile tile)
        {
            return new Stat(StatType.WoodYield, woodYield + tile.Owner.Modifiers[StatType.WoodYield]);
        }

        [SerializeField]
        protected int stoneYield = 0;
        public Stat BaseStoneYield { get { return new Stat(StatType.StoneYield, stoneYield); } }
        public Stat StoneYield(HexTile tile)
        {
            return new Stat(StatType.StoneYield, stoneYield + tile.Owner.Modifiers[StatType.StoneYield]);
        }

        [SerializeField]
        protected int goldYield = 0;
        public Stat BaseGoldYield { get { return new Stat(StatType.GoldYield, goldYield); } }
        public Stat GoldYield(HexTile tile)
        {
            return new Stat(StatType.GoldYield, goldYield + tile.Owner.Modifiers[StatType.GoldYield]);
        }

        [Header("Clear Land Stats")]
        [SerializeField]
        protected bool clearable = false;
        public bool Clearable (HexTile tile)
        {
            if (clearable && tile.Owner.Goods.CanProcessTransaction(ClearLandCost(tile)) && ClearedLand != null)
                return true;
            else
                return false;
        }

        [SerializeField]
        protected string clearedLandName;

        [System.NonSerialized]
        protected LandType clearedLand;
        public LandType ClearedLand { get { return clearedLand; } }

        [SerializeField]
        protected int clearLandTurns = 2;
        public Stat BaseClearLandTurns { get { return new Stat(StatType.ClearLandTurns, clearLandTurns); } }
        public Stat ClearLandTurns(HexTile tile)
        {
            return new Stat(StatType.ClearLandTurns, clearLandTurns + tile.Owner.Modifiers[StatType.ClearLandTurns]);
        }

        [SerializeField]
        protected int clearLandWork;
        public Stat BaseClearLandWork { get { return new Stat(StatType.ClearLandCost, clearLandWork); } }
        public Transaction ClearLandCost (HexTile tile)
        {
            return new Transaction(new Good(GoodType.Work, clearLandWork + tile.Owner.Modifiers[StatType.ClearLandCost]), false, "Clearing " + name);
        }

        [SerializeField]
        protected int clearFoodBonus;
        public Stat BaseClearFoodBonus { get { return new Stat(StatType.ClearLandBonus, clearFoodBonus); } }
        public Transaction ClearFoodBonus(HexTile tile)
        {
            return new Transaction(new Good(GoodType.Food, clearFoodBonus + tile.Owner.Modifiers[StatType.ClearLandBonus]), false, "Clearing " + name);
        }

        [SerializeField]
        protected int clearWoodBonus;
        public Stat BaseClearWoodBonus { get { return new Stat(StatType.ClearLandBonus, clearWoodBonus); } }
        public Transaction ClearWoodBonus(HexTile tile)
        {
            return new Transaction(new Good(GoodType.Wood, clearWoodBonus + tile.Owner.Modifiers[StatType.ClearLandBonus]), false, "Clearing " + name);
        }

        [SerializeField]
        protected int clearStoneBonus;
        public Stat BaseClearStoneBonus { get { return new Stat(StatType.ClearLandBonus, clearStoneBonus); } }
        public Transaction ClearStoneBonus(HexTile tile)
        {
            return new Transaction(new Good(GoodType.Stone, clearStoneBonus + tile.Owner.Modifiers[StatType.ClearLandBonus]), false, "Clearing " + name);
        }

        [SerializeField]
        protected int clearGoldBonus;
        public Stat BaseClearGoldBonus { get { return new Stat(StatType.ClearLandBonus, clearStoneBonus); } }
        public Transaction ClearGoldBonus(HexTile tile)
        {
            return new Transaction(new Good(GoodType.Gold, clearGoldBonus + tile.Owner.Modifiers[StatType.ClearLandBonus]), false, "Clearing " + name);
        }


        [Header("Work Land Stats")]
        protected bool workable = false;
        public bool Workable (HexTile tile)
        {
            return false;
        }


        [Header("Modifiers")]
        [SerializeField]
        protected Modifier[] ownerModifiers = new Modifier[0];
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
                tile.Owner.Modifiers.Add(ownerModifiers);
        }

        public override void OnDeactivation(HexTile tile)
        {
            base.OnDeactivation(tile);

            tile.TerrainGraphics.Remove(TileLayers.Land);

            if (tile.Owner != null)
                tile.Owner.Modifiers.Remove(ownerModifiers);
        }

        public override void OnChangeOwner(HexTile tile, Group oldOwner, Group newOwner)
        {
            base.OnChangeOwner(tile, oldOwner, newOwner);

            if (oldOwner != null)
                oldOwner.Modifiers.Remove(ownerModifiers);

            if (newOwner != null)
                newOwner.Modifiers.Add(ownerModifiers);

        }
        #endregion

    }
}
