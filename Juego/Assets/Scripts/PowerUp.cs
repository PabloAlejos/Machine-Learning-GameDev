using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public int type;
    SoundController sounds;

    void Start()
    {
        sounds = FindObjectOfType<SoundController>();
    }

    void Activate(int type)
    {
        switch (type)
        {
            case 1:
                GameObject[] guns = GameObject.FindGameObjectsWithTag("gun");
                guns[0].GetComponent<Gun>().CoolGun(20);
                guns[1].GetComponent<Gun>().CoolGun(20);
                sounds.itemPickUp.Play();
                Destroy(gameObject);
                break;
            case 2:
                PlayerController p = FindObjectOfType<PlayerController>();
                p.gunsOP.SetActive(true);
                p.opTime = Time.time + 5;
                Destroy(gameObject);
                break;
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            Activate(type);
    }
}
