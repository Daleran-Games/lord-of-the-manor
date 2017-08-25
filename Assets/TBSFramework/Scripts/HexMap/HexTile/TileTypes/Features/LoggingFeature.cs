using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LoggingFeature : FeatureType, IPlaceable, ISeasonable
    {
        [SerializeField]
        string loggingGraphicName;
        TileGraphic loggingGraphic;

        [SerializeField]
        Cost loggingTime;

        [SerializeField]
        Cost loggingLaborCost;

        [SerializeField]
        List<string> validLandNames;
        List<LandType> validLands;

        [SerializeField]
        List<string> completeLandNames;
        List<LandType> completeLands;

        [SerializeField]
        List<Modifier> tileModifiers;
        public virtual List<Modifier> TileModifiers { get { return new List<Modifier>(tileModifiers); } }
        [SerializeField]
        List<Modifier> ownerModifiers;
        public virtual List<Modifier> OwnerModifiers { get { return new List<Modifier>(ownerModifiers); } }

        public LoggingFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];

            loggingGraphicName = entry["iconName"];
            validLandNames = entry.ParseList("startLand");
            completeLandNames = entry.ParseList("endLand");

            loggingTime = new Cost(GoodType.Turns, StatType.LoggingTime, Int32.Parse(entry["loggingTime"]), name);
            loggingLaborCost = new Cost(GoodType.Labor, StatType.LoggingLaborCost, Int32.Parse(entry["loggingLaborCost"]), name);

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.MaxCondition, Int32.Parse(entry["maxCondition"]), name));
            tileModifiers.Add(new Modifier(StatType.LoggingWoodPerYield, Int32.Parse(entry["loggingWoodRate"]), name));
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return loggingGraphic;
        }

        public override void OnDatabaseInitialization()
        {
            loggingGraphic = GameDatabase.Instance.TileGraphics[loggingGraphicName];

            validLands = new List<LandType>();
            for (int i = 0; i < validLandNames.Count; i++)
            {
                validLands.Add(GameDatabase.Instance.Lands[validLandNames[i]]);
            }

            completeLands = new List<LandType>();
            for (int i = 0; i < completeLandNames.Count; i++)
            {
                completeLands.Add(GameDatabase.Instance.Lands[completeLandNames[i]]);
            }
        }

        public override void OnActivation(HexTile tile)
        {
            tile.TerrainGraphics.Add(TileLayers.Improvements, loggingGraphic);
            tile.Counters.AddCounter(loggingTime.ModifiedBy,loggingTime.ModifiedValue(tile.Owner.Stats));
            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.ProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingWoodRate], name));

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {
                if (tile.Counters[loggingTime.ModifiedBy] < loggingTime.ModifiedValue(tile.Owner.Stats))
                {
                    tile.Owner.Goods.AddFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
                    tile.Owner.Goods.AddFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingWoodRate], name));
                }
                else if (tile.Counters[loggingTime.ModifiedBy] >= loggingTime.ModifiedValue(tile.Owner.Stats))
                    OnLoggingComplete(tile);
        }

        public override void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnLoggingComplete(HexTile tile)
        {
            for (int i = 0; i < validLands.Count; i++)
            {
                if (validLands[i] == tile.Land)
                {
                    tile.Land = completeLands[i];
                }
            }

            tile.Feature = FeatureType.Null;
        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Improvements);
            tile.Owner.Goods.RemoveFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Counters.RemoveCounter(loggingTime.ModifiedBy);
            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);
            tile.Work.ResetSeasons();
            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
        }

        public void Pause(HexTile tile)
        {
            tile.Counters.PauseCounter(true, loggingTime.ModifiedBy);

            tile.Owner.Goods.RemoveFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.ProcessNow(loggingLaborCost.ReverseModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.RemoveFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingWoodRate], name));

            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);

            tile.Work.Paused = true;

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public void Resume(HexTile tile)
        {
            tile.Counters.PauseCounter(false, loggingTime.ModifiedBy);

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.ProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));

            tile.Owner.Goods.AddFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingWoodRate], name));

            tile.Work.Paused = false;

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public bool CanResume(HexTile tile)
        {
            if (tile.Owner.Goods.CanProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats)))
                return true;
            else
                return false;
        }

        public int GetWorkUtility(HexTile tile)
        {
            return 1 * tile.Stats[StatType.QuarryingStoneRate];
        }

        public int GetLaborWorkCosts(HexTile tile)
        {
            return loggingLaborCost.ModifiedValue(tile.Owner.Stats);
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            if (tile.Work.Paused)
                return GameDatabase.Instance.TileGraphics["Icon_16px_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["Icon_16px_ClearForest"];
        }

        public bool CanPlace(HexTile tile)
        {
            if (validLands.Contains(tile.Land) && tile.Owner.Goods.CanProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats)))
                return true;
            else
                return false;
        }

        public void Place(HexTile tile)
        {
            tile.Feature = this;
        }

        public void WorkSeason(HexTile tile, Seasons season, bool work)
        {
            tile.Work.SetSeasonWorkable(season, work);

            if (TurnManager.Instance.CurrentTurn.Season == season && tile.Work.Paused != work)
            {
                WorkCommand newWork = new WorkCommand();
                 
            }
        }
    }
}