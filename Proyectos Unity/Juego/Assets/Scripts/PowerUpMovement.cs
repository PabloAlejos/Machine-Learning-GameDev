using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMovement : MonoBehaviour {

    public Vector2 target;
    public float speed;

    // Use this for initialization
    void Start () {
       target  = new Vector2(Random.Range(-2.8f, 2.8f), -1.5f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * 0.1f);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
