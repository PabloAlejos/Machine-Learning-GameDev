using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{

    public GameObject drop;
    // Use this for initialization
    void OnDestroy()
    {
        Instantiate(drop, transform.position, Quaternion.identity);
    }

}
