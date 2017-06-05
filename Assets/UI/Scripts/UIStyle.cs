using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.UI
{
    [CreateAssetMenu(fileName = "NewUIStyle", menuName = "DaleranGames/UI/UI Style", order = 0)]
    public class UIStyle : ScriptableObject
    {
        /* TODO:
         * Create the UI objects and relavent scripts
         */

        [Header("Default UI Objects")]
        public GameObject TextBox;
        public GameObject StatBar;
        public GameObject ButtonIcon;
        public GameObject ButtonText;

        [Header("Gameplay Color Scheme")]
        public Color32 NeutralColor;
        public Color32 BridgeColor;
        public Color32 TacticalColor;
        public Color32 EngineeringColor;
        public Color32 ScienceColor;

        [Header("UI Color Scheme")]
        public Color32 PrimaryUIColor;
        public Color32 SecondaryUIColor;
        public Color32 StatIncreaseColor;
        public Color32 StatDecreaseColor;

    }
}
