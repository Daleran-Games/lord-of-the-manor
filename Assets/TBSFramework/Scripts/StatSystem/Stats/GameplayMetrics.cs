﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public static class GameplayMetrics
    {
        public static float CoupleSize = 2f;
        public static float AverageLifespan = 50f;
        public static float ChildbearingYears = 20f;
        public const float Seasons = 4;
        public static float YearsBetweenChildren = 2f;


        //public static int DefaultBirthRate = Mathf.RoundToInt(1000f / (CoupleSize) * (ChildbearingYears / AverageLifespan) / Seasons / YearsBetweenChildren);
        public static int DefaultBirthRate = 40;
        public static Stat BaseBirthRateStat = new Stat(StatType.GroupBirthRate, DefaultBirthRate);

        //public static int DefaultDeathRate = Mathf.RoundToInt(1000f / AverageLifespan / Seasons) * 2;
        public static int DefaultDeathRate = 10;
        public static Stat BaseDeathRateStats = new Stat(StatType.GroupDeathRate, DefaultDeathRate);

        public static int DefaultStarvationRate = 250;
        public static Stat BaseStarvationRateStats = new Stat(StatType.GroupStarvationRate, DefaultStarvationRate);

        public static int DefaultFreezingRate = 125;
        public static Stat BaseFreezingRateStats = new Stat(StatType.GroupFreezeRate, DefaultFreezingRate);

        public static int DefaultForestGrowRate = 10;

    }
}
