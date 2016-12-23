using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float speed = 3;
	
    // Update is called once per frame
	void Update () {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
	}

}
