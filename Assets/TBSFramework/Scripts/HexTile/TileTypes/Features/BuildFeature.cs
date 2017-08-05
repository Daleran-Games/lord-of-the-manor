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
        Cost BuildTimeCost;
        [SerializeField]
        CostCollection InitialBuildCosts;
        [SerializeField]
        CostCollection PerTurnBuildCosts;
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

            BuildTimeCost = new Cost(GoodType.Turns, StatType.BuildTime, Int32.Parse(entry["buildTime"]), name);

            InitialBuildCosts = new CostCollection(
                new Cost(GoodType.Labor,StatType.BuildLaborCost,Int32.Parse(entry["buildLaborCost"]),name),
                new Cost(GoodType.Wood, StatType.BuildWoodCost, Int32.Parse(entry["buildWoodCost"]), name),
                new Cost(GoodType.Stone, StatType.BuildStoneCost, Int32.Parse(entry["buildStoneCost"]), name),
                new Cost(GoodType.Gold, StatType.BuildGoldCost, Int32.Parse(entry["buildGoldCost"]), name)
                );

            PerTurnBuildCosts = new CostCollection(new Cost(GoodType.Labor, StatType.BuildLaborCost, Int32.Parse(entry["buildLaborCost"]), name));
            
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
            if (BuildTimeCost.ModifiedValue(tile.Owner.Stats) < 1)
            {
                tile.Owner.Goods.TryProcessNow(InitialBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
                OnBuildCompleted(tile);
            } else
            {
                tile.TerrainGraphics.Add(TileLayers.Building, buildGraphic);
                tile.Owner.Goods.TryProcessNow(InitialBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
                tile.Owner.Goods.AddFuture(PerTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
                tile.Counters.AddCounter(BuildTimeCost.ModifiedBy);
            }

        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {
            if (tile.Counters[BuildTimeCost.ModifiedBy] < BuildTimeCost.ModifiedValue(tile.Owner.Stats))
                tile.Owner.Goods.AddFuture(PerTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
            else if (tile.Counters[BuildTimeCost.ModifiedBy] == BuildTimeCost.ModifiedValue(tile.Owner.Stats))
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
            tile.Owner.Goods.RemoveFuture(PerTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
            tile.TerrainGraphics.Remove(TileLayers.Building);
        }

        public bool CanPlace(HexTile tile)
        {
            if (tile.Owner.Goods.CanProcessNow(InitialBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats)) && validLands.Contains(tile.Land))
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
            tile.Owner.Goods.TryProcessNow(InitialBuildCosts.GetAllReverseCostsAsTransaction(tile.Owner.Stats));
            tile.Feature = FeatureType.Null;
        }

        public bool CanCancel(HexTile tile)
        {
            return true;
        }

        public void Pause(HexTile tile)
        {
            tile.Counters.PauseCounter(true, BuildTimeCost.ModifiedBy);
            tile.Owner.Goods.RemoveFuture(PerTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
            tile.Paused = true;
        }

        public void Resume(HexTile tile)
        {
            tile.Counters.PauseCounter(false, BuildTimeCost.ModifiedBy);
            tile.Owner.Goods.AddFuture(PerTurnBuildCosts.GetAllCostsAsTransaction(tile.Owner.Stats));
            tile.Paused = false;
        }

        public bool CanResume(HexTile tile)
        {
            return true;
        }

        public TileGraphic GetWorkIcon(HexTile tile)
        {
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["UIAtlas_SmallIcon_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_Hammer"];
        }




        #endregion
    }
}
