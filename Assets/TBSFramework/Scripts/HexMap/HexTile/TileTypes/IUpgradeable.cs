using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.TBSFramework
{
    public interface IUpgradeable : ICommandable
    {
        bool CanUpgrade(HexTile tile);
        void Upgrade(HexTile tile);
        TileGraphic GetUpgradeGraphic(HexTile tile);
    }
}