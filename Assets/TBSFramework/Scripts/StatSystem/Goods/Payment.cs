using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Payment 
    {
        [SerializeField]
        protected Transaction amount;
        public Transaction Amount { get { return amount; } }

        [SerializeField]
        protected bool paid = false;
        public bool Paid { get { return paid; } set { paid = value; } }

        [SerializeField]
        protected bool perCapita = false;
        public bool PerCapita { get { return perCapita; } set { perCapita = value; } }

        [SerializeField]
        protected bool rent = false;
        public bool Rent { get { return rent; } set { perCapita = rent; } }

        public Payment (Transaction transaction, bool isPerCapita, bool isRent)
        {
            amount = transaction;
            rent = isRent;
            perCapita = isPerCapita;
            paid = false;
        }

        public Transaction GetIfPerCapita (int population)
        {
            if (PerCapita)
                return new Transaction(amount.Type,amount.Value * population, amount.Description);
            else return Amount;
        }



    }
}
