using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameConfig : MonoBehaviour
{


    public int iterations = 5; //Numero de partidas que juega un individuo
    public static int n;
    ScoreController sc;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        n = 0;

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
            file.WriteLine(FindObjectOfType<ScoreController>().GetScore().ToString());
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

