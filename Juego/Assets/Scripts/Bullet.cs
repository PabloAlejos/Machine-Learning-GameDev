using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 15;

	
	// Update is called once per frame
	void Update () {
        transform.Translate (Vector3.up * speed * Time.deltaTime);
	}
}
