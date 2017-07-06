using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Transaction 
    {
        public enum ScheduleMode
        {
            Immediate=0,
            TurnBeginning=1,
            TurnEnd=2
        }

        ScheduleMode schedule;
        public ScheduleMode Schedule { get { return schedule; } }

        Good good;
        public Good TransactedGood { get { return TransactedGood; } }

        string description;
        public string Description { get { return description; } }

        public Transaction (ScheduleMode scheduleMode,Good good)
        {
            this.good = good;
            this.schedule = scheduleMode;
            description = "None";
        }

        public Transaction(ScheduleMode scheduleMode, Good good, string description)
        {
            this.good = good;
            this.schedule = scheduleMode;
            this.description = description;
        }

    }
}