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
        const string listSplit = @";";

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


        public static List<string> ParseList (string csvListElement)
        {
            List<string> items = new List<string>(Regex.Split(csvListElement,listSplit));
            //Debug.Log("List Lenght: "+items.Count);
            if (items.Count == 1 && items[0] == "")
                return new List<string>(0);
            else
                return items;
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
