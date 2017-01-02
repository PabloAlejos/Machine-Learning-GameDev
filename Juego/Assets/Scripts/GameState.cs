using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class GameState
{
    public Vector2 playerPosition;
    public List<Enemy> enemiesPosition;

    private KeyCode lastHitKey;

    public GameState(Vector2 playerPosition, List<Enemy> enemiesPosition,double score, KeyCode lastHitKey)
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

        foreach (Enemy v in enemiesPosition)
        {
            if ( v!= null)
            sb.Append(MakeValueCsvFriendly((Vector2)v.transform.position)).Append(",");
            else
                sb.Append(MakeValueCsvFriendly(v)).Append(",");
        }

        sb.Append(MakeValueCsvFriendly(lastHitKey)).Append(",");

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
            return ((Vector2)value).x.ToString("n2") + "," + (((Vector2)value).y).ToString("n2");
        }

        if (value is KeyCode)
        {
            return value.ToString();
        }

        if (value == null) return "";

        string output = value.ToString();

        if (output.Contains(",") || output.Contains("\""))
            output = '"' + output.Replace("\"", "\"\"") + '"';

        return output;

    }


}
