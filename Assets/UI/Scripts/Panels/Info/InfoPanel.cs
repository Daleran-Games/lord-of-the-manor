using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using System;

namespace DaleranGames.UI
{
    public class InfoPanel : Singleton<InfoPanel>
    {
        protected InfoPanel() { }

        [SerializeField]
        RectTransform infoPanelRect;
        RectTransform thisTransform;

        [SerializeField]
        TextMeshProUGUI infoPanelText;

        [SerializeField]
        bool isActive = false;
        public bool IsActive { get { return isActive; } }

        [SerializeField]
        string selectionRing;
        TileGraphic selectionRingGraphic;

        [SerializeField]
        HexTile selectedTile = null;
        public HexTile SelectedTile
        {
            get { return selectedTile; }
            protected set
            {
                if (TileDeselected != null)
                    TileDeselected(selectedTile);

                selectedTile = value;

                if (TileSelected != null)
                    TileSelected(selectedTile);
            }
        }

        public event Action<HexTile> TileSelected;
        public event Action<HexTile> TileDeselected;

        [SerializeField]
        float timeSkip = 0.1f;
        float lastClickTime;

        // Use this for initialization
        void Start()
        {
            infoPanelRect.gameObject.SetActive(false);
            isActive = false;
            SelectedTile = null;
            selectionRingGraphic = GameDatabase.Instance.TileGraphics[selectionRing];

            thisTransform = GetComponent<RectTransform>();

            GameManager.Instance.Play.StateEnabled += OnGameStart;

            if (GameManager.Instance.CurrentState == GameManager.Instance.Play)
                OnGameStart(GameManager.Instance.CurrentState);
        }

        void OnGameStart(GameState state)
        {
            HexCursor.Instance.HexTileEntered += OnHexTileEnter;
            HexCursor.Instance.HexTileExited += OnHexTileExit;
            HexCursor.Instance.HexTileLMBClicked += OnLeftTileClick;
            MouseCursor.Instance.RMBClick.MouseButtonUp += OnRightClick;
        }

        protected override void OnDestroy()
        {
            HexCursor.Instance.HexTileEntered -= OnHexTileEnter;
            HexCursor.Instance.HexTileExited -= OnHexTileExit;
            HexCursor.Instance.HexTileLMBClicked -= OnLeftTileClick;
            GameManager.Instance.Play.StateEnabled -= OnGameStart;
            MouseCursor.Instance.RMBClick.MouseButtonUp -= OnRightClick;
        }

        public void ForceInfoPanelUpdate()
        {
            OnHexTileEnter(HexCursor.Instance.CurrentTile);
        }

        void OnHexTileEnter(HexTile tile)
        {
            // Think about adding a delay 0.3-0.5s delay
            if (tile != null && selectedTile == null)
            {
                ShowInfoPanel(tile);
                
            }
        }

        void OnHexTileExit(HexTile tile)
        {
            if (isActive && selectedTile == null) 
            {
                HideInfoPanel();
            }
        }

        void OnLeftTileClick(HexTile tile)
        {
            if (Time.time - lastClickTime > timeSkip && !CommandMediator.Instance.ActiveMode && tile != SelectedTile)
            {
                if (SelectedTile != null)
                    SelectedTile.UIGraphics.Remove(TileLayers.Fog);


                lastClickTime = Time.time;
                tile.UIGraphics.Add(TileLayers.Fog, selectionRingGraphic);
                SelectedTile = tile;
                ShowInfoPanel(tile);
            }
        }

        void OnRightClick()
        {
            if (selectedTile != null && !CommandMediator.Instance.ActiveMode)
            {
                HideInfoPanel();
                SelectedTile.UIGraphics.Remove(TileLayers.Fog);
                SelectedTile = null;
                ForceInfoPanelUpdate();
            }
        }

        void ShowInfoPanel (HexTile tile)
        {
            isActive = true;
            infoPanelRect.gameObject.SetActive(true);
            infoPanelText.text = tile.Info;
            LayoutRebuilder.ForceRebuildLayoutImmediate(thisTransform);
        }

        void HideInfoPanel ()
        {
            isActive = false;
            infoPanelRect.gameObject.SetActive(false);
        }

    }
}
