using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class QuarryFeature : FeatureType, IWorkable, IPlaceable
    {
        [SerializeField]
        string quarryGraphicName;
        TileGraphic quarryGraphic;

        [SerializeField]
        Cost quarryTime;

        [SerializeField]
        Cost quarryLaborCost;

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

        public QuarryFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];

            quarryGraphicName = entry["iconName"];
            validLandNames = entry.ParseList("startLand");
            completeLandNames = entry.ParseList("endLand");

            quarryTime = new Cost(GoodType.Turns, StatType.QuarryTime, Int32.Parse(entry["quarryTime"]), name);
            quarryLaborCost = new Cost(GoodType.Labor, StatType.QuarryLaborCost, Int32.Parse(entry["quarryLaborCost"]), name);

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.MaxCondition, Int32.Parse(entry["maxCondition"]), name));
            tileModifiers.Add(new Modifier(StatType.QuarryingStonePerYield, Int32.Parse(entry["quarryStoneRate"]), name));
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return quarryGraphic;

        }

        public override void OnDatabaseInitialization()
        {
            quarryGraphic = GameDatabase.Instance.TileGraphics[quarryGraphicName];

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
            tile.TerrainGraphics.Add(TileLayers.Improvements, quarryGraphic);

            tile.Counters.AddCounter(quarryTime.ModifiedBy,quarryTime.ModifiedValue(tile.Owner.Stats));
            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.ProcessNow(quarryLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(quarryLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(new Transaction(GoodType.Stone, tile.Stats[StatType.QuarryingStoneRate], name));

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));

        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

                if (tile.Counters[quarryTime.ModifiedBy] < quarryTime.ModifiedValue(tile.Owner.Stats))
                {
                    tile.Owner.Goods.AddFuture(quarryLaborCost.ModifiedTransaction(tile.Owner.Stats));
                    tile.Owner.Goods.AddFuture(new Transaction(GoodType.Stone, tile.Stats[StatType.QuarryingStoneRate], name));
                }
                else if (tile.Counters[quarryTime.ModifiedBy] >= quarryTime.ModifiedValue(tile.Owner.Stats))
                    OnQuarryComplete(tile);


        }

        public override void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnQuarryComplete(HexTile tile)
        {
            for (int i=0;i<validLands.Count; i++)
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
            tile.Owner.Goods.RemoveFuture(quarryLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Counters.RemoveCounter(quarryTime.ModifiedBy);
            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);

            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
        }

        public void Pause(HexTile tile)
        {
            tile.Counters.PauseCounter(true, quarryTime.ModifiedBy);

            tile.Owner.Goods.RemoveFuture(quarryLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.ProcessNow(quarryLaborCost.ReverseModifiedTransaction(tile.Owner.Stats));

            tile.Owner.Goods.RemoveFuture(new Transaction(GoodType.Stone, tile.Stats[StatType.QuarryingStoneRate], name));

            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);

            tile.Paused = true;

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public void Resume(HexTile tile)
        {
            tile.Counters.PauseCounter(false, quarryTime.ModifiedBy);

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.ProcessNow(quarryLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(quarryLaborCost.ModifiedTransaction(tile.Owner.Stats));

            tile.Owner.Goods.AddFuture(new Transaction(GoodType.Stone, tile.Stats[StatType.QuarryingStoneRate], name));

            tile.Paused = false;

            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public bool CanResume(HexTile tile)
        {
            return true;
        }

        public int GetWorkUtility(HexTile tile)
        {
            return 3 * tile.Stats[StatType.QuarryingStoneRate];
        }

        public int GetLaborWorkCosts(HexTile tile)
        {
            return quarryLaborCost.ModifiedValue(tile.Owner.Stats);
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["Icon_16px_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["Icon_16px_Quarry"];
        }

        public bool CanPlace(HexTile tile)
        {
            if (validLands.Contains(tile.Land))
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
