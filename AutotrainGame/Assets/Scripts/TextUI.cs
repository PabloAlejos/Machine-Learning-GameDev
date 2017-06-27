using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{

    public Text scoreValue;
    public Text higScoreValue;
    public Text iteration;

    private ScoreController sc;
    private GameConfig gconfig;
    void Start()
    {

        gconfig = FindObjectOfType<GameConfig>();
        sc = FindObjectOfType<ScoreController>();

        iteration.text = gconfig.getIteration().ToString() + "/" + gconfig.nIterations.ToString();
    }

    void Update()
    {
        scoreValue.text = sc.GetScore().ToString();
        higScoreValue.text = sc.GetHighScore().ToString();
    }
}
