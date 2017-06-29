using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LandType : TileType
    {
        [SerializeField]
        protected bool clearable = false;
        public bool Clearable { get { return clearable; } }

        [SerializeField]
        protected string clearedLandName;
        public string ClearedLandName { get { return clearedLandName; } }

        [System.NonSerialized]
        protected LandType clearedLand;
        public LandType ClearedLand { get { return clearedLand; } }

        public LandType(LandType type, int id)
        {
            name = type.Name;
            this.id = id;
            clearable = type.Clearable;
            iconName = type.IconName;
            clearedLandName = type.ClearedLandName;
        }

        public override void OnDatabaseInitialization()
        {

            base.OnDatabaseInitialization();

            if (clearable)
                clearedLand = GameDatabase.Instance.LandTiles.Get(clearedLandName);

        }

        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.TerrainGraphics.Add(TileLayers.Land, iconGraphic);
        }

        public override void OnGameStart(HexTile tile)
        {
            base.OnGameStart(tile);
        }

        public override void OnTurnChange(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.TerrainGraphics.Remove(TileLayers.Land);
        }

        public virtual void ClearTile (HexTile tile)
        {
            if (ClearedLand != null && clearable)
                tile.Land = ClearedLand;
        }

        public override string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }

    }
}
