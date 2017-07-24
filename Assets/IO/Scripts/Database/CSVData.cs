using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace DaleranGames.IO
{
    public class CSVData
    {
        public readonly string Name;
        string[][] csvArray;

        public CSVData(string name, string[][] csvArray)
        {
            Name = name;
            this.csvArray = csvArray;
        }

        public string this[int col, int row] { get { return csvArray[row][col]; } }
        public string this[string header, int id] { get { return csvArray[FindRowWithId(id.ToString())][FindColumnWithHeader(header)]; } }
        public string[] this[int row] { get { return csvArray[row]; } }
        public int Rows { get { return csvArray.Length; } }


        public string[] ParseList(string listName, int id)
        {
            int row = FindRowWithId(id.ToString());
            return CSVUtility.ParseList(csvArray[row], listName);
        }

        public int FindRowWithId(string idString)
        {
            for (int i=0; i < csvArray.Length; i++)
            {
                if (csvArray[i][0] == idString)
                    return i;
            }
            Debug.LogError("id " + idString + " not found in CVSData "+ Name + ". Returning 0.");
            return 0;
        }

        public int FindColumnWithHeader (string header)
        {
            for (int i = 0; i < csvArray[0].Length; i++)
            {
                if (csvArray[0][i] == header)
                    return i;
            }
            Debug.LogError("header " + header + " not found in CVSData " + Name + ". Returning 0.");
            return 0;
        }
    }
}