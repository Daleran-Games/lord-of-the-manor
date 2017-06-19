using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.TBSFramework;
using DaleranGames.Tools;


namespace DaleranGames.UI
{
    public class HexOverlay : MonoBehaviour
    {

        [Header("Settings")]
        [SerializeField]
        Text cellLabel;

        [SerializeField]
        RectTransform overlay;

        [Header("Overlays")]
        [SerializeField]
        Overlays current = Overlays.None;

        [SerializeField]
        Toggle elevationToggle;

        [SerializeField]
        Toggle moistureToggle;

        [SerializeField]
        Toggle coordinateToggle;

        public enum Overlays
        {
            None,
            Elevation,
            Mositure,
            Coordinate
        }

        Canvas canvas;
        HexGrid grid;
        HexStrategyCamera camera;

        Text[,] labels;

        bool labelsExsist = false;
        public bool OverlayActive { get { return overlay.gameObject.activeInHierarchy; } }

        private void Awake()
        {
            grid = FindObjectOfType<HexGrid>();
            camera = FindObjectOfType<HexStrategyCamera>();
            canvas = gameObject.GetRequiredComponent<Canvas>();
            grid.MapGenerationComplete += CreateLabels;

            elevationToggle.onValueChanged.AddListener(DisplayElevation);
            coordinateToggle.onValueChanged.AddListener(DisplayCoordinates);
            moistureToggle.onValueChanged.AddListener(DisplayMoisture);
        }

        private void OnDestroy()
        {
            grid.MapGenerationComplete -= CreateLabels;
        }

        Text CreateLabel(Vector3 position)
        {
            Text label = Instantiate<Text>(cellLabel);
            label.rectTransform.SetParent(overlay, false);
            label.rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            label.text = "";
            return label;
        }


        public void CreateLabels ()
        {
            if (!labelsExsist)
            {
                labels = new Text[grid.Width, grid.Height];
                for (int y = 0; y < grid.Height; y++)
                {
                    for (int x = 0; x < grid.Width; x++)
                    {   
                        labels[x, y] = CreateLabel(grid[x, y].Position);
                    }
                }
                labelsExsist = true;
            } else
            {
                UpdateLabels(current);
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

        public void UpdateLabels (Overlays newOverlay)
        {
            switch (newOverlay)
            {
                case Overlays.Coordinate:
                    DisplayCoordinates(true);
                    break;
                case Overlays.Elevation:
                    DisplayElevation(true);
                    break;
                case Overlays.Mositure:
                    DisplayMoisture(true);
                    break;
                default:
                    DisplayNone();
                    break;
            }
        }

        void DisplayElevation (bool on)
        {
            if (on == true)
            {
                current = Overlays.Elevation;

                if (overlay.gameObject.activeInHierarchy == false)
                    overlay.gameObject.SetActive(true);

                for (int y = 0; y < grid.Height; y++)
                {
                    for (int x = 0; x < grid.Width; x++)
                    {
                        labels[x, y].text = grid[x, y].HexLand.Elevation.ToString();
                        labels[x, y].fontSize = 9;
                    }
                }

            } else if (on == false)
            {
                DisplayNone();
            }
        }

        void DisplayMoisture (bool on)
        {
            if (on == true)
            {
                current = Overlays.Mositure;

                if (overlay.gameObject.activeInHierarchy == false)
                    overlay.gameObject.SetActive(true);

                for (int y = 0; y < grid.Height; y++)
                {
                    for (int x = 0; x < grid.Width; x++)
                    {
                        labels[x, y].text = grid[x, y].HexLand.Moisture.ToString();
                        labels[x, y].fontSize = 9;
                    }
                }

            }
            else if (on == false)
            {
                DisplayNone();
            }
        }

        void DisplayCoordinates(bool on)
        {
            if (on == true)
            {
                current = Overlays.Coordinate;

                if (overlay.gameObject.activeInHierarchy == false)
                    overlay.gameObject.SetActive(true);

                for (int y = 0; y < grid.Height; y++)
                {
                    for (int x = 0; x < grid.Width; x++)
                    {
                        labels[x, y].text = grid[x, y].Coord.ToStringOnSeparateLines();
                        labels[x, y].fontSize = 5;
                    }
                }

            }
            else if (on == false)
            {
                DisplayNone();
            }
        }

        void DisplayNone()
        {
            current = Overlays.None;
            overlay.gameObject.SetActive(false);
        }


    }
}
