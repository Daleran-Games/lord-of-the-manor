using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    public interface IWorkable
    {
        void Pause(HexTile tile);
        void Resume(HexTile tile);
    }
}