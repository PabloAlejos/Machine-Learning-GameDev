using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(StatesFileManager))]
public class GameStateController : MonoBehaviour
{
    StatesFileManager sfm;
    public GameState gs;
    private Player player;
    public List<Enemy> enemies;

    private int maxEnemies;
    void Start()
    {

        sfm = FindObjectOfType<StatesFileManager>();
        FindObjectOfType<EnemySpawner>().spawnEvent += OnEnemySpawn;
        maxEnemies = FindObjectOfType<EnemySpawner>().maxEnemiesOnScreen;
        player = FindObjectOfType<Player>();
        player.playerInputEvent += OnPlayerInput;
    }


    //Se crea un nuevo estado con cada tecla que pulsa el jugador
    void OnPlayerInput(KeyCode k)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        gs = new GameState(playerPos, makeEnemiesCsvFriendly(), k);
        sfm.AddState(gs.State2csv());
    }

    
    //
    private List<Enemy> makeEnemiesCsvFriendly()
    {
        List<Enemy> output = new List<Enemy>(enemies);

        while (output.Count <= maxEnemies)
        {
            output.Add(null);
        }
        return output;
    }


    //Función que hace que GameSateControlles se suscriba al enemigo que acaba de aparecer.
    private void OnEnemySpawn(Enemy e)
    {
        e.deathEvent += OnEnemyDie;
        enemies.Add(e);
    }

    //Cuando el enemigo muere se desuscribe.
    private void OnEnemyDie(float points, Enemy e)
    {
        enemies.Remove(e);
        e.deathEvent -= OnEnemyDie;
    }

}
