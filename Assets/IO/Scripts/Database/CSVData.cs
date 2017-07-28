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
        public CSVEntry this[int id] { get { return entries[id]; } }
        public int Entries { get { return entries.Count; } }
        public string[][] RawData { get { return csvArray; } }

        string[][] csvArray;
        List<CSVEntry> entries;

        public CSVData(string name, string[][] csvArray)
        {
            Name = name;
            this.csvArray = csvArray;
            entries = ParseOneHeader(csvArray);

        }

        public CSVData(string name, string[][] csvArray, List<string> multipleHeaders)
        {
            Name = name;
            this.csvArray = csvArray;
            entries = ParseMultipleHeaders(csvArray, multipleHeaders);
        }

        List<CSVEntry> ParseOneHeader (string[][] csvArray)
        {
            string[] header = csvArray[0];
            List<CSVEntry> newEntries = new List<CSVEntry>();


            for (int i=1;i<csvArray.Length;i++)
            {
                newEntries.Add(new CSVEntry(i-1,PadJAggedStringArray(header,csvArray[i]),header));
            }
            return newEntries;
        }

        List<CSVEntry> ParseMultipleHeaders(string[][] csvArray, List<string> multipleHeaders)
        {
            string[] header = csvArray[0];
            bool headerNext = false;
            int currentID = 0;
            List<CSVEntry> newEntries = new List<CSVEntry>();
            for (int i = 0; i < csvArray.Length; i++)
            {
                if (multipleHeaders.Contains(csvArray[i][0]))
                {
                    headerNext = true;
                } else if (headerNext == true)
                {
                    header = csvArray[i];
                    headerNext = false;
                } else
                {
                    newEntries.Add(new CSVEntry(currentID, PadJAggedStringArray(header, csvArray[i]), header));
                    currentID++;
                }
            }
            return newEntries;
        }

        string[] PadJAggedStringArray(string[] header, string[] entry)
        {
            if (header.Length == entry.Length)
                return entry;

            string[] newEntry = new string[header.Length];
            entry.CopyTo(newEntry, 0);
            return newEntry;
        }



    }
}