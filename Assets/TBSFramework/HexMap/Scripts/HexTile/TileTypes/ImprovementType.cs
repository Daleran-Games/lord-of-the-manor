using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ImprovementType : TileType
    {

        public ImprovementType(string[] csv)
        {

            id = Int32.Parse(csv[0]);
            name = csv[1];
            type = csv[2];
            iconName = csv[3];
            maxCondition = Int32.Parse(csv[4]);
            defenseBonus = Int32.Parse(csv[5]);
            movementCost = Int32.Parse(csv[6]);
            upgradeable = Boolean.Parse(csv[7]);

            if (upgradeable)
            {
                upgradeName = csv[8];
            }

            validLand = new List<string>(CSVUtility.ParseList(csv, "landList"));
            ownerModifiers = Modifier.ParseCSVList(CSVUtility.ParseList(csv, "improvementModifiers"));

        }


        #region Tile Stats
        [Header("Tile Stats")]
        [SerializeField]
        protected int defenseBonus = 0;
        public Stat BaseDefenseBonus { get { return new Stat(StatType.DefenseBonus, defenseBonus); } }

        [SerializeField]
        protected int movementCost = 1;
        public Stat BaseMovementCost { get { return new Stat(StatType.MovementCost, movementCost); } }

        [SerializeField]
        protected int maxCondition = 10;
        public Stat BaseMaxCondition { get { return new Stat(StatType.MaxCondition, maxCondition); } }
        public Stat MaxCondition(HexTile tile)
        {
            return new Stat(StatType.MaxCondition, maxCondition + tile.Owner.Modifiers[StatType.MaxCondition]);
        }


        [Header("Build Stats")]
        public int buildWorkCost;
        public int buildWoodCost;
        public int builtTime;

        [SerializeField]
        protected List<string> validLand;
        public virtual List<string> ValidLand { get { return validLand; } }

        [Header("Upgrade Stats")]
        [SerializeField]
        bool upgradeable = false;
        public bool Upgradeable { get { return upgradeable; } }

        [SerializeField]
        protected string upgradeName;

        [SerializeField]
        protected ImprovementType upgradedImprovement;
        public ImprovementType UpgradedImprovement { get { return upgradedImprovement; } }

        [Header("Raze Stats")]
        public int razeWorkCost;
        public int razeTime;
        public int razeRecoveryPercentage;

        [Header("Work Stats")]
        bool workable = false;


        [Header("Modifiers")]
        [SerializeField]
        protected Modifier[] ownerModifiers;



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
                tile.Owner.Modifiers.Remove(ownerModifiers);
        }

        public virtual void OnBuilt (HexTile tile)
        {
            if (tile.Owner != null)
                tile.Owner.Modifiers.Add(ownerModifiers);
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


        public virtual bool CheckIfCanBuild (HexTile tile)
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