using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class WriteFile
{

    // Update is called once per frame
    public static void AddRow(string row)
    {
        DateTime now = DateTime.Now;
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"myData.csv", true))
        {
            file.WriteLine(row);
        }


    }
}

