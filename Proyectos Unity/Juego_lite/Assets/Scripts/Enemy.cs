using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public delegate void DeathDelegate(float points, Enemy e);
    public delegate void PassTroughDelegate();
    public event DeathDelegate deathEvent;
    public event PassTroughDelegate passTroughEvent;

    [HideInInspector]
    public float health;
    public float maxHealth;
    private Image HealthBar;

    public GameObject destroyAnimation;
    public float scorePoints = 1;
    ScoreController sc;

    SoundController sounds;

    void Start()
    {
        sc = FindObjectOfType<ScoreController>();
        sounds = FindObjectOfType<SoundController>();
        HealthBar = transform.FindChild("EnemyHealth").FindChild("HealthBG").FindChild("Health").GetComponent<Image>();
        health = maxHealth;
    }

    void Update()
    {

        if (IsDead())
        {
            if (deathEvent != null)
            {
                deathEvent(scorePoints, this);
            }
            Instantiate(destroyAnimation, transform.position, Quaternion.identity);
            sounds.enemieExplosion.Play();
            Destroy(gameObject);
        }
        else
        {
            if (transform.position.y < -1)
            {
                sc.SetScore(sc.GetScore() -scorePoints);
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
        PlayHitEffect();
        health -= amount;
        HealthBar.fillAmount = Health / maxHealth;
    }

    public float Health { get { return health; } }

    void PlayHitEffect()
    {
        if (!sounds.enemieHit.isPlaying)
            sounds.enemieHit.Play();
    }

}
