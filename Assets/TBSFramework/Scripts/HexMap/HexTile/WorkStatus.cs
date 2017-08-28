using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class WorkStatus
    {
        [SerializeField]
        protected bool paused = false;
        public bool Paused { get { return paused; } set { paused = value; } }

        [SerializeField]
        protected bool pausedOverride = false;
        public bool PausedOverride { get { return pausedOverride; } set { pausedOverride = value; } }

        [SerializeField]
        protected bool workSpring = true;
        public bool WorkSpring { get { return workSpring; } set { workSpring = value; } }

        [SerializeField]
        protected bool workSummer = true;
        public bool WorkSummer { get { return workSummer; } set { workSummer = value; } }

        [SerializeField]
        protected bool workFall = true;
        public bool WorkFall { get { return workFall; } set { workFall = value; } }

        [SerializeField]
        protected bool workWinter = true;
        public bool WorkWinter { get { return workWinter; } set { workWinter = value; } }

        public WorkStatus()
        {

        }

        public bool IsSeasonWorkable(Seasons season)
        {
            switch(season)
            {
                case Seasons.Spring:
                    return WorkSpring;
                case Seasons.Summer:
                    return WorkSummer;
                case Seasons.Fall:
                    return WorkFall;
                case Seasons.Winter:
                    return WorkWinter;
                default:
                    return false;
            }
        }

        public void SetSeasonWorkable(Seasons season, bool work)
        {
            switch (season)
            {
                case Seasons.Spring:
                    WorkSpring = work;
                    break;
                case Seasons.Summer:
                    WorkSummer = work;
                    break;
                case Seasons.Fall:
                    WorkFall = work;
                    break;
                case Seasons.Winter:
                    WorkWinter = work;
                    break;
            }
        }

        public void ResetSeasons()
        {
            WorkSpring = true;
            WorkSummer = true;
            WorkFall = true;
            WorkWinter = true;
        }

    }
}
