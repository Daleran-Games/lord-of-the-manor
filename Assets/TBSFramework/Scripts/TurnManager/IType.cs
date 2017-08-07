using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public interface IType<T>
    {
        void OnActivation(T obj);
        void OnGameStart(T obj);
        void OnTurnEnd(BaseTurn turn, T obje);
        void OnTurnSetUp(BaseTurn turn, T obj);
        void OnTurnStart(BaseTurn turn, T obj);
        void OnDeactivation(T obj);
    }

}
