using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DaleranGames.UI
{
    public class PanelView : MonoBehaviour {

        [SerializeField]
        protected Image panelImage;

        // Use this for initialization
        protected virtual void Awake()
        {
            //SetPanelColor(UIManager.Instance.CurrentUIStyle.PrimaryUIColor);
        }

        public virtual void SetPanelColor(Color newColor)
        {
            if (panelImage != null)
                panelImage.color = newColor;
        }

        public virtual void EnableImage(bool enable)
        {
            if (panelImage != null)
                panelImage.enabled = enable;
        }

    }
}
