using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class PlaceCommand : Command
    {
        [SerializeField]
        FeatureType feature;
        IPlaceable placeable;

        public PlaceCommand(string featureName)
        {
            feature = GameDatabase.Instance.Features[featureName];
            placeable = GameDatabase.Instance.Features[featureName] as IPlaceable;

            if (placeable == null)
                Debug.LogError(featureName + " cannot be casted to IPlaceable");
        }

        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            if (placeable.CanPlace(tile) && tile.Owner == owner && (tile.Feature == FeatureType.Null || tile.Feature == null))
                return true;
            else
                return false;
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            placeable.Place(tile);
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return feature.GetMainGraphic(tile);
        }

        public override string GetInfo(HexTile tile, Group group)
        {
            if (tile.Owner != group)
                return ("You do not own "+tile.Land.Name).ToNegativeColor();

            if (tile.Feature != FeatureType.Null && tile.Feature != null)
                return ("There is already something here.").ToNegativeColor();

           if (!placeable.CanPlace(tile))
                return("You canno place "+feature.Name+" here.").ToNegativeColor();

            return ("Place " + feature.Name + " here.").ToPositiveColor();
        }

    }
}
