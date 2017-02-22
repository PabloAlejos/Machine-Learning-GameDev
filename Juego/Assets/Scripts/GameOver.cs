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
            SceneManager.LoadScene(0);
        }
    }

    void OnGameOver()
    {

        Debug.Log("GameOver");
        gameOverScreen.SetActive(true);
        gsc.gameObject.SetActive(false);
        Time.timeScale = 0;
        gameOver = true;
    }

}
