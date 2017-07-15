using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames
{
    [System.Serializable]
    public class EventList<T> : IList<T>
    {
        [SerializeField]
        protected List<T> eventList;

        public event Action ListModified;

        public EventList()
        {
            eventList = new List<T>();
        }

        public virtual T this[int index]
        {
            get { return eventList[index]; }
            set
            {
                eventList[index] = value;
                FireAction();
            }
        }

        public virtual int Count { get { return eventList.Count; } }

        public virtual bool IsReadOnly { get { return false; } }

        public virtual void Add(T item)
        {
            eventList.Add(item);
            FireAction();
        }

        public virtual void Clear()
        {
            eventList.Clear();
            FireAction();
        }

        public virtual bool Contains(T item)
        {
            return eventList.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            eventList.CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return eventList.GetEnumerator();
        }

        public virtual int IndexOf(T item)
        {
            return eventList.IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            eventList.Insert(index, item);
            FireAction();
        }

        public virtual bool Remove(T item)
        {
            FireAction();
            return eventList.Remove(item);

        }

        public virtual void RemoveAt(int index)
        {
            eventList.RemoveAt(index);
            FireAction();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return eventList.GetEnumerator();
        }

        protected virtual void FireAction ()
        {
            if (ListModified != null)
                ListModified(); 
        }
    }

}


