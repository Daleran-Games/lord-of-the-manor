using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    public interface ICancelable : ICommandable
    {
        bool CanCancel(HexTile tile);
        void Cancel(HexTile tile);
    }
}
