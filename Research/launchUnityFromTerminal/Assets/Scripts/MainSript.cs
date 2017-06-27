using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSript : MonoBehaviour
{

    String[] arguments;
    int n = 0;

    private void Start()
    {
       arguments = Environment.GetCommandLineArgs();
    }

    void Update()
    {
        if (n < 10)
        {
            int v = UnityEngine.Random.Range(0, 500);
            WriteFile.AddRow(v.ToString());
            n++;
        }else
        {
            Application.Quit();
        }
        
    }
}
