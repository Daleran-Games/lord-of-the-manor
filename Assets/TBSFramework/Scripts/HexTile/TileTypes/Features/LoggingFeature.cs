using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LoggingFeature : FeatureType, IWorkable, IPlaceable
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
            tileModifiers.Add(new Modifier(StatType.LoggingRate, Int32.Parse(entry["loggingWoodRate"]), name));
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
            tile.Counters.AddCounter(loggingTime.ModifiedBy);
            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.TryProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingRate] * tile.Stats[StatType.WoodYield], name));
        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {
            if (CanResume(tile))
            {
                if (tile.Counters[loggingTime.ModifiedBy] < loggingTime.ModifiedValue(tile.Owner.Stats))
                {
                    tile.Owner.Goods.AddFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
                    tile.Owner.Goods.AddFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingRate] * tile.Stats[StatType.WoodYield], name));
                }
                else if (tile.Counters[loggingTime.ModifiedBy] >= loggingTime.ModifiedValue(tile.Owner.Stats))
                    OnLoggingComplete(tile);
            }
            else
                Pause(tile);

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
        }

        public void Pause(HexTile tile)
        {
            tile.Counters.PauseCounter(true, loggingTime.ModifiedBy);

            tile.Owner.Goods.RemoveFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.TryProcessNow(loggingLaborCost.ReverseModifiedTransaction(tile.Owner.Stats));

            tile.Owner.Goods.RemoveFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingRate] * tile.Stats[StatType.WoodYield], name));

            tile.Paused = true;
        }

        public void Resume(HexTile tile)
        {
            tile.Counters.PauseCounter(false, loggingTime.ModifiedBy);

            tile.Owner.Goods.TryProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats));

            tile.Owner.Goods.AddFuture(new Transaction(GoodType.Wood, tile.Stats[StatType.LoggingRate] * tile.Stats[StatType.WoodYield], name));

            tile.Paused = false;
        }

        public bool CanResume(HexTile tile)
        {
            if (tile.Owner.Goods.CanProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats)))
                return true;
            else
                return false;
        }

        public TileGraphic GetWorkIcon(HexTile tile)
        {
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["UIAtlas_SmallIcon_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_WorkForest"];
        }

        public bool CanPlace(HexTile tile)
        {
            if (tile.Owner.Goods.CanProcessNow(loggingLaborCost.ModifiedTransaction(tile.Owner.Stats)) && validLands.Contains(tile.Land))
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
    }
}