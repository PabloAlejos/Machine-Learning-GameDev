using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    public float health = 3;

    // Update is called once per frame
    void Update()
    {
        if (IsDead())
            Destroy(gameObject);
    }

    private bool IsDead()
    {
        return health <= 0;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
    }

}
