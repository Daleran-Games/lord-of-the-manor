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

        public ImprovementType(ImprovementType type,int id)
        {
            name = type.Name;
            this.id = id;
            upgradeable = type.Upgradeable;
            iconName = type.IconName;
            upgradeName = type.UpgradeName;
            validLand = type.ValidLand;
            this.type = this.ToString();
        }

        public override void OnDatabaseInitialization()
        {
            base.OnDatabaseInitialization();

            if (upgradeable)
                upgradedImprovement = GameDatabase.Instance.Improvements.Get(upgradeName);
        }

        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Improvements,iconGraphic);
        }

        public override void OnGameStart(HexTile tile)
        {
 
        }

        public override void OnTurnChange(BaseTurn turn, HexTile tile)
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