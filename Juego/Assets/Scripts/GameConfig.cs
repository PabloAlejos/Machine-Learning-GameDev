using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConfig : MonoBehaviour
{

    String[] arguments; //donde se van a almacenar los parámetros
    public int iterations = 5; //Numero de partidas que juega un individuo
    public int maxTime = 60;
    public static int n;
    ScoreController sc;

    private void Awake()
    {
        Application.runInBackground = true;
        DontDestroyOnLoad(this.gameObject);
        arguments = Environment.GetCommandLineArgs();
       
       
    }

    void Start()
    {
        if (arguments.Length > 2)
        {
            n = Int32.Parse(arguments[1]);
            maxTime = Int32.Parse(arguments[2]);
        }
        

    }

    public void Update()
    {
        if (Time.timeSinceLevelLoad > 60)
        {
            n++;
            SceneManager.LoadScene(1);
        }
    }


    public void GameOver()
    {
        n++;
        Debug.Log(n);
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"myData.csv", true))
        {
            file.WriteLine((FindObjectOfType<ScoreController>().GetScore() + 5 * Time.timeSinceLevelLoad).ToString());
        }

       

        if (n >= iterations)
        {
            Debug.Log("Quit!");
            Application.Quit();
        }

    }

    public int getIteration()
    {
        return n;
    }

}

