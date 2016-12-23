using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemies;
    public float SpwanRate = 1;
    private float nextSpawnTime;
    private float ScreenHalfSizeInWorldUnits;


    // Use this for initialization
    void Start()
    {
        ScreenHalfSizeInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        nextSpawnTime = Time.time + SpwanRate / 10;
    }

    // Update is called once per frame
    void Update()
    {

        float xPos = Random.Range(-ScreenHalfSizeInWorldUnits, ScreenHalfSizeInWorldUnits);
        Vector3 spawnPosition = new Vector3(xPos, transform.position.y,transform.position.z);
        if (Time.time > nextSpawnTime && enemiesOnScreen() <= 4)
        {
            Instantiate(randomEnemy(), spawnPosition, Quaternion.identity);
            nextSpawnTime = Time.time + SpwanRate / 10;
        }



    }

    //Elige un enemigo al aza de la lista
    private GameObject randomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }

    private int enemiesOnScreen()
    {
        return FindObjectsOfType<Enemy>().Length;
    }
}
