using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class SummerTurn : BaseTurn
    {
        public override Seasons Season { get { return Seasons.Summer; } }

        private void OnEnable()
        {
            if (StateEnabled != null)
                StateEnabled(this);
        }

        public override void NextTurn()
        {
            if (StateDisabled != null)
                StateDisabled(this);

            TurnManager.Instance.ChangeTurn(TurnManager.Instance.Fall);
        }

    }
}
