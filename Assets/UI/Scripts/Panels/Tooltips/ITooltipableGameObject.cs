using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace DaleranGames.UI
{
    public interface ITooltipableGameObject : IPointerEnterHandler, IPointerExitHandler
    {
        string ObjectInfo { get; }
        void OnInfoUpdate(string newInfo);
    }
}