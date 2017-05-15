using UnityEngine;
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

	// Use this for initialization
	void Start () {
        FindObjectOfType<ScoreController>().enabled = true;
        sc = FindObjectOfType<ScoreController>();
        gsc = FindObjectOfType<GameStateController>();
        FindObjectOfType<Player>().playerDeath += OnGameOver;
	}
	
    void Update()
    {

        if (Input.GetKey("return"))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        gsc.gameObject.SetActive(false);
        
        gameOver = true;
    }

}
