using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{

    public class DwellingButtonDisabler : MonoBehaviour
    {
        private void Start()
        {
            GroupManager.Instance.PlayerGroup.HomeChanged += HomeChanged;
        }

        void HomeChanged(bool state)
        {
            if (state)
            {
                gameObject.SetActive(false);
            }
            else
                gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            GroupManager.Instance.PlayerGroup.HomeChanged -= HomeChanged;
        }
    }
}
