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
        GameObject cellLabel;

        [SerializeField]
        RectTransform overlay;

        [SerializeField]
        [Range (1,16)]
        int displayRange = 5;

        [Header("Overlays")]
        [SerializeField]
        bool overlayEnabled = false;

        [SerializeField]
        Sprite elevationIcon;
        [SerializeField]
        bool elevationOn = false;

        [SerializeField]
        Sprite moistureIcon;
        [SerializeField]
        bool moistureOn = false;
  
        [SerializeField]
        Sprite coordinateIcon;
        [SerializeField]
        bool coordinateOn = false;



        Canvas canvas;
        HexGrid grid;
        HexStrategyCamera camera;

        List<OverlayLabel> labels;

        bool labelsExsist = false;
        public bool OverlayActive { get { return overlay.gameObject.activeInHierarchy; } }

        private void Awake()
        {
            grid = FindObjectOfType<HexGrid>();
            camera = FindObjectOfType<HexStrategyCamera>();
            canvas = gameObject.GetRequiredComponent<Canvas>();
            grid.MapGenerationComplete += CreateLabels;
            camera.CameraCellChanged += OnCameraCellChange;
            labels = new List<OverlayLabel>();
        }

        private void OnDestroy()
        {
            grid.MapGenerationComplete -= CreateLabels;
            camera.CameraCellChanged -= OnCameraCellChange;
        }

        OverlayLabel CreateLabel (Vector3 position)
        {
            GameObject label = Instantiate<GameObject>(cellLabel);
            OverlayLabel overlayLabel = label.GetComponent<OverlayLabel>();
            overlayLabel.Rect.SetParent(overlay, false);
            overlayLabel.MoveToPosition(position);
            return overlayLabel;
        }



        public void CreateLabels ()
        {
            DeleteLabels();

            for(int q = -displayRange; q <= displayRange; q++)
            {
                for (int r = -displayRange; r <= displayRange; r++)
                {
                    for (int s = -displayRange; s <= displayRange; s++)
                    {
                        labels.Add(CreateLabel(HexCoordinates.GetUnityPosition(q, r, s)));
                    }
                }
            }

            labelsExsist = true;
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

        void OnCameraCellChange(HexCell hex)
        {
            overlay.anchoredPosition = hex.Position;
            foreach (OverlayLabel ol in labels)
            {

            }

        }

        void DisplayElevation ()
        {

        }

        void DisplayMoisture ()
        {

        }

        void DisplayCoordinates()
        {

        }


    }
}
