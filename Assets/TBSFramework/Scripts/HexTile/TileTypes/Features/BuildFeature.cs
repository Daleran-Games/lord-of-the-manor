using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class BuildFeature : FeatureType, ICancelable, IWorkable, IPlaceable
    {
        [SerializeField]
        protected string builtFeatureName;

        [SerializeField]
        protected FeatureType builtFeature;

        [SerializeField]
        CostCollection InitialBuildCosts;

        [SerializeField]
        CostCollection PerTurnBuildCosts;

        [SerializeField]
        List<string> validLandNames;

        [SerializeField]
        List<LandType> ValidLands;

        public BuildFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = "Build "+entry["name"];
            type = entry["type"];

            builtFeatureName = entry["name"];
            validLandNames = entry.ParseList("validLand");

        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            if (builtFeature != null)
                return builtFeature.GetMainGraphic(tile);
            else
                return TileGraphic.Clear;
        }

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {

        }

        public override void OnActivation(HexTile tile)
        {
            tile.Owner.Goods.TryProcessNow(InitialBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {

        }

        public bool CanPlace(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public void Place(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public void Cancel(HexTile tile)
        {
            OnDeactivation(tile);
        }

        public bool CanCancel(HexTile tile)
        {
            return true;
        }

        public void Pause(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public void Resume(HexTile tile)
        {
            throw new NotImplementedException();
        }




        #endregion
    }
}
