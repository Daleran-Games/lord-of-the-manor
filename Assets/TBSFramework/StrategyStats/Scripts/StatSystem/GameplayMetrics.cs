using System.Collections;
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
        public static float YearsBetweenChildren = 1f;

        public static Stat BaseBirthRate = new Stat("UnitBirthRate", Mathf.RoundToInt(1000f / (CoupleSize) * (ChildbearingYears / AverageLifespan) / Seasons / YearsBetweenChildren));
        public static Stat BaseDeathRate = new Stat("UnitDeathRate", Mathf.RoundToInt(1000f / AverageLifespan / Seasons));

        public static Stat BaseStarvationRate = new Stat("UnitStarvationRate", 500);
        public static Stat BaseFreezingRate = new Stat("UnitFreezingRate", 250);

    }
}
