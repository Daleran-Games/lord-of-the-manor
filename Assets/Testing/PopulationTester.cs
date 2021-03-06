﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{

    //Dumm script for making quick tests
    public class PopulationTester : MonoBehaviour
    {
        [SerializeField]
        int pop = 4;

        [SerializeField]
        int run = 0;

        [SerializeField]
        int maxRuns = 500;



        // Use this for initialization
        void Start()
        {
            Debug.Log("Birth Rate: " + GameplayMetrics.BaseBirthRateStat);
            Debug.Log("Death Rate: " + GameplayMetrics.BaseDeathRateStats);
        }

        // Update is called once per frame
        void Update()
        {
            if (run < maxRuns)
            {
                if (Random.Bool(GameplayMetrics.BaseBirthRateStat.Value))
                    pop++;

                if (Random.Bool(GameplayMetrics.BaseDeathRateStats.Value))
                    pop--;

                run++;
            }

        }
    }
}

