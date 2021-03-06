﻿using System.Collections;
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
        private bool isActive = false;
        public bool IsActive { get { return isActive; } }

        public abstract string Info { get; }

        public virtual void OnInfoUpdate(string newInfo)
        {
            
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            TooltipController.Instance.ShowTooltip(Info);
            isActive = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            CloseTooltip();
        }

        public virtual void CloseTooltip()
        {
            TooltipController.Instance.HideTooltip();
            isActive = false;
        }

    }
}
