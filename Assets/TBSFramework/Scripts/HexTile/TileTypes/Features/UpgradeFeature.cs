using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UpgradeFeature : FeatureType, ICancelable, IWorkable, IPlaceable
    {
        [SerializeField]
        string originalFeatureName;
        FeatureType originalFeature;

        [SerializeField]
        string upgradedFeatureName;
        FeatureType upgradedFeature;
        IBuildable upgradedBuildable;
        BuildFeature upgradedBuildFeature;

        public UpgradeFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = "Upgrade " + entry["name"];
            type = entry["type"];

            originalFeatureName = entry["name"];
            upgradedFeatureName = entry["upgradeName"];
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return upgradedFeature.GetMainGraphic(tile);
        }

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {
            originalFeature = GameDatabase.Instance.Features[originalFeatureName];
            upgradedFeature = GameDatabase.Instance.Features[upgradedFeatureName];
            upgradedBuildable = upgradedFeature as IBuildable;

            if (upgradedBuildable == null)
                Debug.LogWarning("Upgrade feature for " + upgradedFeature.Name + " does not implement IBuildable");
            else
                upgradedBuildFeature = upgradedBuildable.Build;

        }

        public override void OnActivation(HexTile tile)
        {
            upgradedBuildFeature.OnActivation(tile);
        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {
            upgradedBuildFeature.OnTurnEnd(turn, tile);
        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {
            upgradedBuildFeature.OnTurnSetUp(turn, tile);
        }

        public override void OnTurnStart(BaseTurn turn, HexTile tile)
        {
            upgradedBuildFeature.OnTurnStart(turn, tile);
        }

        public override void OnDeactivation(HexTile tile)
        {
            originalFeature.OnDeactivation(tile);
            upgradedBuildFeature.OnDeactivation(tile);
            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
        }

        public bool CanPlace(HexTile tile)
        {
            return upgradedBuildFeature.CanPlace(tile);
        }

        public void Place(HexTile tile)
        {
            tile.SwitchFeatureWithNoDeactiviation(this);
        }

        public void Cancel(HexTile tile)
        {
            upgradedBuildFeature.CancelWithNoFeatureSwitch(tile);
            tile.SwitchFeatureWithNoActiviation(originalFeature);
        }

        public bool CanCancel(HexTile tile)
        {
            return upgradedBuildFeature.CanCancel(tile);
        }

        public void Pause(HexTile tile)
        {
            upgradedBuildFeature.Pause(tile);
        }

        public void Resume(HexTile tile)
        {
            upgradedBuildFeature.Resume(tile);
        }

        public bool CanResume(HexTile tile)
        {
            return upgradedBuildFeature.CanResume(tile);
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            return upgradedBuildFeature.GetWorkIcon(tile);
        }
        #endregion


    }
}
