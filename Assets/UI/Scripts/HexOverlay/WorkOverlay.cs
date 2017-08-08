using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using UnityEngine.UI;
using DaleranGames.IO;

namespace DaleranGames.UI
{
    public class WorkOverlay : BaseOverlay
    {

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            FeatureType.WorkIconChanged += UpdateLabel;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            FeatureType.WorkIconChanged -= UpdateLabel;
        }

        protected override void SetLabel(HexTile tile)
        {
            overlay.SetLabelIcon(tile, tile.Feature.GetWorkIcon(tile));
        }

        protected void UpdateLabel(HexTile tile, TileGraphic icon)
        {
            if (overlayToggle.isOn)
            {
                overlay.ClearLabelIcon(tile);
                overlay.SetLabelIcon(tile, icon);
            }

        }
    }
}
