using UnityEngine;
using System.Collections;
using System.IO;
using System.Web;

public class StatesFileManager : MonoBehaviour
{

    
    // Update is called once per frame
    public void AddState(string state)
    {
#if UNITY_STANDALONE_WIN
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"gameStates.csv", true))
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
