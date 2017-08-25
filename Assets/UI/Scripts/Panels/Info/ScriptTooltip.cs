using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;
using UnityEngine.EventSystems;
using System;

namespace DaleranGames.UI
{
    public class ScriptTooltip : BaseGameObjectTooltip
    {
        string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;

                if (IsActive)
                    TooltipController.Instance.ShowTooltip(Info);
            }
        }

        public override string Info { get { return text; } }
    }
}
