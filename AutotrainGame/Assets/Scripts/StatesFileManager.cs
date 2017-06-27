using UnityEngine;
using System.Collections;
using System.IO;
using System.Web;
using System;

public class StatesFileManager : MonoBehaviour
{

    
    // Update is called once per frame
    public void AddState(string state)
    {
        DateTime now = DateTime.Now;

#if UNITY_STANDALONE_WIN
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"GameStates_"+now.DayOfYear+".csv", true))
        {
            file.WriteLine(state);
        }
#endif

#if UNITY_WEBGL
        string fullSavePath = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/testData.csv", true));
        StreamWriter dataFile = new StreamWriter(fullSavePath);
        dataFile.Write(state);
#endif

    }
}
