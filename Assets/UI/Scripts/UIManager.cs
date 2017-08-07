using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;

namespace DaleranGames.UI
{
    public class UIManager : Singleton<UIManager>
    {
        protected UIManager() { }
#pragma warning disable 0649
        [SerializeField]
        UIStyle style;
        public UIStyle Style { get { return style; } }
#pragma warning restore 0649

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}