using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class NullFeature : FeatureType
    {
        public NullFeature ()
        {
            name = "Null Feature";
            id = -1;
            type = "NullFeature";

        }

        public override TileGraphic GetMainGraphic(HexTile tile)
        {
            return TileGraphic.Clear;
        }

    }
}
