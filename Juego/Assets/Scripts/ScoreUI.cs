using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    public Text scoreValue;
    public Text higScoreValue;
    private ScoreController sc;

	// Use this for initialization
	void Start () {

        sc = FindObjectOfType<ScoreController>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreValue.text = sc.score.ToString();
        higScoreValue.text = sc.GetHighScore().ToString();
	}
}
