using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DaleranGames.TBSFramework;
using DaleranGames.IO;

namespace DaleranGames.UI
{
    public class ManualTooltip : BaseGameObjectTooltip
    {

        [SerializeField]
        [TextArea(3, 8)]
        string text;
        public override string Info { get { return text; } }

    }
}
