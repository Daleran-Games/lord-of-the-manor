using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace DaleranGames.UI
{
    public interface ITooltipableGameObject : IPointerEnterHandler, IPointerExitHandler, ITooltipable
    {
        void OnInfoUpdate(string newInfo);
    }
}