using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.TBSFramework;


namespace DaleranGames.UI
{
    public class OverlayLabel : MonoBehaviour
    {

        [SerializeField]
        protected Text label;

        [SerializeField]
        protected Image icon;

        public RectTransform Rect;


        private void Awake()
        {
            label = GetComponentInChildren<Text>();
            icon = GetComponentInChildren<Image>();
            Rect = GetComponent<RectTransform>();

        }

        public void MoveToPosition (Vector2 position)
        {
            Rect.anchoredPosition = position;
        }

        public void SetIconAndText (Sprite sprite, string text)
        {
            icon.sprite = sprite;
            label.text = text;
        }
    
    }
}
