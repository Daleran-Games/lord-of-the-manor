using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;


namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public class ModifierList : EventList<ModifierSet>
    {

        ModifiableStat[] stats;

        public ModifierList (IModifiableStatCollection statCollection) : base()
        {
            stats = statCollection.GetModifiableStats();
        }

        public override ModifierSet this[int index]
        {
            get { return eventList[index]; }
            set
            {

                eventList[index] = value;
                FireAction();
            }
        }

        public override void Add(ModifierSet item)
        {
            eventList.Add(item);
            FireAction();
        }

        public override void Insert(int index, ModifierSet item)
        {
            eventList.Insert(index, item);
            FireAction();
        }



    }
}

