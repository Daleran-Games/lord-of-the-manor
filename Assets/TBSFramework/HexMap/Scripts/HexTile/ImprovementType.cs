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
        protected List<string> validLandToBuildNames;

        [SerializeField]
        bool upgradeable = false;
        public bool Upgradeable {  get { return upgradeable; } }

        [SerializeField]
        protected string upgradeName;
        public string UpgradeName { get { return upgradeName; } }
        protected ImprovementType upgradedImprovement;
        public ImprovementType UpgradedImprovement { get { return upgradedImprovement; } }

        public ImprovementType(string name, int id, string iconName, string upgrade, bool canUpgrade)
        {
            this.name = name;
            this.id = id;
            upgradeable = canUpgrade;
            this.iconName = iconName;
            upgradeName = upgrade;
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
            tile.TerrainGraphics.Remove(TileLayers.Land);
        }

        public virtual bool CheckIfCanBuild (HexTile tile)
        {
            if (validLandToBuildNames.Contains(tile.Land.Name) && tile.Improvement == null)
                return true;

            return false;
        }

        public virtual void Upgrade(HexTile tile)
        {
            if (UpgradedImprovement != null)
                tile.Improvement = UpgradedImprovement;
        }

    }
}