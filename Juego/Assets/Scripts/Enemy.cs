using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public delegate void DeathDelegate(float points, Enemy e);
    public event DeathDelegate deathEvent;


    public float health = 3;
    public GameObject destroyAnimation;
    public float scorePoints = 1;


    // Update is called once per frame
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
            
    }

    private bool IsDead()
    {
       
        return health <= 0;
    }


    //Función que elimina al enemigo cuando da
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
    }

}
