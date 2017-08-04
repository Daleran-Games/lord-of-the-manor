using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.TBSFramework
{
    public interface IUpgradeable : IWorkable, ICancelable
    {
        bool CanUpgrade(HexTile tile);
        void Upgrade(HexTile tile);
        FeatureType UpgradedFeature { get; }
    }
}