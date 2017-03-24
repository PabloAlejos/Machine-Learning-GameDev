using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float maxSpeed = 3;
    public float minSpeed = 0.5f;
    float speed = 1;
    float offset = 0;
	
    void Start()
    {
    speed = Random.Range(minSpeed,maxSpeed);
    offset = Random.Range(-1, 1);
    }

    // Update is called once per frame
	void Update () {
        Vector2 target = new Vector2(Mathf.Cos((Time.time * offset) * 2) * 2.8f, -1.5f);
        //transform.Translate(movement * speed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * 0.025f);
	}

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
