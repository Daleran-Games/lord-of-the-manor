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
#pragma warning disable 0649
        [Header("Settings")]
        [SerializeField]
        GameObject tileLabel;

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
#pragma warning restore 0649

        public enum Overlays
        {
            None,
            Elevation,
            Mositure,
            Coordinate
        }

        Canvas canvas;
        HexGrid grid;
        HexStrategyCamera hexCam;

        Text[,] labels;

        bool labelsExsist = false;
        public bool OverlayActive { get { return overlay.gameObject.activeInHierarchy; } }

        private void Awake()
        {
            grid = FindObjectOfType<HexGrid>();
            hexCam = FindObjectOfType<HexStrategyCamera>();
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
            GameObject go = Instantiate<GameObject>(tileLabel);
            Text label = go.GetComponentInChildren<Text>();
            RectTransform rect = go.GetRequiredComponent<RectTransform>();
            rect.SetParent(overlay, false);
            rect.anchoredPosition = new Vector2(position.x, position.y);
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
                DisplayNone();
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
                        labels[x, y].text = grid[x, y].Elevation.ToString();
                        labels[x, y].fontSize = 6;
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
                        labels[x, y].text = grid[x, y].Moisture.ToString();
                        labels[x, y].fontSize = 6;
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
                        labels[x, y].text = grid[x, y].HexPosition.ToStringOnSeparateLines();
                        labels[x, y].fontSize = 4;
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
