using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    //Eventos
    public delegate void SpawnDelegate(Enemy e);
    public event SpawnDelegate spawnEvent;

    //Enemigos
    public int MAX_ENEMIES = 6;
    public GameObject boss;
    public GameObject[] enemies;
    public float SpwanRate = 1;
    public int maxEnemiesOnScreen;
    public float nextWave;
    public float BossSpawnRateiInSecs = 60;

    //PowerUps
    public GameObject[] PowerUps;
    private float PowerUpChance = 0.1f;

    private float nextSpawnTime;
    private float nextBossSpawnTime;
    private bool bossSpawn = false;

    // Use this for initialization
    void Start()
    {
        nextBossSpawnTime = Time.time + BossSpawnRateiInSecs;
        nextSpawnTime = Time.time + SpwanRate / 10;
        nextWave = 5;
    }

    // Update is called once per frame
    void Update()
    {


        float xPos = Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f);
        Vector3 spawnPosition = new Vector3(xPos, transform.position.y, transform.position.z);

        if (Time.time > nextSpawnTime && CountenemiesOnScreen() <= maxEnemiesOnScreen)
        {

            if (!bossSpawn)
            {
                GameObject enemy = (GameObject)Instantiate(randomEnemy(), spawnPosition, Quaternion.identity);
                spawnEvent(enemy.GetComponent<Enemy>());
                nextSpawnTime = Time.time + SpwanRate / 20;
            }
            else
            {
                GameObject myBoss = (GameObject)Instantiate(boss, spawnPosition, Quaternion.identity);
                spawnEvent(myBoss.GetComponent<Enemy>());
                myBoss.GetComponent<Enemy>().maxHealth = Mathf.RoundToInt(Time.timeSinceLevelLoad/4);
                myBoss.GetComponent<Enemy>().health = myBoss.GetComponent<Enemy>().maxHealth;
                myBoss.GetComponent<Enemy>().scorePoints = Mathf.RoundToInt(Time.timeSinceLevelLoad * 2);
                nextBossSpawnTime = Time.time + BossSpawnRateiInSecs;
                bossSpawn = false;
            }
            //Hay cierta probabilidad de que aparezca un power up a la vez que un enemigo
            SpawnPowerUp();
        }

        //Voy cambiando el máximo de enemigos en patalla para dar un respiro al jugador
        if (Time.time > nextWave)
        {
            maxEnemiesOnScreen = Random.Range(2, MAX_ENEMIES);
            nextWave = Time.time + 2;
        }

        if (Time.time > nextBossSpawnTime)
        {
            bossSpawn = true;
        }


    }

    //Elige un enemigo al azar de la lista
    private GameObject randomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }

    private int CountenemiesOnScreen()
    {
        return FindObjectsOfType<Enemy>().Length;
    }


    //Probabilidad dinámica de aparición de power ups ( A mas tiempo mas facil es que salga)
    void SpawnPowerUp()
    {
        if (Random.Range(0, 100) < PowerUpChance)
        {
            float xPos = Random.Range(transform.position.x - 2.5f, transform.position.x + 2.5f);
            Vector3 spawnPosition = new Vector3(xPos, transform.position.y, transform.position.z);
            GameObject powerUp = (GameObject)Instantiate(PowerUps[Random.Range(0, PowerUps.Length)], spawnPosition, Quaternion.identity);
            PowerUpChance = 0.1f;
        }
        else
        {
            PowerUpChance += 0.5f;
        }

    }
}
