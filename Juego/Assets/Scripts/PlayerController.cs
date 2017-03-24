using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public KeyCode lasHitKey;

    // Animator;
    public Sprite[] animSprites;

    //propuedades del jugador
    public float speed = 5;
    [HideInInspector]
    public GameObject[] guns;
    private float ScreenHalfSizeInWorldUnits = 3;

    //Eventos
    public delegate void PlayerInputDelegate(KeyCode k);
    public delegate void DeathDelegate();
    public event DeathDelegate playerDeath;


    //Se ejecuta antes de todo
    void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        lasHitKey = KeyCode.None;
        guns = GameObject.FindGameObjectsWithTag("gun");
        FindObjectOfType<EnemySpawner>().spawnEvent += OnEnemySpawn;
    }


    void Update()
    {
        InputRead();
    }

    public void InputRead()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        PlayerAnimation(movement.x);
        transform.Translate (movement.normalized * speed * Time.deltaTime);


        //Evita que el jugadore se salga de la pantalla
        if (transform.position.x - transform.localScale.x / 2 < -ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(-ScreenHalfSizeInWorldUnits + transform.localScale.x / 2, transform.position.y);
        }else if (transform.position.x + transform.localScale.x/2 > ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(ScreenHalfSizeInWorldUnits - transform.localScale.x/2, transform.position.y);
        }

        if (transform.position.y + transform.localScale.y / 2 >= 9)
        {
            transform.position = new Vector2(transform.position.x, 9 - transform.localScale.y / 2);
        }else if (transform.position.y - transform.localScale.y/2 <= -1)
        {
            transform.position = new Vector2(transform.position.x, -1 + transform.localScale.y / 2);
        }


        lasHitKey = KeyCode.None;

        if (Input.GetKeyDown("space"))
        {
            foreach (GameObject g in guns)
            {
                g.GetComponent<Gun>().Shoot();
            }
           
           

        }
    }

    void onPassTrough()
    {
        
        if (playerDeath != null)
        {
            Debug.Log("Death");
            playerDeath();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Debug.Log("Enemy!");
                //Añadir animacion muerte
                playerDeath();
                break;
            case "PU1":
                Debug.Log("PU!");
                guns[0].GetComponent<Gun>().CoolGun(20);
                guns[1].GetComponent<Gun>().CoolGun(20);
                Destroy(other.gameObject);
                break;
        }

    }


    //Evento que es llamado cuando aparece un enemigo
    //Es se suscribe a la lista de enventos para ver cuando es destruido
    void OnEnemySpawn(Enemy e)
    {
        e.passTroughEvent += onPassTrough;
    }

    void PlayerAnimation(float dirx)
    {
        if (dirx > 0.5f)
        {
            GetComponent<SpriteRenderer>().sprite = animSprites[1];
        }
        else if (dirx < -0.5f)
        {
            GetComponent<SpriteRenderer>().sprite = animSprites[2];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = animSprites[0];
        }
    }

}
