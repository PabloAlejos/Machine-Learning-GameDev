using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public delegate void DeathDelegate(float points, Enemy e);
    public delegate void PassTroughDelegate();
    public event DeathDelegate deathEvent;
    public event PassTroughDelegate passTroughEvent;

    public float health = 3;
    public GameObject destroyAnimation;
    public float scorePoints = 1;
  
    void Update()
    {

        if (IsDead())
        {
            if (deathEvent != null)
            {
                deathEvent(scorePoints, this);
            }
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            if (transform.position.y <  -1)
            {
                if (passTroughEvent != null)
                {
                    passTroughEvent();  
                }
                
            }
         
        }

    }

    private bool IsDead()
    {

        return health <= 0;
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
    }

}
