using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.TBSFramework
{
    public static class TurnUtilities
    {
        public static Seasons GetNext(this Seasons season)
        {
            switch (season)
            {
                case Seasons.Spring:
                    return Seasons.Summer;
                case Seasons.Summer:
                    return Seasons.Fall;
                case Seasons.Fall:
                    return Seasons.Winter;
                case Seasons.Winter:
                    return Seasons.Spring;
                default:
                    return Seasons.Spring;
            }
        }

        public static Seasons GetPrevious(this Seasons season)
        {
            switch (season)
            {
                case Seasons.Spring:
                    return Seasons.Winter;
                case Seasons.Summer:
                    return Seasons.Spring;
                case Seasons.Fall:
                    return Seasons.Summer;
                case Seasons.Winter:
                    return Seasons.Fall;
                default:
                    return Seasons.Spring;
            }
        }

    }
}
