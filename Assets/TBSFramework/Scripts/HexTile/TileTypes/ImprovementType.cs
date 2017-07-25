using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ImprovementType : TileType
    {

        protected ImprovementType()
        {
            id = -1;
            name = "Null Improvment Type";
            type = "ImprovementType";
            iconName = "null";

            tileModifiers = new List<Modifier>(0);
            ownerModifiers = new List<Modifier>(0);
            //occupierModifiers = new Modifier[0];

            buildable = false;
            upgradeable = false;
            razeable = false;
            //workable = false;

        }

        public static readonly ImprovementType Null = new ImprovementType();

        public ImprovementType(CSVData data, int id)
        {

            this.id = id;
            name = data["name", id];
            type = data["type", id];
            iconName = data["iconName", id];

            tileModifiers = Modifier.ParseCSVList(data.ParseList("tileModifierList", id));
            ownerModifiers = Modifier.ParseCSVList(data.ParseList("ownerModifierList", id));
            //occupierModifiers = Modifier.ParseCSVList(data.ParseList("occupierModifierList", id));

            buildable = Boolean.Parse(data["buildable", id]);
            if (buildable)
            {
                buildGraphicName = data["buildGraphic", id];
                buildTime = Int32.Parse(data["buildTime", id]);
                buildCosts = Cost.ParseCSVList(data.ParseList("buildCostList", id));
                validLand = new List<string>(data.ParseList("landList", id));
            }

            upgradeable = Boolean.Parse(data["upgradeable",id]);
            if (upgradeable)
                upgradeName = data["upgradeName", id];

            razeable = Boolean.Parse(data["razeable", id]);
            if(razeable)
            {
                razeTime = Int32.Parse(data["razeTime", id]);
                razeCosts = Cost.ParseCSVList(data.ParseList("razeCostList", id));
                //razeBonuses = Cost.ParseCSVList(data.ParseList("razeBonusList", id));
            }

            /*
            workable = Boolean.Parse(data["workable", id]);
            if (workable)
            {
                workCosts = Cost.ParseCSVList(data.ParseList("workCostList", id));
                workBonuses = Cost.ParseCSVList(data.ParseList("workBonusList", id));
                workModifiers = Modifier.ParseCSVList(data.ParseList("workModifierList", id));
            }
            */
        }

        #region Tile Stats
        [Header("Tile Stats")]
        [SerializeField]
        protected List<Modifier> tileModifiers;
        public virtual ReadOnlyCollection<Modifier> TileModifiers { get { return tileModifiers.AsReadOnly(); } }

        [SerializeField]
        protected List<Modifier> ownerModifiers;
        public virtual ReadOnlyCollection<Modifier> OwnerModifiers { get { return ownerModifiers.AsReadOnly(); } }

        //[SerializeField]
       // protected Modifier[] occupierModifiers;
       // public virtual Modifier[] OccupierModifiers { get { return occupierModifiers; } }

        [Header("Build Stats")]
        [SerializeField]
        protected bool buildable;
        public virtual bool Buildable { get { return buildable; } }

        [SerializeField]
        protected int buildTime;
        public virtual int Builtdime { get { return buildTime; } }

        [SerializeField]
        protected string buildGraphicName;

        protected TileGraphic buildGraphic;
        public TileGraphic BuildGraphic { get { return buildGraphic; } }

        [SerializeField]
        protected List<Cost> buildCosts;
        public virtual ReadOnlyCollection<Cost> BuildCosts { get { return buildCosts.AsReadOnly(); } }

        [SerializeField]
        protected List<string> validLand;
        public virtual ReadOnlyCollection<string>ValidLand { get { return validLand.AsReadOnly(); } }

        [Header("Upgrade Stats")]
        [SerializeField]
        protected bool upgradeable = false;
        public virtual bool Upgradeable { get { return upgradeable; } }

        [SerializeField]
        protected string upgradeName;

        [SerializeField]
        protected ImprovementType upgradedImprovement;
        public virtual ImprovementType UpgradedImprovement { get { return upgradedImprovement; } }

        [Header("Raze Stats")]
        [SerializeField]
        protected bool razeable = false;
        public virtual bool Razeable { get { return razeable; } }

        public int razeTime;
        public virtual int RazeTime { get { return razeTime; } }

        protected List<Cost> razeCosts;
        public virtual ReadOnlyCollection<Cost> RazeCosts { get { return razeCosts.AsReadOnly(); } }

        //protected List<Cost> razeBonuses;
        //public virtual ReadOnlyCollection<Cost> RazeBonuses { get { return razeBonuses.AsReadOnly(); } }

            /*
        [Header("Work Stats")]
        [SerializeField]
        protected bool workable = false;
        public virtual bool Workable { get { return workable; } }

        [SerializeField]
        protected List<Cost> workCosts;
        public virtual ReadOnlyCollection<Cost> WorkCosts { get { return workCosts.AsReadOnly(); } }
        
        [SerializeField]
        protected List<Cost> workBonuses;
        public virtual ReadOnlyCollection<Cost> WorkBonuses { get { return workBonuses.AsReadOnly(); } }

        [SerializeField]
        protected List<Modifier> workModifiers;
        public virtual ReadOnlyCollection<Modifier> WorkModifiers {get { return workModifiers.AsReadOnly(); } }
        */
        

        #endregion

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {
            base.OnDatabaseInitialization();

            if (upgradeable)
                upgradedImprovement = GameDatabase.Instance.Improvements[upgradeName];

            if (buildable)
                buildGraphic = GameDatabase.Instance.TileGraphics[buildGraphicName];
        }

        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Improvements,iconGraphic);

            tile.Stats.Add(tileModifiers);
            tile.OwnerModifiers.Add(ownerModifiers);
        }

        public override void OnGameStart(HexTile tile)
        {
 
        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Improvements);

            tile.Stats.Remove(tileModifiers);
            tile.OwnerModifiers.Remove(ownerModifiers);
        }

        #endregion

    }
}