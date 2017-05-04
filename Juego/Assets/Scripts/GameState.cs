using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class GameState
{
    private Vector2 playerPosition;
    private Enemy[] enemies;
    private KeyCode VKey = KeyCode.None;
    private KeyCode HKey = KeyCode.None;
    string timeStamp = "";
    bool isShooting = false;
    private int maxEnemies = 6;
    private float HeatGunValue;
    private PowerUp[] powerUps;
    private int[] surroundingEnemies;


    public GameState(string timeStamp, Vector2 playerPosition, float HeatGunValue, PowerUp[] powerUps, Enemy[] enemies,int[] sorroundingEnemies, KeyCode VKey, KeyCode HKey, bool isShooting)
    {

        this.timeStamp = timeStamp;
        this.playerPosition = playerPosition;
        this.HeatGunValue = HeatGunValue;
        this.powerUps = powerUps;
        this.enemies = enemies;
        this.VKey = VKey;
        this.HKey = HKey;
        this.isShooting = isShooting;
        this.surroundingEnemies = sorroundingEnemies;

    }

    //Método que traduce el estado a una cadena de texto csv
    public String State2csv()
    {

        StringBuilder sb = new StringBuilder();

        sb.Append(timeStamp).Append(",");
        sb.Append(MakeValueCsvFriendly(playerPosition)).Append(",");
        sb.Append(HeatGunValue).Append(",");
        sb.Append(MakeValueCsvFriendly(powerUps));
        sb.Append(MakeValueCsvFriendly(enemies));
        sb.Append(MakeValueCsvFriendly(surroundingEnemies));

        sb.Append(MakeValueCsvFriendly(VKey)).Append(",");
        sb.Append(MakeValueCsvFriendly(HKey)).Append(",");
        sb.Append(isShooting);

        return sb.ToString();

    }


    //
    //http://stackoverflow.com/questions/2422212/how-to-create-csv-excel-file-c
    //

    //Método que facilita el paso a csv, tratando cada tipo de dato de una forma diferente
    private string MakeValueCsvFriendly(object value)
    {
        StringBuilder sb = new StringBuilder();
        if (value is Vector2)
        {
            return ((Vector2)value).x.ToString("0.00") + "," + (((Vector2)value).y).ToString("0.00");
        }

        if (value is PowerUp[])
        {
            for (int i = 0; i < 2; i++)
            {
                if (i >= powerUps.Length)
                {
                    sb.Append(("999,999,"));
                }
                else
                {
                    if (powerUps[i] != null)
                    {
                        sb.Append(powerUps[0].transform.position.x).Append(",").Append(powerUps[0].transform.position.y).Append(",");
                    }
                }
            }
            return sb.ToString();
        }

        

        if (value is Enemy[])
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (i >= enemies.Length)
                {
                    sb.Append(("999,999,"));
                    sb.Append("0").Append(",");
                }
                else
                {
                    if (enemies[i] != null)
                    {
                        sb.Append(MakeValueCsvFriendly((Vector2)enemies[i].transform.position)).Append(",");
                        sb.Append(MakeValueCsvFriendly(enemies[i].health)).Append(",");
                    }

                    else
                        sb.Append(MakeValueCsvFriendly(enemies[i])).Append(",");
                }

            }
            return sb.ToString();
        }

        if (value is int[])
        {
            foreach(int i in surroundingEnemies)
            {
                sb.Append(i);
                sb.Append(",");
            }
            return sb.ToString();
        }


            if (value is KeyCode)
        {
            return value.ToString();
        }

        if (value is double)
        {
            return (value.ToString());
        }

        if (value == null) return "";

        string output = value.ToString();

        if (output.Contains(",") || output.Contains("\""))
            output = '"' + output.Replace("\"", "\"\"") + '"';

        return output;

    }


}
