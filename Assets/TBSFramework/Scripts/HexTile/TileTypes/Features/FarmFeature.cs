using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class FarmFeature : FeatureType, IWorkable, IPlaceable
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
            tile.Counters.AddCounter(StatType.FarmGrowTime);

            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);

            tile.Owner.Goods.TryProcessNow(buildLaborCost.ModifiedTransaction(tile.Owner.Stats));

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
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["UIAtlas_SmallIcon_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_WorkFarm"];
        }

        public bool CanPlace(HexTile tile)
        {
            if (tile.Owner.Goods.CanProcessNow(buildLaborCost.ModifiedTransaction(tile.Owner.Stats)) && validLands.Contains(tile.Land) && (TurnManager.Instance.CurrentTurn == TurnManager.Instance.Spring || TurnManager.Instance.CurrentTurn == TurnManager.Instance.Fall))
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
