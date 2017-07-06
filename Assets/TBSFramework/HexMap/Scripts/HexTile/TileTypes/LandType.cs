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
        [SerializeField]
        protected bool clearable = false;
        public bool Clearable { get { return clearable; } }

        [SerializeField]
        protected string clearedLandName;
        public string ClearedLandName { get { return clearedLandName; } }

        [System.NonSerialized]
        protected LandType clearedLand;
        public LandType ClearedLand { get { return clearedLand; } }

        [SerializeField]
        protected Stat foodYield = new Stat(Stat.Category.FoodYield, 0);
        public Stat FoodYield { get { return foodYield; } }

        [SerializeField]
        protected Stat woodYield = new Stat(Stat.Category.FoodYield, 0);
        public Stat WoodYield { get { return woodYield; } }

        [SerializeField]
        protected Stat stoneYield = new Stat(Stat.Category.FoodYield, 0);
        public Stat StoneYield { get { return stoneYield; } }

        [SerializeField]
        protected Stat goldYield = new Stat(Stat.Category.FoodYield, 0);
        public Stat GoldYield { get { return goldYield; } }

        [SerializeField]
        protected Stat defenseBonus = new Stat(Stat.Category.FoodYield, 0);
        public Stat DefenseBonus { get { return defenseBonus; } }

        [SerializeField]
        protected Stat movementCost = new Stat(Stat.Category.FoodYield, 1);
        public Stat MovementCost { get { return movementCost; } }

        public LandType(LandType land, int id)
        {
            name = land.Name;
            this.id = id;
            clearable = land.Clearable;
            iconName = land.IconName;
            clearedLandName = land.ClearedLandName;
            this.type = this.ToString();

            foodYield = land.FoodYield;
            woodYield = land.WoodYield;
            stoneYield = land.StoneYield;
            goldYield = land.GoldYield;
            defenseBonus = land.DefenseBonus;
            movementCost = land.MovementCost;

        }

        public override void OnDatabaseInitialization()
        {

            base.OnDatabaseInitialization();

            if (clearable)
                clearedLand = GameDatabase.Instance.LandTiles.Get(clearedLandName);

        }

        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Land, iconGraphic);
        }

        public override void OnGameStart(HexTile tile)
        {
            base.OnGameStart(tile);
        }

        public override void OnTurnBegin(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Land);
        }

        public virtual void ClearTile (HexTile tile)
        {
            if (ClearedLand != null && clearable)
                tile.Land = ClearedLand;
        }

        public override string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }

    }
}
