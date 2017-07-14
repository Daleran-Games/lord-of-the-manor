using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LandType : TileType
    {


        public LandType(LandType land, int id)
        {
            name = land.Name;
            this.id = id;
            iconName = land.IconName;
            this.type = this.ToString();

            foodYield = land.BaseFoodYield;
            woodYield = land.BaseWoodYield;
            stoneYield = land.BaseStoneYield;
            goldYield = land.BaseGoldYield;
            defenseBonus = land.BaseDefenseBonus;
            movementCost = land.BaseMovementCost;

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
            if (clearable && tile.Owner.Goods.CanProcessTransaction(ClearLandCosts(tile)) && ClearedLand != null)
                return true;
            else
                return false;
        }

        [SerializeField]
        protected string clearedLandName;
        public string ClearedLandName { get { return clearedLandName; } }

        [System.NonSerialized]
        protected LandType clearedLand;
        public LandType ClearedLand { get { return clearedLand; } }

        [SerializeField]
        protected int clearLandTurns = 2;
        public Stat BaseClearLandTurns { get { return new Stat(StatType.ClearLandTurns, clearLandTurns); } }
        public Transaction ClearLandTurns(HexTile tile)
        {
            return new Transaction(false, new Good(GoodType.Turns, clearLandTurns + tile.Owner.Modifiers[StatType.ClearLandTurns]), "Clear Land");
        }

        [SerializeField]
        protected Transaction[] clearLandCosts;
        public Transaction[] BaseClearLandCosts { get { return clearLandCosts; } }
        public Transaction[] ClearLandCosts (HexTile tile)
        {
            Transaction[] newTransactions = BaseClearLandCosts;
            for (int i=0;i < BaseClearLandCosts.Length; i++)
            {
                newTransactions[i] = new Transaction(BaseClearLandCosts[i].Immediate, new Good(BaseClearLandCosts[i].TransactedGood.Type, BaseClearLandCosts[i].TransactedGood + tile.Owner.Modifiers[StatType.ClearLandCosts]), BaseClearLandCosts[i].Description);
            }
            return newTransactions;
        }

        [SerializeField]
        protected Transaction[] clearLandBonus;
        public Transaction[] BaseClearLandBonus { get { return clearLandBonus; } }

        public Transaction[] ClearLandBonus(HexTile tile)
        {
            Transaction[] newTransactions = BaseClearLandBonus;
            for (int i = 0; i < BaseClearLandCosts.Length; i++)
            {
                newTransactions[i] = new Transaction(BaseClearLandBonus[i].Immediate, new Good(BaseClearLandBonus[i].TransactedGood.Type, BaseClearLandBonus[i].TransactedGood + tile.Owner.Modifiers[StatType.ClearLandBonus]), BaseClearLandBonus[i].Description);
            }
            return newTransactions;
        }

        [Header("Work Land Stats")]
        protected bool workable = false;
        public bool Workable (HexTile tile)
        {

        }





        [SerializeField]
        protected ModifierEntry[] tileModifiers;
        #endregion

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {

            base.OnDatabaseInitialization();

            if (clearable)
                clearedLand = GameDatabase.Instance.LandTiles[ClearedLandName];
        }
        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Land, iconGraphic);

            if (tile.Owner != null)
                tile.Owner.Modifiers.Add(tileModifiers);
        }

        public override void OnDeactivation(HexTile tile)
        {
            base.OnDeactivation(tile);

            tile.TerrainGraphics.Remove(TileLayers.Land);

            if (tile.Owner != null)
                tile.Owner.Modifiers.Remove(tileModifiers);
        }

        public override void OnChangeOwner(HexTile tile, Group oldOwner, Group newOwner)
        {
            base.OnChangeOwner(tile, oldOwner, newOwner);

            if (oldOwner != null)
                oldOwner.Modifiers.Remove(tileModifiers);

            if (newOwner != null)
                newOwner.Modifiers.Add(tileModifiers);

        }
        #endregion

        public override string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }

    }
}
