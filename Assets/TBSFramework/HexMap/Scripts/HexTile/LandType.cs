using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;
using System;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewLandType", menuName = "DaleranGames/TBS/Tile Types/Land Type", order = 0)]
    public class LandType : TileType
    {
        [SerializeField]
        protected Vector2Int atlasCoord = new Vector2Int(0, 0);

        [SerializeField]
        protected string clearedTile = "None";

        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.AddGraphic(HexLayers.Land, atlasCoord);
        }

        public override void OnGameStart(HexTile tile)
        {

        }

        public override void OnTurnChange(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.RemoveGraphic(HexLayers.Land);
        }

        public virtual void ClearTile (HexTile tile)
        {
            if (clearedTile != "None")
                tile.Land = GameDatabase.Instance.GetDatabaseObject<LandType>(clearedTile);
        }

        public virtual bool CanClear()
        {
            if (clearedTile != "None")
                return true;
            else
                return false;
        }

    }
}
