using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class RazeFeature : FeatureType, ICancelable, IWorkable, IPlaceable
    {

        [SerializeField]
        Cost razeTimeCost;
        [SerializeField]
        Cost razeLaborCost;

        [SerializeField]
        string builtFeatureName;
        FeatureType builtFeature;


        public RazeFeature(CSVEntry entry)
        {
            this.id = entry.ID;
            name = "Raze " + entry["name"];
            type = entry["type"];
            builtFeatureName = entry["name"];

            razeTimeCost = new Cost(GoodType.Turns, StatType.RazeTime, Int32.Parse(entry["razeTime"]), name);
            razeLaborCost = new Cost(GoodType.Labor, StatType.RazeLaborCost, Int32.Parse(entry["razeLaborCost"]), name);
        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_Cancel"];
        }

        #region Tile Callbacks
        public override void OnDatabaseInitialization()
        {
            builtFeature = GameDatabase.Instance.Features[builtFeatureName];

        }

        public override void OnActivation(HexTile tile)
        {
            if (razeTimeCost.ModifiedValue(tile.Owner.Stats) < 1)
            {
                tile.Owner.Goods.ProcessNow(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
                OnRazeCompleted(tile);
            }
            else
            {
                tile.Owner.Goods.ProcessNow(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
                tile.Owner.Goods.AddFuture(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
                tile.Counters.AddCounter(razeTimeCost.ModifiedBy);
                RaiseWorkIconChangeEvent(tile,GetWorkIcon(tile));
            }

        }

        public override void OnTurnEnd(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnTurnSetUp(BaseTurn turn, HexTile tile)
        {

                if (tile.Counters[razeTimeCost.ModifiedBy] < razeLaborCost.ModifiedValue(tile.Owner.Stats))
                    tile.Owner.Goods.AddFuture(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
                else if (tile.Counters[razeTimeCost.ModifiedBy] >= razeLaborCost.ModifiedValue(tile.Owner.Stats))
                    OnRazeCompleted(tile);

        }

        public override void OnTurnStart(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnRazeCompleted(HexTile tile)
        {
            tile.Counters.RemoveCounter(razeTimeCost.ModifiedBy);
            builtFeature.OnDeactivation(tile);
            tile.Feature = FeatureType.Null;
            
        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.Owner.Goods.RemoveFuture(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
            RaiseWorkIconChangeEvent(tile, TileGraphic.Clear);
        }

        public void Cancel(HexTile tile)
        {
            tile.Owner.Goods.ProcessNow(razeLaborCost.ReverseModifiedTransaction(tile.Owner.Stats));
            tile.SwitchFeatureWithNoActiviation(builtFeature);
        }

        public bool CanCancel(HexTile tile)
        {
            return true;
        }

        public void Pause(HexTile tile)
        {
            tile.Counters.PauseCounter(true, razeTimeCost.ModifiedBy);
            tile.Owner.Goods.ProcessNow(razeLaborCost.ReverseModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.RemoveFuture(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Paused = true;
            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public void Resume(HexTile tile)
        {
            tile.Counters.PauseCounter(false, razeTimeCost.ModifiedBy);
            tile.Owner.Goods.ProcessNow(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Owner.Goods.AddFuture(razeLaborCost.ModifiedTransaction(tile.Owner.Stats));
            tile.Paused = false;
            RaiseWorkIconChangeEvent(tile, GetWorkIcon(tile));
        }

        public int GetWorkUtility(HexTile tile)
        {
            return 99;
        }

        public int GetLaborWorkCosts(HexTile tile)
        {
            return razeLaborCost.ModifiedValue(tile.Owner.Stats);
        }

        public bool CanResume(HexTile tile)
        {
            return true;
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["UIAtlas_SmallIcon_Sleep"];
            else
                return GameDatabase.Instance.TileGraphics["UIAtlas_SmallIcon_Cancel"];
        }

        public bool CanPlace(HexTile tile)
        {
            return CanResume(tile);
        }

        public void Place(HexTile tile)
        {
            tile.SwitchFeatureWithNoDeactiviation(this);
        }
        #endregion

    }
}
