using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class FarmFeature : FeatureType, ICancelable, IWorkable, IPlaceable
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
        Cost fallowTime;

        [SerializeField]
        Cost buildLaborCost;
        [SerializeField]
        Cost cultivateLaborCost;
        [SerializeField]
        Cost harvestLaborCost;

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

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.MaxCondition, Int32.Parse(entry["maxCondition"]), name));
            tileModifiers.Add(new Modifier(StatType.FarmingRate, Int32.Parse(entry["farmFoodRate"]), name));

        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return harvestGraphic;
        }

        public override void OnDatabaseInitialization()
        {
            harvestGraphic = GameDatabase.Instance.TileGraphics[harvestGraphicName];
        }

        public override void OnActivation(HexTile tile)
        {
            tile.TerrainGraphics.Add(TileLayers.Improvements, sowingGraphic);
            tile.Owner.Goods.TryProcessNow(buildLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Counters.AddCounter(StatType.FarmGrowTime);

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

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
            tile.TerrainGraphics.Remove(TileLayers.Improvements);

            tile.Counters.RemoveCounter(StatType.FarmGrowTime);
            tile.Counters.RemoveCounter(StatType.FarmFallowTime);

            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);
        }

        public bool CanCancel(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public void Cancel(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public void Pause(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public bool CanResume(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public void Resume(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public TileGraphic GetWorkIcon(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public bool CanPlace(HexTile tile)
        {
            throw new NotImplementedException();
        }

        public void Place(HexTile tile)
        {
            throw new NotImplementedException();
        }
    }
}
