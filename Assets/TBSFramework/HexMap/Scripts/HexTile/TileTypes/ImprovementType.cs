using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;


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
        public string UpgradeName { get { return upgradeName; } }

        [System.NonSerialized]
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
        protected ModifierEntry[] tileModifiers;


        public ImprovementType(ImprovementType impr,int id)
        {
            name = impr.Name;
            this.id = id;
            upgradeable = impr.Upgradeable;
            iconName = impr.IconName;
            upgradeName = impr.UpgradeName;
            validLand = impr.ValidLand;
            this.type = this.ToString();

            defenseBonus = impr.defenseBonus;
            movementCost = impr.movementCost;
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

        public override string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }

    }
}