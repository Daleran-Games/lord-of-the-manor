using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.UI
{
    public interface IDescribable
    {
        string GetRichTectBasicInfo();
        int TooltipPriority { get; }
        bool IsTooltipSuppressed { get; }
    }
}