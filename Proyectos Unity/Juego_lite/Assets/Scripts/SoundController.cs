﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{


    public AudioClip shoot;
    public AudioClip hit;
    public AudioClip pickUp;
    public AudioClip explosion;
    public AudioClip explosion2;
    public AudioClip explosion3;
    public AudioClip playerExplosion;

    [HideInInspector]
    public AudioSource gun;
    [HideInInspector]
    public AudioSource itemPickUp;
    [HideInInspector]
    public AudioSource enemieHit;
    [HideInInspector]
    public AudioSource enemieExplosion;
    [HideInInspector]
    public AudioSource enemieExplosion2;
    [HideInInspector]
    public AudioSource enemieExplosion3;
    [HideInInspector]
    public AudioSource audio_playerExlosion;



    private AudioSource music;
    public Text muteText;

    void Awake()
    {
        gun = AddAudio(shoot, false, false, 1);
        itemPickUp = AddAudio(pickUp, false, false, 1);
        enemieHit = AddAudio(hit, false, false, 0.5f);
        enemieExplosion = AddAudio(explosion, false, false, 0.5f);
        enemieExplosion2 = AddAudio(explosion2, false, false, 0.5f);
        enemieExplosion3 = AddAudio(explosion3, false, false, 0.5f);
        audio_playerExlosion = AddAudio(playerExplosion, false, false, 0.5f);
    }


    // Use this for initialization
    void Start()
    {
        music = FindObjectOfType<Camera>().GetComponent<AudioSource>();
        AudioListener.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MuteMusic();
        }
    }

    public void MuteMusic()
    {
        if (music.mute)
        {
            music.mute = false;
            muteText.text = "On";

        }
        else
        {
            music.mute = true;
            muteText.text = "Off";
        }

    }

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake,float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;

    }

}