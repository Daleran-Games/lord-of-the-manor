using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DaleranGames.UI
{
    [System.Serializable]
    public class TextLinkEntry : IComparer, IComparable
    {

        public int Priority;
        public IDescribable DescribedObject;
        public Text TooltipText;

        public TextLinkEntry(int pri, IDescribable desc, Text txt)
        {
            Priority = pri;
            DescribedObject = desc;
            TooltipText = txt;
        }

        public int Compare(object x, object y)
        {
            TextLinkEntry tle1 = x as TextLinkEntry;
            TextLinkEntry tle2 = y as TextLinkEntry;

            if (tle1 != null && tle2 != null)
            {
                if (tle1.Priority > tle2.Priority)
                    return 1;
                else if (tle1.Priority < tle2.Priority)
                    return -1;
                else
                    return 0;
            }
            else
                throw new ArgumentException("DG Error: Object is not a TextLinkEntry");


        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            TextLinkEntry tle = obj as TextLinkEntry;
            if (tle != null)
                return Priority.CompareTo(tle.Priority);
            else
                throw new ArgumentException("DG Error: Object is not a TextLinkEntry");



        }
    }
}
