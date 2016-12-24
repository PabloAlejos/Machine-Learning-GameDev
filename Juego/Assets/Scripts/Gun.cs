using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public GameObject bulletType;
    public float fireRate = 10;
    private float nextShotTime;

    // Use this for initialization
    void Start()
    {
        nextShotTime = Time.time + fireRate / 10;
    }


    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(bulletType, transform.position, Quaternion.identity);
            nextShotTime += fireRate / 10;
        }
    }
}
