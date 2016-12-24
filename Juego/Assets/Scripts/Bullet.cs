using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 15;
    public float damage = 1;
    public GameObject hitEffect;
   
	
	// Update is called once per frame
	void Update () {
        transform.Translate (Vector3.up * speed * Time.deltaTime);
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Enemy")
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            hit.transform.SendMessage("TakeDamage", damage);
        }
        Destroy(gameObject);
    }
}
