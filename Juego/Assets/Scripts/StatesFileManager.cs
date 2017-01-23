using UnityEngine;
using System.Collections;

public class StatesFileManager : MonoBehaviour
{

    // Update is called once per frame
    public void AddState(string state)
    {
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"testEstado.csv", true))
        {
            file.WriteLine(state);
        }
    }

}
