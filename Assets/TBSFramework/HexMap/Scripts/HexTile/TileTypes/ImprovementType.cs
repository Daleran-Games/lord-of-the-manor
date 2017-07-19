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
        [SerializeField]
        protected List<string> validLand;
        public virtual List<string> ValidLand { get { return validLand; } }

        [SerializeField]
        bool upgradeable = false;
        public bool Upgradeable {  get { return upgradeable; } }

        [SerializeField]
        protected string upgradeName;

        [SerializeField]
        protected ImprovementType upgradedImprovement;
        public ImprovementType UpgradedImprovement { get { return upgradedImprovement; } }
        

        [SerializeField]
        protected int maxCondition = 10;
        public Stat BaseMaxCondition { get { return new Stat(StatType.MaxCondition, maxCondition); } }
        public Stat MaxCondition(HexTile tile)
        {
            return new Stat(StatType.MaxCondition, maxCondition + tile.Owner.Modifiers[StatType.MaxCondition]);
        }

        [SerializeField]
        protected int defenseBonus = 0;
        public Stat BaseDefenseBonus { get { return new Stat(StatType.DefenseBonus, defenseBonus); } }

        [SerializeField]
        protected int movementCost = 1;
        public Stat BaseMovementCost { get { return new Stat(StatType.MovementCost, movementCost); } }

        [SerializeField]
        protected Modifier[] tileModifiers;


        public ImprovementType(string[] csv)
        {

            id = Int32.Parse(csv[0]);
            name = csv[1];
            type = csv[2];

            maxCondition = Int32.Parse(csv[3]);
            defenseBonus = Int32.Parse(csv[4]);
            movementCost = Int32.Parse(csv[5]);
            upgradeable = Boolean.Parse(csv[6]);

            if (upgradeable)
            {

            }
            
            
        }

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
                tile.Owner.Modifiers.Remove(tileModifiers);
        }

        public virtual void OnBuilt (HexTile tile)
        {
            if (tile.Owner != null)
                tile.Owner.Modifiers.Add(tileModifiers);
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


        public virtual bool CheckIfCanBuild (HexTile tile)
        {
            if (validLand.Contains(tile.Land.Name) && tile.Improvement == null)
                return true;

            return false;
        }

        public virtual void Upgrade(HexTile tile)
        {
           // if (UpgradedImprovement != null && Upgradeable)
                //tile.ChangeImprovementType(UpgradedImprovement);
        }

    }
}