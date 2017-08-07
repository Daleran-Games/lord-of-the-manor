using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class CostCollection
    {

        [SerializeField]
        protected List<Cost> costs;

        public virtual Cost this[StatType type]
        {
            get
            {
                for (int i = 0; i < costs.Count; i++)
                {
                    if (costs[i].ModifiedBy == type)
                        return costs[i];

                }
                return Cost.Null;
            }
        }

        public CostCollection(params Cost[] costs)
        {
            this.costs = new List<Cost>();

            for (int i = 0; i < costs.Length; i++)
            {
                if (costs[i].Value != 0)
                    this.costs.Add(costs[i]);
            }

        }

        public List<Cost> GetAllCosts()
        {
            return new List<Cost>(costs);
        }

        public List<Transaction> GetAllCostsAsTransaction(IStatCollection<StatType> stats)
        {
            List<Transaction> query = new List<Transaction>();
            for (int i = 0; i < costs.Count; i++)
            {
                query.Add(costs[i].ModifiedTransaction(stats));
            }
            return query;
        }

        public List<Transaction> GetAllReverseCostsAsTransaction(IStatCollection<StatType> stats)
        {
            List<Transaction> query = new List<Transaction>();
            for (int i = 0; i < costs.Count; i++)
            {
                query.Add(costs[i].ReverseModifiedTransaction(stats));
            }
            return query;
        }

    }
}