using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class GameState
{
    public Vector2 playerPosition;
    public GameObject[] enemiesPosition;
    private KeyCode lastHitKey;
    public int maxEnemies = 4 ;

    public GameState(Vector2 playerPosition, GameObject[] enemiesPosition, KeyCode lastHitKey)
    {
        this.playerPosition = playerPosition;
        this.enemiesPosition = enemiesPosition;
        this.lastHitKey = lastHitKey;
    }


    //Método que traduce el estado a una cadena de texto csv
    public String State2csv()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(MakeValueCsvFriendly(playerPosition)).Append(",");

        for (int i = 0; i < maxEnemies; i++)
        {
            if (i >= enemiesPosition.Length)
            {
                sb.Append(("999,999,"));
            }
            else
            {
                if (enemiesPosition[i] != null)
                    sb.Append(MakeValueCsvFriendly((Vector2)enemiesPosition[i].transform.position)).Append(",");
                else
                    sb.Append(MakeValueCsvFriendly(enemiesPosition[i])).Append(",");
            }

        }
        sb.Append(MakeValueCsvFriendly(lastHitKey));

        Debug.Log(sb.ToString().Split(',').Length - 1);
        return sb.ToString();

    }

    //
    //http://stackoverflow.com/questions/2422212/how-to-create-csv-excel-file-c
    //

    //Método que facilita el paso a csv, tratando cada tipo de dato de una forma diferente
    private string MakeValueCsvFriendly(object value)
    {

        if (value is Vector2)
        {
            return ((Vector2)value).x.ToString("0.00") + "," + (((Vector2)value).y).ToString("0.00");
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
