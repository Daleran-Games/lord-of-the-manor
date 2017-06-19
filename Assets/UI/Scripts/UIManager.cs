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
        UIStyle currentUIStlye;

        public UIStyle CurrentUIStyle
        {
            get { return currentUIStlye; }
        }

        void Awake()
        {

        }

    }
}