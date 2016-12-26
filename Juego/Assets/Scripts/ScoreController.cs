using UnityEngine;
using System.Collections;


public class ScoreController : MonoBehaviour
{

    public double score;

    // Use this for initialization
    void Start()
    {
        score = 0;
        FindObjectOfType<EnemySpawner>().spawnEvent += OnEnemySpawn;
    }

    //Evento que es llamado cuando un enemigo muere
    //Suma la puntuación multiplicada por la distancia a la que es destruido
    void OnEnemyDie(float points, Enemy e)
    {
        score += points * System.Math.Floor(e.transform.position.y);
        e.deathEvent -= OnEnemyDie;
    }

    //Evento que es llamado cuando aparece un enemigo
    //Es se suscribe a la lista de enventos para ver cuando es destruido
    void OnEnemySpawn(Enemy e)
    {
        e.deathEvent += OnEnemyDie;
    }

}
