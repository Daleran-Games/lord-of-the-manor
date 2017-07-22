using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace DaleranGames.IO
{
    public class CSVData
    {

        string[][] csvArray;

        public CSVData(string[][] csvArray)
        {
            this.csvArray = csvArray;
        }

        string this[int col, int row]
        {
            get
            {
                return "";
            }
        }

        string this[string header, int row]
        {
            get
            {
                return "";
            }
        }

        public static string[] ParseList(int row, string listName)
        {
            bool inList = false;
            List<string> items = new List<string>();

            for (int i = 0; i < csvLine.Length; i++)
            {
                if (inList)
                {
                    if (csvLine[i] == CSVUtility.EndList)
                        inList = false;
                    else
                        items.Add(csvLine[i]);
                }
                else if (csvLine[i] == listName && !inList)
                    inList = true;
            }

            if (inList)
                Debug.LogError("Never Exited list: " + listName);

            return items.ToArray();
        }
    }
}