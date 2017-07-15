using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DaleranGames.UI
{
    public class UIButton : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        Text label;
        [SerializeField]
        Image backgroundImage;
#pragma warning restore 0649

        [SerializeField]
        public Button ButtonControl;


        public static UIButton CreateTextButton(Transform parent, string label)
        {
            GameObject newGO = (GameObject)Instantiate(UIManager.Instance.CurrentUIStyle.ButtonText, parent);
            UIButton newButton = newGO.GetRequiredComponent<UIButton>();
            newButton.ChangeColor(UIManager.Instance.CurrentUIStyle.PrimaryUIColor);
            newButton.SetLabel(label);
            return newButton;
        }

        public static UIButton CreateIconButton(Transform parent, Sprite icon, Color buttonColor)
        {
            GameObject newGO = (GameObject)Instantiate(UIManager.Instance.CurrentUIStyle.ButtonIcon, parent);
            UIButton newButton = newGO.GetRequiredComponent<UIButton>();
            newButton.ChangeColor(UIManager.Instance.CurrentUIStyle.PrimaryUIColor);
            newButton.EnableLabel(false);
            newButton.ChangeSprite(icon);
            newButton.ChangeColor(buttonColor);
            return newButton;
        }

        public static UIButton CreateSmallButton(Transform parent, string label)
        {
            GameObject newGO = (GameObject)Instantiate(UIManager.Instance.CurrentUIStyle.ButtonIcon, parent);
            UIButton newButton = newGO.GetRequiredComponent<UIButton>();
            newButton.ChangeColor(UIManager.Instance.CurrentUIStyle.PrimaryUIColor);
            newButton.SetLabel(label);
            return newButton;
        }

        public void ChangeColor (Color newColor)
        {
            ColorBlock newColorBlock = new ColorBlock();
            newColorBlock.normalColor = newColor;
            newColorBlock.highlightedColor = Color.Lerp(newColor, Color.white, 0.5f);
            newColorBlock.pressedColor = Color.Lerp(newColor, Color.black, 0.5f);
            newColorBlock.disabledColor = Color.Lerp(newColor, Color.clear, 0.5f);
            newColorBlock.colorMultiplier = 1f;
            newColorBlock.fadeDuration = 0.1f;

            ButtonControl.colors = newColorBlock;
        }

        public void SetLabel (string text)
        {
            label.text = text;
        }

        public void ChangeSprite(Sprite newSprite)
        {
            backgroundImage.sprite = newSprite;
        }

        public void EnableLabel (bool enable)
        {
            label.enabled = enable;
        }


    }
}