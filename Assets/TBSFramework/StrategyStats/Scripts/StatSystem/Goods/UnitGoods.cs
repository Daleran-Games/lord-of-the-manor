using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UnitGoods
    {

        protected int FoodStored;
        protected int WoodStored;
        protected int StoneStored;
        protected int Population;
        protected int Work;

        protected List<Transaction> transactions;

    }
}
