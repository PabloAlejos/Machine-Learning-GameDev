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
	

    public void ChangeScene(int scene)
    {
        if (gameOver)
        {
            SceneManager.LoadScene(scene);
        }
    }

    void OnGameOver()
    {
        Debug.Log("GameOver");
        gameOverScreen.SetActive(true);
        gsc.gameObject.SetActive(false); 
        gameOver = true;
    }

}
