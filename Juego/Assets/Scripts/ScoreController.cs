using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour
{

   

    public double score;
    public int highScore;
    string highScoreKey = "HighScore";
    // Use this for initialization
    void Start()
    {
        FindObjectOfType<PlayerController>().playerDeath += OnGameOver;
        score = 0;
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        FindObjectOfType<EnemySpawner>().spawnEvent += OnEnemySpawn;
    }


    void OnGameOver()
    {
        if(score> PlayerPrefs.GetInt(highScoreKey))
        {
            PlayerPrefs.SetInt(highScoreKey, (int)score);
            PlayerPrefs.Save();
        }
    }

    //Evento que es llamado cuando un enemigo muere
    //Suma la puntuación multiplicada por la distancia a la que es destruido
    void OnEnemyDie(float points, Enemy e)
    {
        score += points * System.Math.Floor(e.transform.position.y+6);
        e.deathEvent -= OnEnemyDie;
    }

    //Evento que es llamado cuando aparece un enemigo
    //Es se suscribe a la lista de enventos para ver cuando es destruido
    void OnEnemySpawn(Enemy e)
    {
        e.deathEvent += OnEnemyDie;
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }



}
