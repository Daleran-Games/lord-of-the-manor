using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.UI
{
    public class UIManager : MonoBehaviour
    {
        protected UIManager() { }
        public static UIManager Instance = null;

        [SerializeField]
        UIStyle currentUIStlye;

        public UIStyle CurrentUIStyle
        {
            get { return currentUIStlye; }
        }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

    }
}