using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;

namespace DaleranGames.UI
{
    public class TooltipController : Singleton<TooltipController>
    {
        protected TooltipController( ) { }

        [SerializeField]
        RectTransform canvasRect;
        Canvas canvas;

        [SerializeField]
        RectTransform tooltipRect;
        VerticalLayoutGroup verticalLayout;

        [SerializeField]
        TextMeshProUGUI tooltipText;

        [SerializeField]
        Vector2 offset = new Vector2(32f, -32f);

        [SerializeField]
        bool isActive = false;
        public bool IsActive { get { return isActive; } }

        [SerializeField]
        bool hexCursorOverride = false;
        public bool HexCursorOverride { get { return hexCursorOverride; } set { hexCursorOverride = value; } }

        // Use this for initialization
        void Start()
        {
            tooltipRect.gameObject.SetActive(false);
            isActive = false;
            hexCursorOverride = false;
            canvas = canvasRect.GetComponent<Canvas>();
            verticalLayout = tooltipRect.GetComponent<VerticalLayoutGroup>();

            GameManager.Instance.Play.StateEnabled += OnGameStart;

            if (GameManager.Instance.CurrentState == GameManager.Instance.Play)
                OnGameStart(GameManager.Instance.CurrentState);
        }

        void OnGameStart(GameState state)
        {
            HexCursor.Instance.HexTileEntered += OnHexTileEnter;
            HexCursor.Instance.HexTileExited += OnHexTileExit;
        }

        protected override void OnDestroy()
        {
            HexCursor.Instance.HexTileEntered -= OnHexTileEnter;
            HexCursor.Instance.HexTileExited -= OnHexTileExit;
            GameManager.Instance.Play.StateEnabled -= OnGameStart;
        }



        // Update is called once per frame
        void Update()
        {
            if (IsActive)
            {
                CheckTooltipAnchors();
            }
        }

        void CheckTooltipAnchors()
        {
            Vector3 mousePos = MouseCursor.Instance.Position;
            float screenHalfX = Screen.width - tooltipRect.rect.width;
            float screenHalfY = tooltipRect.rect.height;

            if (mousePos.x >= screenHalfX) // Right Side
            {
                if (mousePos.y >= screenHalfY) // Upper Right
                {

                    SetRectAnchorsAndPicot(Vector2.one);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = new Vector2(-offset.x, offset.y);
                    verticalLayout.childAlignment = TextAnchor.UpperRight;
                } else // Lower Right
                {
                    SetRectAnchorsAndPicot(Vector2.right);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = new Vector2(-offset.x, -offset.y);
                    verticalLayout.childAlignment = TextAnchor.UpperRight;
                }
            } else // Left Side
            {
                if (mousePos.y >= screenHalfY) // Upper Left
                {
                    SetRectAnchorsAndPicot(Vector2.up);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = offset;
                    verticalLayout.childAlignment = TextAnchor.UpperLeft;
                }
                else // Lower Left
                {
                    SetRectAnchorsAndPicot( Vector2.zero);
                    canvasRect.anchoredPosition = Vector2.zero;
                    tooltipRect.anchoredPosition = new Vector2(offset.x, -offset.y);
                    verticalLayout.childAlignment = TextAnchor.UpperLeft;
                }
            }

        }

        void SetRectAnchorsAndPicot (Vector2 vec)
        {
            canvasRect.anchorMin = vec;
            canvasRect.anchorMax = vec;
            canvasRect.pivot = vec;
            tooltipRect.anchorMin = vec;
            tooltipRect.anchorMax = vec;
            tooltipRect.pivot = vec;
            
        }

        public void ForceHexTileTooltipUpdate()
        {
            OnHexTileEnter(HexCursor.Instance.CurrentTile);
        }

        void OnHexTileEnter(HexTile tile)
        {
            // Think about adding a delay 0.3-0.5s delay
            if (!HexCursorOverride && tile != null)
            {
                isActive = true;
                tooltipRect.gameObject.SetActive(true);

                if (CommandMediator.Instance.ActiveMode && CommandMediator.Instance.CurrentCommand != null)
                    tooltipText.text = CommandMediator.Instance.CurrentCommand.GetInfo(tile, GroupManager.Instance.PlayerGroup);
                else
                    tooltipText.text = tile.Info;
            }
        }

        void OnHexTileExit(HexTile tile)
        {
            if (!HexCursorOverride && isActive)
            {
                isActive = false;
                tooltipRect.gameObject.SetActive(false);
            }

        }

        public void ShowTooltip(string text)
        {
                isActive = true;
                HexCursorOverride = true;
                tooltipRect.gameObject.SetActive(true);
                tooltipText.text = text;
        }

        public void HideTooltip()
        {
                isActive = false;
                HexCursorOverride = false;
                tooltipRect.gameObject.SetActive(false);

        }
    }
}

