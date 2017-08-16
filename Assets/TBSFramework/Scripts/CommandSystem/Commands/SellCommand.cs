using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class SellCommand : Command
    {
        GoodType good;

        public SellCommand(GoodType good) : base()
        {
            this.good = good;
        }

        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            return false;
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {

        }
    }
}
