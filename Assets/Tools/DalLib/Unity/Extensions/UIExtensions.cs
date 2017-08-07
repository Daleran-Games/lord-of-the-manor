using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.UI
{
    public static class UIExtensions
    {
        
        public static void AutoColor (this ColorBlock colorBlock, Color32 newColor)
        {
  
            colorBlock.normalColor = newColor;
            colorBlock.highlightedColor = Color.Lerp(newColor, Color.white, 0.5f);
            colorBlock.pressedColor = Color.Lerp(newColor, Color.black, 0.5f);
            colorBlock.disabledColor = Color.Lerp(newColor, Color.clear, 0.5f);
            colorBlock.colorMultiplier = 1f;
            colorBlock.fadeDuration = 0.1f;

        }
       
    }

}
