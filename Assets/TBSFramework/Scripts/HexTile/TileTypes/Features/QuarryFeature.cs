using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class QuarryFeature : FeatureType, ICancelable, IWorkable, IPlaceable
    {
        [SerializeField]
        string quarryGraphicName;
        TileGraphic quarryGraphic;

        [SerializeField]
        Cost quarryTime;

        [SerializeField]
        Cost buildLaborCost;

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

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.MaxCondition, Int32.Parse(entry["maxCondition"]), name));
            tileModifiers.Add(new Modifier(StatType.QuarryingRate, Int32.Parse(entry["quarryStoneRate"]), name));
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return quarryGraphic;
        }

        public override void OnDatabaseInitialization()
        {
            quarryGraphic = GameDatabase.Instance.TileGraphics[quarryGraphicName];
        }

        public override void OnActivation(HexTile tile)
        {
            tile.TerrainGraphics.Add(TileLayers.Improvements, quarryGraphic);
            tile.Owner.Goods.TryProcessNow(buildLaborCost.ModifiedTransaction(tile.Owner.Stats));
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
