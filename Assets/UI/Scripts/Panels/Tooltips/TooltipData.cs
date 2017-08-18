using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using UnityEngine.EventSystems;

namespace DaleranGames.UI
{
    [System.Serializable]
    public class TooltipData
    {
        public string Title;
        public string[] BasicInfo;
        public bool hasExpanded;
        public string[] ExpandedInfo;

        public TooltipData(string title, string[] basic)
        {

        }

        public TooltipData(string title, string[] basic, string expanded)
        {

        }

    }
}