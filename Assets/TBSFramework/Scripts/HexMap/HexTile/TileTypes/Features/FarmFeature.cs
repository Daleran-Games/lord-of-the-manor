using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class FarmFeature : FeatureType, IWorkable, IPlaceable, ICancelable
    {
        [SerializeField]
        string sowingGraphicName;
        TileGraphic sowingGraphic;
        [SerializeField]
        string growingGraphicName;
        TileGraphic growingGraphic;
        [SerializeField]
        string harvestGraphicName;
        TileGraphic harvestGraphic;
        [SerializeField]
        string fallowGraphicName;
        TileGraphic fallowGraphic;

        [SerializeField]
        Cost growingTime;
        [SerializeField]
        Cost cycleTime;

        [SerializeField]
        Cost buildLaborCost;
        [SerializeField]
        Cost cultivateLaborCost;

        [SerializeField]
        List<string> validLandNames;
        List<LandType> validLands;

        [SerializeField]
        List<Modifier> tileModifiers;
        public virtual List<Modifier> TileModifiers { get { return new List<Modifier>(tileModifiers); } }
        [SerializeField]
        List<Modifier> ownerModifiers;
        public virtual List<Modifier> OwnerModifiers { get { return new List<Modifier>(ownerModifiers); } }

        public FarmFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];

            sowingGraphicName = entry["sowingGraphic"];
            growingGraphicName = entry["growingGraphic"];
            harvestGraphicName = entry["harvestGraphic"];
            fallowGraphicName = entry["fallowGraphic"];

            validLandNames = entry.ParseList("validLand");

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));


            growingTime = new Cost(GoodType.Turns, StatType.FarmGrowTime, Int32.Parse(entry["growTime"]), name);
            cycleTime = new Cost(GoodType.Turns, StatType.FarmCycleTime, Int32.Parse(entry["cycleTime"]), name);

            buildLaborCost = new Cost(GoodType.Labor, StatType.FarmLaborCost, Int32.Parse(entry["buildLaborCost"]), name);
            cultivateLaborCost = new Cost(GoodType.Labor, StatType.FarmLaborCost, Int32.Parse(entry["cultivateLaborCost"]), name);

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.MaxCondition, Int32.Parse(entry["maxCondition"]), name));
            tileModifiers.Add(new Modifier(StatType.FarmingFoodPerYield, Int32.Parse(entry["farmFoodRate"]), name));

        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return harvestGraphic;
        }

        public override void OnDatabaseInitialization()
        {
            sowingGraphic = GameDatabase.Instance.TileGraphics[sowingGraphicName];
            growingGraphic = GameDatabase.Instance.TileGraphics[growingGraphicName];
            harvestGraphic = GameDatabase.Instance.TileGraphics[harvestGraphicName];
            fallowGraphic = GameDatabase.Instance.TileGraphics[fallowGraphicName];

            validLands = new List<LandType>();
            for (int i = 0; i < validLandNames.Count; i++)
            {
                validLands.Add(GameDatabase.Instance.Lands[validLandNames[i]]);
            }
        }

        public override void OnActivation(HexTile tile)
        {
            tile.TerrainGraphics.Add(TileLayers.Improvements, sowingGraphic);

            tile.Counters.AddCounter(cycleTime.ModifiedBy);

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.ProcessNow(buildLaborCost.ModifiedTransaction(tile.Owner.Stats));

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {
            if (tile.Counters[cycleTime.ModifiedBy] == 0)
            {
                tile.Counters.AddCounter(cycleTime.ModifiedBy);

                tile.TerrainGraphics.Remove(TileLayers.Improvements);
                tile.TerrainGraphics.Add(TileLayers.Improvements, sowingGraphic);
            }
            else if (tile.Counters[cycleTime.ModifiedBy] == 1)
            {
                tile.TerrainGraphics.Remove(TileLayers.Improvements);
                tile.TerrainGraphics.Add(TileLayers.Improvements, growingGraphic);

            } else if (tile.Counters[cycleTime.ModifiedBy] == growingTime.ModifiedValue(tile.Owner.Stats))
            {
                tile.TerrainGraphics.Remove(TileLayers.Improvements);
                tile.TerrainGraphics.Add(TileLayers.Improvements, harvestGraphic);
                tile.Owner.Goods.AddFuture(cultivateLaborCost.ModifiedTransaction(tile.Owner.Stats));
                tile.Owner.Goods.AddFuture(new Transaction(GoodType.Food, tile.Stats[StatType.FarmingFoodRate], name));

            }else if (tile.Counters[cycleTime.ModifiedBy] < cycleTime.ModifiedValue(tile.Owner.Stats)-1 && tile.Counters[cycleTime.ModifiedBy] > growingTime.ModifiedValue(tile.Owner.Stats))
            {
                tile.TerrainGraphics.Remove(TileLayers.Improvements);
                tile.TerrainGraphics.Add(TileLayers.Improvements, fallowGraphic);
            }
            if (tile.Counters[cycleTime.ModifiedBy] >= cycleTime.ModifiedValue(tile.Owner.Stats)-1)
            {
                tile.Counters.RemoveCounter(cycleTime.ModifiedBy);
                tile.Owner.Goods.AddFuture(cultivateLaborCost.ModifiedTransaction(tile.Owner.Stats));
            }
        }

        public override void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Improvements);

            tile.Counters.RemoveCounter(cycleTime.ModifiedBy);

            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);

            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
        }


        public void Pause(HexTile tile)
        {

            tile.Counters.RemoveCounter(cycleTime.ModifiedBy);



            tile.TerrainGraphics.Remove(TileLayers.Improvements);
            tile.TerrainGraphics.Add(TileLayers.Improvements, fallowGraphic);

            tile.Owner.Goods.RemoveFuture(cultivateLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.ProcessNow(cultivateLaborCost.ReverseModifiedTransaction(tile.Owner.Stats));

            tile.Owner.Goods.RemoveFuture(new Transaction(GoodType.Food, tile.Stats[StatType.FarmingFoodRate], name));

            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);

            tile.Paused = true;

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public void Resume(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Improvements);
            tile.TerrainGraphics.Add(TileLayers.Improvements, sowingGraphic);

            tile.Counters.AddCounter(cycleTime.ModifiedBy);

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.ProcessNow(cultivateLaborCost.ModifiedTransaction(tile.Owner.Stats));

            tile.Paused = false;

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public bool CanResume(HexTile tile)
        {
            if ((TurnManager.Instance.CurrentTurn == TurnManager.Instance.Spring || TurnManager.Instance.CurrentTurn == TurnManager.Instance.Fall))
                return true;
            else
                return false;
        }

        public int GetWorkUtility(HexTile tile)
        {
            return 9 * tile.Stats[StatType.QuarryingStoneRate];
        }

        public int GetLaborWorkCosts(HexTile tile)
        {
            return cultivateLaborCost.ModifiedValue(tile.Owner.Stats);
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["Icon_16px_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["Icon_16px_Farm"];
        }

        public bool CanPlace(HexTile tile)
        {
            if (validLands.Contains(tile.Land) && (TurnManager.Instance.CurrentTurn == TurnManager.Instance.Spring || TurnManager.Instance.CurrentTurn == TurnManager.Instance.Fall))
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

        public bool CanCancel(HexTile tile)
        {
            return true;
        }

        public void Cancel(HexTile tile)
        {
            tile.Owner.Goods.ProcessNow(cultivateLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Feature = FeatureType.Null;
        }
    }
}
