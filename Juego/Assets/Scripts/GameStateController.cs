using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(StatesFileManager))]
public class GameStateController : MonoBehaviour
{
    private StatesFileManager sfm;
    private PlayerController player;
   
    private float nextStateRead;

    public float stateReadRate = .1f;
   

    void Start()
    {
        sfm = FindObjectOfType<StatesFileManager>(); //Encargado de escribir el estado en el csv
        player = FindObjectOfType<PlayerController>();
        nextStateRead = stateReadRate + Time.time;
    }


    //Método que es llamado al final de cada fotograma, es decir, cuando todas las fisicas han sido calculadas.
    void FixedUpdate()
    {
        GameState gs;
        GameObject[] enemies;
        if (Time.time > nextStateRead)
        {
            enemies = MakeEnemyList(); 
            
            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            gs = new GameState(playerPos, enemies , ReadKey() );
            sfm.AddState(gs.State2csv());
            nextStateRead = stateReadRate + Time.time;
        }
    }
    
    private GameObject[] MakeEnemyList()
    {
        return GameObject.FindGameObjectsWithTag("Enemy");
    }


    KeyCode ReadKey()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return KeyCode.LeftArrow;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return KeyCode.RightArrow;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            return KeyCode.Space;
        }
        return KeyCode.None;
    }

}
