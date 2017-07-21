using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace DaleranGames.IO
{
    public static class CSVUtility 
    {
        public const string End = "end";
        public const string List = "list";
        public const string EndList = "endList";

        const string columnSplit = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        const string rowSplit = @"\r\n|\n\r|\n|\r";
        static char[] trimChars = { '\"' };


        public static string[][] ParseCSVToArray(string csv)
        {
            string[] rows = Regex.Split(csv, rowSplit);
            if (rows.Length <= 1)
            {
                string[][] zero = new string[0][];
                return zero;
            }
            string[][] output = new string[rows.Length][];
            for (int i=0; i < rows.Length; i++)
            {
                string[] row = Regex.Split(rows[i], columnSplit);
                if (row.Length == 0 || row[0] == "")
                    continue;
                output[i] = row;

            }
            return output;
        }

        public static string[] ParseList (string[] csvLine, string listName)
        {
            bool inList = false;
            List<string> items = new List<string>();

            for (int i=0; i<csvLine.Length;i++)
            {
                if (inList)
                {
                    if (csvLine[i] == EndList)
                        inList = false;
                    else
                        items.Add(csvLine[i]);
                } else if (csvLine[i] == listName && !inList)
                    inList = true;
            }

            if (inList)
                Debug.LogError("Never Exited list: " + listName);

            return items.ToArray();
        }

        public static void PrintCSVArray (string[][] csv)
        {
            for (int i = 0; i < csv.Length; i++)
            {
                Debug.Log(csv[i]);
            }
        }

    }
}
