using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    private Text scoreValue;
    private ScoreController sc;
    

	// Use this for initialization
	void Start () {
        scoreValue = GetComponent<Text>();
        sc = FindObjectOfType<ScoreController>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreValue.text = sc.score.ToString();
	}
}
