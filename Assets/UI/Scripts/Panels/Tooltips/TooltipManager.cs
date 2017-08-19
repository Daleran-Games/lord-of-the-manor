using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;

namespace DaleranGames.UI
{
    public class TooltipManager : Singleton<TooltipManager>
    {
        protected TooltipManager( ) { }

        [SerializeField]
        RectTransform tooltipRect;

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
            float screenHalfY = tooltipRect.rect.height*2f;

            if (mousePos.x >= screenHalfX) // Right Side
            {
                if (mousePos.y >= screenHalfY) // Upper Right
                {
                    tooltipRect.pivot = Vector2.one;
                    tooltipRect.anchoredPosition = new Vector2(-offset.x, offset.y);
                } else // Lower Right
                {
                    tooltipRect.pivot = Vector2.right;
                    tooltipRect.anchoredPosition = new Vector2(-offset.x, -offset.y);
                }
            } else // Left Side
            {
                if (mousePos.y >= screenHalfY) // Upper Left
                {
                    tooltipRect.pivot = Vector2.up;
                    tooltipRect.anchoredPosition = offset;
                }
                else // Lower Left
                {
                    tooltipRect.pivot = Vector2.zero;
                    tooltipRect.anchoredPosition = new Vector2(offset.x, -offset.y);
                }
            }
        }

        void OnHexTileEnter(HexTile tile)
        {
            // Think about adding a delay 0.3-0.5s delay
            if (!HexCursorOverride && tile != null)
            {
                isActive = true;
                tooltipRect.gameObject.SetActive(true);

                if (CommandMediator.Instance.ActiveMode && CommandMediator.Instance.CurrentCommand != null)
                    tooltipText.text = CommandMediator.Instance.CurrentCommand.Info;
                else
                    tooltipText.text = tile.Info;
            }
        }

        void OnHexTileExit(HexTile tile)
        {
            isActive = false;
            tooltipRect.gameObject.SetActive(false);
        }

        public void ShowTooltip(string text)
        {
            isActive = true;
            HexCursorOverride = true;
            tooltipRect.gameObject.SetActive(true);
            tooltipText.text = text;

        }

        public void UpdateText(string text)
        {
            if (isActive)
            {
                tooltipText.text = text;
            }
        }

        public void HideTooltip()
        {
            isActive = false;
            HexCursorOverride = false;
            tooltipRect.gameObject.SetActive(false);
        }
    }
}

