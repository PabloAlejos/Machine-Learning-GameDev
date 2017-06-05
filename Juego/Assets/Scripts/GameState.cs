using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class GameState
{
    private Vector2 playerPosition;
    private KeyCode VKey = KeyCode.None;
    private KeyCode HKey = KeyCode.None;
    string timeStamp = "";
    bool isShooting = false;
    private int maxEnemies = 6;
    private float HeatGunValue;
    private float[] surroundingInfo;
    private int score;


    public GameState(string timeStamp, Vector2 playerPosition, float HeatGunValue, float[] surroundingInfo, int score, KeyCode VKey, KeyCode HKey, bool isShooting)
    {

        this.timeStamp = timeStamp;
        this.playerPosition = playerPosition;
        this.HeatGunValue = HeatGunValue;
        this.VKey = VKey;
        this.HKey = HKey;
        this.isShooting = isShooting;
        this.surroundingInfo = surroundingInfo;
        this.score = score;

    }

    //Método que traduce el estado a una cadena de texto csv
    public String State2csv()
    {

        StringBuilder sb = new StringBuilder();

        sb.Append(timeStamp).Append(",");
        sb.Append(MakeValueCsvFriendly(playerPosition)).Append(",");
        sb.Append(HeatGunValue).Append(",");
        sb.Append(MakeValueCsvFriendly(surroundingInfo));
        sb.Append(score).Append(",");

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


        if (value is float[])
        {
            for (int i = 0; i < surroundingInfo.Length; i++)
            {
                    sb.Append(surroundingInfo[i].ToString("0.00"));
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
