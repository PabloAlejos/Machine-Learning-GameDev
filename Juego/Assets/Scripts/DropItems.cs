using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{

    public GameObject drop;


    // Use this for initialization
    void OnDestroy()
    {

        for (int i = 0; i < 5; i++)
        {
            GameObject item = (GameObject)Instantiate(drop, transform.position, Quaternion.identity);

        }
    }

}
