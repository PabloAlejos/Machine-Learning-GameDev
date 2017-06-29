using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConfig : MonoBehaviour
{
    String[] arguments; // Donde se van a almacenar los parámetros
    public int nIterations = 5; // Numero de partidas que juega un individuo
    public int maxTime = 60; // Tiempo máximo de juego
    private int iteration = 0; // Iteración por la que se llega
    private string clasiffier = "pipe.sav"; //clasificador

    private void Awake()
    {
        Application.runInBackground = true; //Hace que no se pause el juego si estoy en otra ventana
        DontDestroyOnLoad(this.gameObject); // Evita que de destruya el objeto al cargar la escena
        arguments = Environment.GetCommandLineArgs(); //Guardo los valores que me pasan por linea de comandos
    }

    void Start()
    {
        switch (arguments.Length)
        {
            case 2:
                nIterations = Int32.Parse(arguments[1]);
                break;
            case 3:
                nIterations = Int32.Parse(arguments[1]);
                maxTime = Int32.Parse(arguments[2]);
                break;
            case 4:
                nIterations = Int32.Parse(arguments[1]);
                maxTime = Int32.Parse(arguments[2]);
                clasiffier = arguments[3];
                break;       
        }

}

public void Update()
{
    if (Time.timeSinceLevelLoad > maxTime)
    {
        OnGameOver();
        SceneManager.LoadScene(1);
        
    }
}


public void OnGameOver()
{
    iteration++;
    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"myData.csv", true))
    {
        file.WriteLine((FindObjectOfType<ScoreController>().GetScore() + 5 * Time.timeSinceLevelLoad).ToString());
    }
    checkEndGame();
}

public int getIteration()
{
    return iteration;
}

public void checkEndGame()
{
    if (iteration >= nIterations)
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}

    public String getClasiffier()
    {
        return clasiffier;
    }

}

