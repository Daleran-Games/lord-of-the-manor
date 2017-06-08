using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DaleranGames.TBSFramework
{

    public class HexOverlay : MonoBehaviour
    {

        [SerializeField]
        Text cellLabel;


        Transform overlay;
        HexGrid grid;

        private void Awake()
        {
            grid = GetComponentInParent<HexGrid>();
            overlay = gameObject.transform.GetChild(0);

        }

        void CreateLabel (Vector3 position, string text)
        {
            Text label = Instantiate<Text>(cellLabel);
            label.rectTransform.SetParent(overlay, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            label.text = text;
        }

        public void CreateLabels (HexCell[,] cells)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    //CreateLabel(cells[x, y].Position, "H:" + cells[x, y].Elevation.ToString() + "\n" + "M:" + cells[x, y].Moisture.ToString());
                    CreateLabel(cells[x, y].Position, "(" + x + ","+y+")" + "\n" + cells[x, y].Coord.ToString());
                }
            }
        }

        public void DeleteLabels ()
        {
            overlay.transform.ClearChildren();
        }

        public void ToggleOverlay ()
        {
            if (overlay.gameObject.activeInHierarchy == true)
                overlay.gameObject.SetActive(false);
            else
                overlay.gameObject.SetActive(true);
        }
    }
}
