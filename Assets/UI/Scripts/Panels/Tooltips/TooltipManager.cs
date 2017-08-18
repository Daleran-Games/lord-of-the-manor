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

        }

        void OnHexTileExit(HexTile tile)
        {

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

