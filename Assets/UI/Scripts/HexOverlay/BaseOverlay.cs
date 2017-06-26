using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{

    public abstract class BaseOverlay : MonoBehaviour
    {

        protected HexGrid grid;
        protected HexGridOverlay overlay;

        protected Toggle overlayToggle;

        [SerializeField]
        protected Vector2Int icon = Vector2Int.zero;

        protected virtual void Awake()
        {
            grid = FindObjectOfType<HexGrid>();
            overlay = FindObjectOfType<HexGridOverlay>();
            overlayToggle = gameObject.GetRequiredComponent<Toggle>();

            overlayToggle.onValueChanged.AddListener(DisplayOveraly);
        }

        protected virtual void OnDestroy()
        {
            overlayToggle.onValueChanged.RemoveListener(DisplayOveraly);
        }

        protected abstract void SetLabel(HexTile tile);

        protected virtual void DisplayOveraly (bool on)
        {
            if (on == true)
            {
                //StopCoroutine("ClearLabels");
                //StartCoroutine("DisplayLabels");
                DisplayLabels();
            } else
            {
                //StopCoroutine("DisplayLabels");
                //StartCoroutine("ClearLabels");
                ClearLabels();
            }
        }

        protected virtual void DisplayLabels()
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    SetLabel(grid[x, y]);
                }
            }
            //overlay.CommitUVChanges();
        }

        protected virtual void ClearLabels()
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    overlay.ClearLabelNunmber(grid[x, y]);
                    overlay.ClearLabelIcon(grid[x, y]);
                }
            }
            //overlay.CommitUVChanges();
        }

    }
}