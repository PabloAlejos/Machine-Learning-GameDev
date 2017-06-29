using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public delegate void DeathDelegate();
    public delegate void PowerUpDelegate();
    public event DeathDelegate playerDeath;
    public event PowerUpDelegate CoolGun;
    public event PowerUpDelegate OverPower;


    private SoundController sounds;
    public GameObject destroyAnimation;

    private void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            playerDeath();
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

    }

}
