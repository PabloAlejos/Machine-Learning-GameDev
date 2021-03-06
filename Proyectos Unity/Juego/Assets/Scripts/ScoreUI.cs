﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    public Text scoreValue;
    public Text higScoreValue;
    public Text iteration;
    private ScoreController sc;

	// Use this for initialization
	void Start () {

        sc = FindObjectOfType<ScoreController>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreValue.text = sc.GetScore().ToString();
        higScoreValue.text = sc.GetHighScore().ToString();
	}
}
