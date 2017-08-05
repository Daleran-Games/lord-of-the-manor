using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class DwellingFeature : FeatureType, IUpgradeable, ICancelable
    {
        [SerializeField]
        Cost razeLaborCost;
        [SerializeField]
        List<Modifier> tileModifiers;
        public virtual List<Modifier> TileModifiers { get { return new List<Modifier>(tileModifiers); } }
        [SerializeField]
        List<Modifier> ownerModifiers;
        public virtual List<Modifier> OwnerModifiers { get { return new List<Modifier>(ownerModifiers); } }
        [SerializeField]
        string dwellingGraphicName;
        TileGraphic dwellingGraphic;


        public DwellingFeature(CSVEntry entry)
        {
            this.id = entry.ID+1000;
            name = entry["name"];
            type = entry["type"];

            dwellingGraphicName = entry["iconName"];

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));
            //occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.MaxCondition, Int32.Parse(entry["maxCondition"]), name));

            ownerModifiers.Add(new Modifier(StatType.MaxPopulation, Int32.Parse(entry["maxPopulation"]), name));
            ownerModifiers.Add(new Modifier(StatType.MaxFood, Int32.Parse(entry["maxFood"]), name));
            ownerModifiers.Add(new Modifier(StatType.MaxWood, Int32.Parse(entry["maxWood"]), name));
            ownerModifiers.Add(new Modifier(StatType.MaxStone, Int32.Parse(entry["maxStone"]), name));

            razeLaborCost = new Cost(GoodType.Labor, StatType.RazeLaborCost, Int32.Parse(entry["razeLaborCost"]), name);
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return dwellingGraphic;
        }

        public override void OnDatabaseInitialization()
        {
            dwellingGraphic = GameDatabase.Instance.TileGraphics[dwellingGraphicName];
        }

        public override void OnActivation(HexTile tile)
        {
            tile.TerrainGraphics.Add(TileLayers.Improvements, dwellingGraphic);
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
            if (tile.Owner.Goods.CanProcessNow(razeLaborCost.ModifiedTransaction(tile.Owner.Stats)))
                return true;
            else
                return false;
        }

        public void Cancel(HexTile tile)
        {
            tile.Owner.Goods.TryProcessNow(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Feature = FeatureType.Null;
        }

        public bool CanUpgrade(HexTile tile)
        {
            return false;
        }

        public void Upgrade(HexTile tile)
        {
            throw new NotImplementedException();
        }
    }
}
