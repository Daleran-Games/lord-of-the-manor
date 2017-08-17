using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using UnityEngine.EventSystems;

namespace DaleranGames.UI
{
    public abstract class BaseGameObjectTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public abstract string Text { get; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.ShowTooltip(Text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }


    }
}
