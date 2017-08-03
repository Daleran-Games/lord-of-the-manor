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

        public override List<Modifier> TileModifiers { get { return new List<Modifier>(0); } }
        public override List<Modifier> OwnerModifiers { get { return new List<Modifier>(0); } }
        public override CostCollection Costs { get { return new CostCollection(); } }

        public override bool CanSwitchToActivity(CommandType type, HexTile tile)
        {
            return false;
        }

        public override TileGraphic GetIconGraphic(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public override void SwitchToActivity(CommandType type, HexTile tile)
        {
            
        }
    }
}
