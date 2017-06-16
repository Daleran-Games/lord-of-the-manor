using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DaleranGames.UI
{
    public class UITextBox : MonoBehaviour
    {
        [SerializeField]
        Text text;
        [SerializeField]
        Image box;

        public static UITextBox CreateTextBox(Transform parent)
        {
            GameObject newGO = (GameObject)Instantiate(UIManager.Instance.CurrentUIStyle.TextBox, parent);
            UITextBox newTextBox = newGO.GetRequiredComponent<UITextBox>();
            newTextBox.box.color = UIManager.Instance.CurrentUIStyle.PrimaryUIColor;
            return newTextBox;

        }

        public static UITextBox CreateTextBox(Transform parent, string initialText, bool withBorder)
        {
            UITextBox newTextBox = UITextBox.CreateTextBox(parent);
            newTextBox.text.text = initialText;
            newTextBox.EnableBorder(withBorder);
            return newTextBox;
        }

        public void SetText(string newText)
        {
            text.text = newText;
        }

        public void EnableBorder (bool enable)
        {
            box.enabled = enable;
        }

        public void ChangeBorderColor (Color col)
        {
            box.color = col;
        }
    }
}
