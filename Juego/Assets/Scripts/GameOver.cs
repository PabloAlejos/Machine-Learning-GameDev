using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour {
    
    public GameObject gameOverScreen;
    public Text score;
    bool gameOver;
    GameStateController gsc;

	// Use this for initialization
	void Start () {
        gsc = FindObjectOfType<GameStateController>();
        FindObjectOfType<PlayerController>().playerDeath += OnGameOver;
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
