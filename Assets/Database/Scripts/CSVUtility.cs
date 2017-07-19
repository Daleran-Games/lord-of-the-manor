using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

namespace DaleranGames.Database
{
    public static class CSVUtility 
    {
        public const string End = "end";

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

        public static void PrintCSVArray (string[][] csv)
        {
            for (int i = 0; i < csv.Length; i++)
            {
                Debug.Log(csv[i]);
            }
        }

    }
}
