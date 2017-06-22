﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewImprovementType", menuName = "DaleranGames/TBS/Tile Types/Improvement Type", order = 0)]
    public class ImprovementType : TileType
    {
        [SerializeField]
        protected Vector2Int atlasCoord = new Vector2Int(0, 0);
        public virtual Vector2Int IconGraphic {  get { return atlasCoord; } }

        [SerializeField]
        protected List<string> validLandToBuild;

        [SerializeField]
        protected string upgradedImprovement;
       
        public override void OnActivation(HexTile tile)
        {
            base.OnActivation(tile);
            tile.AddGraphic(HexLayers.Improvements, atlasCoord);
            //Debug.Log("Added atlas coord");
        }

        public override void OnGameStart(HexTile tile)
        {

        }

        public override void OnTurnChange(BaseTurn turn, HexTile tile)
        {

        }

        public override void OnDeactivation(HexTile tile)
        {
            tile.RemoveGraphic(HexLayers.Improvements);
        }

        public virtual bool CheckIfCanBuild (HexTile tile)
        {
            if (validLandToBuild.Contains(tile.Land.name) && tile.Improvement == null)
                return true;

            return false;
        }

    }
}