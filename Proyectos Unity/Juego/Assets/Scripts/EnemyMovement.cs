using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float maxSpeed = 4;
    public float minSpeed = 0.5f;
    float speed = 1;
    Vector2 target;

    void Start()
    {
    speed = Random.Range(minSpeed,maxSpeed); 
    target  = new Vector2(Random.Range(-2.75f, 2.75f), -2f);
    }

    // Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * 0.025f);
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
