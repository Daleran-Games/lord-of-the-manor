using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using UnityEngine.UI;

namespace DaleranGames.UI
{
    public class ElevationOverlay : BaseOverlay
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void SetLabel(HexTile tile)
        {
            overlay.SetLabelIcon(tile,icon);
            overlay.SetLabelNunmber(tile, tile.Elevation);
        }
    }

}
