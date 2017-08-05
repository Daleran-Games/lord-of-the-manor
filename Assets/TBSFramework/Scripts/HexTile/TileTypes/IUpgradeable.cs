using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.TBSFramework
{
    public interface IUpgradeable
    {
        bool CanUpgrade(HexTile tile);
        void Upgrade(HexTile tile);
    }
}