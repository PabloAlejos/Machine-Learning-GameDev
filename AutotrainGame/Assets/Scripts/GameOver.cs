﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour {
    
    public GameObject gameOverScreen;
    public Text score;
    bool gameOver;
    ScoreController sc;
    GameStateController gsc;
    GameConfig gconfig;

	// Use this for initialization
	void Start () {
        FindObjectOfType<ScoreController>().enabled = true;
        sc = FindObjectOfType<ScoreController>();
        gsc = FindObjectOfType<GameStateController>();
        FindObjectOfType<Player>().playerDeath += OnGameOver;
        gconfig = FindObjectOfType<GameConfig>();
	}
	
    void Update()
    {

        if (Input.GetKey("return"))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnGameOver()
    {
        sc.SetScore(sc.GetScore() - 1000);
        gameOverScreen.SetActive(true);
        gsc.gameObject.SetActive(false);
        gconfig.GameOver();
        gameOver = true;
        SceneManager.LoadScene(1);
    }

}
