using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{

    //Direcciones en las que se van a lanzar los "rayos"
    Vector3[] directions;
    private int nrays = 27;
    float[] enemiesProcedence;
    float[] powerUpProcedence;

    void Start()
    {
        enemiesProcedence = new float[nrays];
        powerUpProcedence = new float[nrays];
        directions = new Vector3[] {
            new Vector3(0, 1, 0),
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0)};
    }

    public int getRayAmount()
    {
        return nrays;
    }

    public float[] getSuroundingEnemies()
    {
        return ScanForEnemies();
    }


    public float[] ScanForEnemies()
    {
        enemiesProcedence = new float[nrays];
        powerUpProcedence = new float[nrays];
        //Variables para los "hits" y el vector de enemigos
        RaycastHit hit = new RaycastHit();
        //Lanzamiento de los rayos para ver si hay enemigos
        for (int i = 0; i < directions.Length; i++)
        {
            //Debug.DrawLine(transform.position, (transform.position ), Color.red);
            //enemiesProcedence[i] = shootRayCast((transform.position + transform.TransformDirection(directions[i])), castArray[i]);
            Quaternion spreadAngleNegative10 = Quaternion.AngleAxis(-10.0f, new Vector3(0, 0, 1));
            Quaternion spreadAnglePositive10 = Quaternion.AngleAxis(10.0f, new Vector3(0, 0, 1));
            Quaternion spreadAngleNegative20 = Quaternion.AngleAxis(-20.0f, new Vector3(0, 0, 1));
            Quaternion spreadAnglePositive20 = Quaternion.AngleAxis(20.0f, new Vector3(0, 0, 1));
            Quaternion spreadAngleNegative30 = Quaternion.AngleAxis(-30.0f, new Vector3(0, 0, 1));
            Quaternion spreadAnglePositive30 = Quaternion.AngleAxis(30.0f, new Vector3(0, 0, 1));
            Quaternion spreadAngleNegative40 = Quaternion.AngleAxis(-40.0f, new Vector3(0, 0, 1));
            Quaternion spreadAnglePositive40 = Quaternion.AngleAxis(40.0f, new Vector3(0, 0, 1));

            shootRayCast(i * 9, transform.position + transform.TransformDirection(directions[i]) * 10, hit);
            shootRayCast(i * 9 + 1, transform.position + transform.TransformDirection(spreadAnglePositive10 * directions[i]) * 10, hit);
            shootRayCast(i * 9 + 2, transform.position + transform.TransformDirection(spreadAngleNegative10 * directions[i]) * 10, hit);
            shootRayCast(i * 9 + 3, transform.position + transform.TransformDirection(spreadAnglePositive20 * directions[i]) * 10, hit);
            shootRayCast(i * 9 + 4, transform.position + transform.TransformDirection(spreadAngleNegative20 * directions[i]) * 10, hit);
            shootRayCast(i * 9 + 5, transform.position + transform.TransformDirection(spreadAnglePositive30 * directions[i]) * 10, hit);
            shootRayCast(i * 9 + 6, transform.position + transform.TransformDirection(spreadAngleNegative30 * directions[i]) * 10, hit);
            shootRayCast(i * 9 + 7, transform.position + transform.TransformDirection(spreadAnglePositive40 * directions[i]) * 10, hit);
            shootRayCast(i * 9 + 8, transform.position + transform.TransformDirection(spreadAngleNegative40 * directions[i]) * 10, hit);

        }

        float[] retorno = enemiesProcedence.Concat(powerUpProcedence).ToArray<float>();
        return retorno;
    }

    private void shootRayCast(int index, Vector3 dir, RaycastHit hit)
    {
        powerUpProcedence[index] = 999.00f;
        enemiesProcedence[index] = 999.00f;
        
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                Debug.DrawLine(transform.position, dir, Color.blue);
                enemiesProcedence[index] = hit.distance;
                powerUpProcedence[index] = 999.00f;
            }
            else if (hit.transform.tag == "PU1" || hit.transform.tag == "PU2")
            {
                Debug.DrawLine(transform.position, dir, Color.yellow);
                powerUpProcedence[index] = hit.distance;
                enemiesProcedence[index] = 999.00f;
            }
        }
    }
}
