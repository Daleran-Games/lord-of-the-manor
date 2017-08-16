using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    public interface IWorkable
    {
        void Pause(HexTile tile);
        bool CanResume(HexTile tile);
        void Resume(HexTile tile);
        int GetWorkUtility(HexTile tile);
        int GetLaborWorkCosts(HexTile tile);
        TileGraphic GetWorkIcon(HexTile tile);
    }
}