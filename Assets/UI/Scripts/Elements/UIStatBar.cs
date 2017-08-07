using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DaleranGames.UI
{
    public class UIStatBar : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        Text label;
        [SerializeField]
        Slider bar;
        [SerializeField]
        Image background;
        [SerializeField]
        Image fillArea;
        [SerializeField]
        Image fillBar;
#pragma warning restore 0649
        string labelText;

        public static UIStatBar CreateStatBar(Transform parent, string barLabel, float initialValue, float initialMax, Color barColor)
        {
            GameObject newGO = (GameObject)Instantiate(UIManager.Instance.Style.StatBar, parent);
            UIStatBar newStatBar = newGO.GetRequiredComponent<UIStatBar>();
            newStatBar.ChangeBackgroundColor(UIManager.Instance.Style.PrimaryUIColor);
            newStatBar.ChangeBarColor(barColor);
            newStatBar.ChangeMaxValue(initialMax);
            newStatBar.ChangeValue(initialValue);
            newStatBar.SetLabel(barLabel);
            return newStatBar;
        }

        public void SetLabel(string newLabel)
        {
            labelText = newLabel;
            UpdateLabel();
        }

        public void UpdateLabel()
        {
            label.text = labelText + ": " + bar.value + "/" + bar.maxValue;
        }

        public void ChangeValue (float newValue)
        {
            bar.value = newValue;
            UpdateLabel();
        }

        public void ChangeMaxValue (float newValue)
        {
           bar.maxValue = newValue;
           UpdateLabel();
        }

        public void ChangeBackgroundColor (Color newColor)
        {
            background.color = newColor;
        }

        public void ChangeBarColor (Color newColor)
        {
            fillArea.color = Color.Lerp(newColor, Color.black, 0.5f);
            fillBar.color = newColor;
        }


    }
}
