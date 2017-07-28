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

        public virtual Cost this[CostType type]
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
            this.costs = new List<Cost>(costs);
        }

        public List<Cost> GetAllCostsOfActivity(CostType.CategoryType category)
        {
            List<Cost> query = new List<Cost>();

            for (int i=0;i<costs.Count;i++)
            {
                if (costs[i].ModifiedBy.Category == category)
                    query.Add(costs[i]);
            }

            if (query.Count == 0)
                Debug.LogWarning("No costs associated with "+category+" found");

            return query;
        }

        public List<Cost> GetAllCosts()
        {
            return new List<Cost>(costs);
        }

    }
}
