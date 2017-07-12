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
        public List<string> ValidLand { get { return validLand; } }

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
        protected int defenseBonus = 0;
        public Stat DefenseBonus { get { return new Stat("UnitDefenseBonus", defenseBonus); } }

        [SerializeField]
        protected int movementCost = 1;
        public Stat MovementCost { get { return new Stat("UnitMovementCost", movementCost); } }

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
        }

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

        public override string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }

    }
}