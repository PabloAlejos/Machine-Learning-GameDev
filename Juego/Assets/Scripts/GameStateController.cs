using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

[RequireComponent(typeof(StatesFileManager))]

public class GameStateController : MonoBehaviour
{

    //Controladores
    private StatesFileManager sfm;
    private PlayerController player;
    private SocketController sc;

    private float heatValue;

    private float nextStateRead;
    public float stateReadRate;
    public bool recordStates;

    //Variables de ReadKey()
    bool isShooting;
    KeyCode VerticalInput; //Variable para el eje vertical
    KeyCode HorizontalInput; //Variable para el eje horicontal

    void Start()
    {

        player = FindObjectOfType<PlayerController>();
        player.playerHorizontal += playerHorizontal; //Eventos para los controles Horizontales
        player.PlayerVertical += playerVertical; //Eventos para los controles verticales
        player.playerShoot += playerShooting;

        sfm = FindObjectOfType<StatesFileManager>(); //Encargado de escribir el estado en el csv

        nextStateRead = stateReadRate + Time.time;
        sc = FindObjectOfType<SocketController>();

        //Inicialización del input
        ResetKeys();


    }

    private void playerShooting(KeyCode k)
    {
        isShooting = true;
    }

    private void playerVertical(KeyCode k)
    {
        VerticalInput = k;
    }

    private void playerHorizontal(KeyCode k)
    {
        HorizontalInput = k;
    }


    //Método que es llamado al final de cada fotograma, es decir, cuando todas las fisicas han sido calculadas.
    void FixedUpdate()
    {
        GameState gs;
        Enemy[] enemies;
        PowerUp[] powerUps;
        int[] enemiesProcedence = player.GetComponent<PlayerController>().GetPlayerAttacked();

        heatValue = player.GetComponent<PlayerController>().heatValue;

        if (Time.time > nextStateRead)
        {
            enemies = MakeEnemyList();
            powerUps = FindObjectsOfType<PowerUp>();

            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            gs = new GameState(GenerateTimeStamp(), playerPos, heatValue, powerUps, enemies,enemiesProcedence, VerticalInput, HorizontalInput, isShooting);

            ResetKeys();

            if (recordStates)
                sfm.AddState(gs.State2csv());

            if (sc.online)
            {
                sc.SetMsg(gs.State2csv());
                sc.SendMessage();
            }
            nextStateRead = stateReadRate + Time.time;
        }
    }




    private Enemy[] MakeEnemyList()
    {
        return FindObjectsOfType<Enemy>();
    }


    void ResetKeys()
    {
        isShooting = false;
        VerticalInput = KeyCode.None;
        HorizontalInput = KeyCode.None;
    }

    String GenerateTimeStamp()
    {
        StringBuilder sb = new StringBuilder();
        DateTime now = DateTime.Now;
        sb.Append(now.DayOfYear); //Añadimos el dia del año (0-365)
        sb.Append(now.Hour); //Hora
        sb.Append(now.Minute); //Minutos
        sb.Append(now.Second); //Minutos
        return sb.ToString();
    }

}
