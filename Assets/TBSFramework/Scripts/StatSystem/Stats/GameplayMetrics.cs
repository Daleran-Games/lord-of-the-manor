using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public static class GameplayMetrics
    {
        //Population

        public static float CoupleSize = 2f;
        public static float AverageLifespan = 50f;
        public static float ChildbearingYears = 20f;
        public const float Seasons = 4;
        public static float YearsBetweenChildren = 2f;

        //public static int DefaultBirthRate = Mathf.RoundToInt(1000f / (CoupleSize) * (ChildbearingYears / AverageLifespan) / Seasons / YearsBetweenChildren);
        public static int DefaultBirthRate = 30;
        public static Stat BaseBirthRateStat = new Stat(StatType.GroupBirthRate, DefaultBirthRate);

        //public static int DefaultDeathRate = Mathf.RoundToInt(1000f / AverageLifespan / Seasons) * 2;
        public static int DefaultDeathRate = 10;
        public static Stat BaseDeathRateStats = new Stat(StatType.GroupDeathRate, DefaultDeathRate);

        public static int DefaultStarvationRate = 500;
        public static Stat BaseStarvationRateStats = new Stat(StatType.GroupStarvationRate, DefaultStarvationRate);

        public static int DefaultFreezingRate = 250;
        public static Stat BaseFreezingRateStats = new Stat(StatType.GroupFreezeRate, DefaultFreezingRate);

        // Ecology

        public static int DefaultForestGrowRate = 10;

        //Economy
        public const int FoodValue = 4;
        public const int FoodLaborMultiplier = 1;
        public const int WoodValue = 6;
        public const int WoodLaborMultiplier = 2;
        public const int StoneValue = 10;
        public const int StoneLaborMultiplier = 3;
        public const int LaborValue = 2;
        public const int GoldFactor = 10;
        public const int LandValueFactor = 3;
        public const int MinimumLandValue = 10;
        public const int DepreciationDivisor = 2;

    }
}
