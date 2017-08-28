using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public interface IBuildable : ICommandable
    {
        BuildFeature Build { get; }
    }
}
