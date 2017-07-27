using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class CostCollection
    {
        protected List<Cost> costs;


        public CostCollection(params Cost[] costs)
        {
            this.costs = new List<Cost>(costs);
        }

        public List<Cost> GetAllCostsOfActivity(string activityType)
        {
            List<Cost> query = new List<Cost>();

            for (int i=0;i<costs.Count;i++)
            {
                if (costs[i].ModifiedBy.Activity == activityType)
                    query.Add(costs[i]);
            }

            if (query.Count == 0)
                Debug.LogWarning("No costs associated with "+activityType+"found");

            return query;
        }

        public List<Cost> GetAllCosts()
        {
            return new List<Cost>(costs);
        }

    }
}
