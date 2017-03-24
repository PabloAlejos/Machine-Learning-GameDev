using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(StatesFileManager))]
public class GameStateController : MonoBehaviour
{
    private StatesFileManager sfm;
    private PlayerController player;
    private SocketController sc;

    private float nextStateRead;

    public float stateReadRate;
    public bool recordStates;
    //Variables de ReadKey()
    float stuckTime;
    KeyCode k;

    void Start()
    {
        sfm = FindObjectOfType<StatesFileManager>(); //Encargado de escribir el estado en el csv
        player = FindObjectOfType<PlayerController>();
        nextStateRead = stateReadRate + Time.time;
        sc = FindObjectOfType<SocketController>();
        
        //Inicialización de ReadKey()
        stuckTime = 0;
        k = KeyCode.None;

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
            gs = new GameState(playerPos, enemies, ReadKey());

            if (recordStates)
                sfm.AddState(gs.State2csv());


            if (sc.online)
            {

                sc.SetMsg(gs.State2String());
                sc.SendMessage();
            }
            nextStateRead = stateReadRate + Time.time;
        }
    }

    private GameObject[] MakeEnemyList()
    {
        return GameObject.FindGameObjectsWithTag("Enemy");
    }


    KeyCode ReadKey()
    {
        

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            k = KeyCode.LeftArrow;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            k = KeyCode.RightArrow;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            k = KeyCode.Space;
            stuckTime = Time.time + 0.5f;
        }
        
        if (Time.time > stuckTime)
            k = KeyCode.None;

        return k;

    }

}
