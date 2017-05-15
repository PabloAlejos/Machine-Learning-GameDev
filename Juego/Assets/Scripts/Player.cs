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


    public GameObject destroyAnimation;


    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy")
        {
            playerDeath();
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            //sounds.audio_playerExlosion.Play();
            Destroy(gameObject);
            Destroy(other.gameObject);
        }

    }

}
