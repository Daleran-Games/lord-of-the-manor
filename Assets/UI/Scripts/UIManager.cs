using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;

namespace DaleranGames.UI
{
    public class UIManager : Singleton<UIManager>
    {
        protected UIManager() { }

        [SerializeField]
        UIStyle style;
        public UIStyle Style { get { return style; } }

        void Awake()
        {

        }

    }
}