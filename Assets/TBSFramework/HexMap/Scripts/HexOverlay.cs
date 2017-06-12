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
        Canvas canvas;
        Transform overlay;
        HexGrid grid;
        bool labelsExsist = false;
        Text[,] labels;

        private void Awake()
        {
            grid = FindObjectOfType<HexGrid>();
            overlay = gameObject.transform.GetChild(0);
            canvas = gameObject.GetRequiredComponent<Canvas>();

            grid.MapGenerationComplete += CreateLabels;
        }


        private void OnDestroy()
        {
            grid.MapGenerationComplete -= CreateLabels;
        }

        Text CreateLabel (Vector3 position, string text)
        {
            Text label = Instantiate<Text>(cellLabel);
            label.rectTransform.SetParent(overlay, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            label.text = text;
            return label;
        }

        public void CreateLabels ()
        {
            if (labelsExsist)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    for (int x = 0; x < grid.Width; x++)
                    {
                        //labels[x, y].text = "H:" + grid[x, y].Elevation.ToString() + "\n" + "M:" + grid[x, y].Moisture.ToString();
                        labels[x, y] =  CreateLabel(grid[x, y].Position, "(" + x + "," + y + ")" + "\n" + grid[x, y].Coord.ToString());
                    }
                }
            } else
            {
                labels = new Text[grid.Width,grid.Height];

                for (int y = 0; y < grid.Height; y++)
                {
                    for (int x = 0; x < grid.Width; x++)
                    {
                        //labels[x, y] = CreateLabel(grid[x, y].Position, "H:" + grid[x, y].Elevation.ToString() + "\n" + "M:" + grid[x, y].Moisture.ToString());
                        labels[x, y] =  CreateLabel(grid[x, y].Position, "(" + x + "," + y + ")" + "\n" + grid[x, y].Coord.ToString());
                    }
                }

                labelsExsist = true;
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
