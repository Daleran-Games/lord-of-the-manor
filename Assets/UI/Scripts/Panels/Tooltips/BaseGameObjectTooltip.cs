using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using UnityEngine.EventSystems;
using System;

namespace DaleranGames.UI
{
    public abstract class BaseGameObjectTooltip : MonoBehaviour, ITooltipableGameObject
    {

        public abstract string ObjectInfo { get; }

        public virtual void OnInfoUpdate(string newInfo)
        {
            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.ShowTooltip(ObjectInfo);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }



    }
}
