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
        protected FeatureType builtFeature;
        string buildGraphicName;
        TileGraphic buildGraphic;
        [SerializeField]
        Cost buildTimeCost;
        [SerializeField]
        CostCollection initialBuildCosts;
        [SerializeField]
        CostCollection perTurnBuildCosts;
        [SerializeField]
        List<string> validLandNames;
        List<LandType> validLands;

        public BuildFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = "Build "+entry["name"];
            type = entry["type"];

            builtFeatureName = entry["name"];
            validLandNames = entry.ParseList("validLand");
            buildGraphicName = entry["buildGraphic"];

            buildTimeCost = new Cost(GoodType.Turns, StatType.BuildTime, Int32.Parse(entry["buildTime"]), name);

            initialBuildCosts = new CostCollection(
                new Cost(GoodType.Labor,StatType.BuildLaborCost,Int32.Parse(entry["buildLaborCost"]),name),
                new Cost(GoodType.Wood, StatType.BuildWoodCost, Int32.Parse(entry["buildWoodCost"]), name),
                new Cost(GoodType.Stone, StatType.BuildStoneCost, Int32.Parse(entry["buildStoneCost"]), name),
                new Cost(GoodType.Gold, StatType.BuildGoldCost, Int32.Parse(entry["buildGoldCost"]), name)
                );

            perTurnBuildCosts = new CostCollection(new Cost(GoodType.Labor, StatType.BuildLaborCost, Int32.Parse(entry["buildLaborCost"]), name));
            
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            if (buildGraphic != null)
                return builtFeature.GetMainGraphic(tile);
            else
                return buildGraphic;
        }

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {
            builtFeature = GameDatabase.Instance.Features[builtFeatureName];
            buildGraphic = GameDatabase.Instance.TileGraphics[buildGraphicName];

            validLands = new List<LandType>();
            for (int i=0; i<validLandNames.Count;i++)
            {
                validLands.Add(GameDatabase.Instance.Lands[validLandNames[i]]);
            }
        }

        public override void OnActivation(HexTile tile)
        {
            if (buildTimeCost.ModifiedValue(tile.Owner.Stats) < 1)
            {
                tile.Owner.Goods.ProcessNow(initialBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
                OnBuildCompleted(tile);
            } else
            {
                tile.TerrainGraphics.Add(TileLayers.Building, buildGraphic);
                tile.Owner.Goods.ProcessNow(initialBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
                tile.Owner.Goods.AddFuture(perTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
                tile.Counters.AddCounter(buildTimeCost.ModifiedBy);
                RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
            }

        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

                if (tile.Counters[buildTimeCost.ModifiedBy] < buildTimeCost.ModifiedValue(tile.Owner.Stats))
                    tile.Owner.Goods.AddFuture(perTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
                else if (tile.Counters[buildTimeCost.ModifiedBy] >= buildTimeCost.ModifiedValue(tile.Owner.Stats))
                    OnBuildCompleted(tile);
        }

        public override void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnBuildCompleted(HexTile tile)
        {
            tile.Feature = builtFeature;
        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.Counters.RemoveCounter(buildTimeCost.ModifiedBy);
            tile.Owner.Goods.RemoveFuture(perTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
            tile.TerrainGraphics.Remove(TileLayers.Building);
            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
        }

        public bool CanPlace(HexTile tile)
        {
            if (tile.Owner.Goods.CanProcessNow(initialBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats)) && validLands.Contains(tile.Land))
            {
                return true;
            }
            else
                return false;

        }

        public void Place(HexTile tile)
        {
            tile.Feature = this;
        }

        public void Cancel(HexTile tile)
        {
            CancelWithNoFeatureSwitch(tile);
            tile.Feature = FeatureType.Null;
            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public void CancelWithNoFeatureSwitch(HexTile tile)
        {
            tile.Owner.Goods.ProcessNow(initialBuildCosts.GetAllReverseCostsAsTransaction(tile.Owner.Stats));
            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public bool CanCancel(HexTile tile)
        {
            return true;
        }

        public void Pause(HexTile tile)
        {
            tile.Counters.PauseCounter(true, buildTimeCost.ModifiedBy);
            tile.Owner.Goods.RemoveFuture(initialBuildCosts.GetAllReverseCostsAsTransaction(tile.Owner.Stats));
            tile.Owner.Goods.ProcessNow(initialBuildCosts.GetAllReverseCostsAsTransaction(tile.Owner.Stats));
            tile.Paused = true;
            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public void Resume(HexTile tile)
        {
            tile.Counters.PauseCounter(false, buildTimeCost.ModifiedBy);
            tile.Owner.Goods.ProcessNow(perTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(perTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
            tile.Paused = false;
            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public bool CanResume(HexTile tile)
        {
            if (tile.Owner.Goods.CanProcessNow(perTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats)))
                return true;
            else
                return false;
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["UIAtlas_SmallIcon_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_Hammer"];
        }
        #endregion
    }
}
