using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class DwellingFeature : FeatureType, IUpgradeable, ICancelable, IPlaceable, IBuildable
    {
        [SerializeField]
        List<Modifier> tileModifiers;
        public virtual List<Modifier> TileModifiers { get { return new List<Modifier>(tileModifiers); } }
        [SerializeField]
        List<Modifier> ownerModifiers;
        public virtual List<Modifier> OwnerModifiers { get { return new List<Modifier>(ownerModifiers); } }
        [SerializeField]
        string dwellingGraphicName;
        TileGraphic dwellingGraphic;

        [SerializeField]
        BuildFeature buildFeature;
        public virtual BuildFeature Build { get { return buildFeature; } }

        [SerializeField]
        RazeFeature razeFeature;
        [SerializeField]
        UpgradeFeature upgradeFeature;

        [SerializeField]
        bool upgradeable = false;


        public DwellingFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];

            dwellingGraphicName = entry["iconName"];

            buildFeature = new BuildFeature(entry);
            razeFeature = new RazeFeature(entry);

            if (Boolean.Parse(entry["upgradeable"]))
            {
                upgradeable = true;
                upgradeFeature = new UpgradeFeature(entry);
            }

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            ownerModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));
            //occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            tileModifiers.Add(new Modifier(StatType.DefenseBonus, Int32.Parse(entry["defenseBonus"]), name));
            tileModifiers.Add(new Modifier(StatType.MovementCost, Int32.Parse(entry["movementCost"]), name));
            tileModifiers.Add(new Modifier(StatType.MaxCondition, Int32.Parse(entry["maxCondition"]), name));

            ownerModifiers.Add(new Modifier(StatType.MaxPopulation, Int32.Parse(entry["maxPopulation"]), name));
            ownerModifiers.Add(new Modifier(StatType.MaxFood, Int32.Parse(entry["maxFood"]), name));
            ownerModifiers.Add(new Modifier(StatType.GroupWoodRate, Int32.Parse(entry["woodRate"]), name));
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return dwellingGraphic;
        }

        public override void OnDatabaseInitialization()
        {
            dwellingGraphic = GameDatabase.Instance.TileGraphics[dwellingGraphicName];
            buildFeature.OnDatabaseInitialization();
            razeFeature.OnDatabaseInitialization();

            if (upgradeable)
                upgradeFeature.OnDatabaseInitialization();
        }

        public override void OnActivation(HexTile tile)
        {
            tile.TerrainGraphics.Add(TileLayers.Improvements, dwellingGraphic);
            tile.Stats.Add(TileModifiers);
            tile.OwnerModifiers.Add(OwnerModifiers);
            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
            tile.Owner.Home = true;
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
            //Debug.Log("Deactivated");

            tile.TerrainGraphics.Remove(TileLayers.Improvements);
            tile.Stats.Remove(TileModifiers);
            tile.OwnerModifiers.Remove(OwnerModifiers);
            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
            tile.Owner.Home = false;
        }


        public bool CanCancel(HexTile tile)
        {
            return razeFeature.CanPlace(tile);
        }

        public void Cancel(HexTile tile)
        {
            razeFeature.Place(tile);
        }

        public bool CanUpgrade(HexTile tile)
        {
            if (upgradeable)
                return upgradeFeature.CanPlace(tile);
            else
                return false;
        }

        public void Upgrade(HexTile tile)
        {
            if (upgradeable)
                upgradeFeature.Place(tile);
        }

        public TileGraphic GetUpgradeGraphic(HexTile tile)
        {
            if (upgradeable)
                return upgradeFeature.GetMainGraphic(tile);
            else
                return TileGraphic.Clear;
        }

        public bool CanPlace(HexTile tile)
        {
            return buildFeature.CanPlace(tile);
        }

        public void Place(HexTile tile)
        {
            buildFeature.Place(tile);
            CommandMediator.Instance.ExitCommandMode();
        }
    }
}
