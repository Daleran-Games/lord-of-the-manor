using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public class CSVEntry
    {
        public readonly int ID;

        protected Dictionary<string, string> entryData;

        public string this[string header]
        {
            get
            {
                //Debug.Log("Attempting to retrieve: " + header);
                return entryData[header];
            }
        }

        public CSVEntry(int id, string[]csvLine, string[] csvHeader)
        {
            ID = id;
            entryData = ParseCSVArrays(csvLine, csvHeader);
        }

        protected Dictionary<string,string> ParseCSVArrays(string[] csvLine, string[] csvHeader)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i=0; i<csvHeader.Length; i++)
            {
                result.Add(csvHeader[i], csvLine[i]);
                //Debug.Log("Adding "+csvHeader[i]+" "+csvLine[i]);
            }
            return result;
        }


        public List<string> ParseList(string header)
        {
            //Debug.Log("Parsing List " + header);
            return CSVUtility.ParseList(this[header]);
        }

    }
}
