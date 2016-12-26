using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(StatesFileManager))]
public class GameStateController : MonoBehaviour
{
    StatesFileManager sfm;
    public GameState gs;
    private Player player;
    public List<Enemy> enemies;

    void Awake()
    {
        sfm = FindObjectOfType<StatesFileManager>();
        FindObjectOfType<EnemySpawner>().spawnEvent += OnEnemySpawn;
        player = FindObjectOfType<Player>();
    }


    //Se ejecutará al final de cada Frame
    void FixedUpdate()
    {


        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        KeyCode k = player.lasHitKey;

        gs = new GameState(playerPos, enemies, k);
        sfm.AddState(gs.State2csv());


    }

    private void OnEnemySpawn(Enemy e)
    {
        e.deathEvent += OnEnemyDie;
        enemies.Add(e);
    }

    private void OnEnemyDie(float points, Enemy e)
    {
        Debug.Log("muere patata");
        enemies.Remove(e);
        e.deathEvent -= OnEnemyDie;
    }

}
