using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    private float score = 0;

    // Use this for initialization
    void Start() {
        score = 0;
        FindObjectOfType<EnemySpawner>().spawnEvent += OnEnemySpawn;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(score);
	}


    void OnEnemyDie(float points, Enemy e)
    {
        score += points * e.transform.position.y;
        e.deathEvent -= OnEnemyDie;
    }

    void OnEnemySpawn(Enemy e)
    {
        FindObjectOfType<Enemy>().deathEvent += OnEnemyDie;
    }

}
