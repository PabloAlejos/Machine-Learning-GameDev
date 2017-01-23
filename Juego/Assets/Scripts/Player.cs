using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{

    [HideInInspector]
    public KeyCode lasHitKey;

    Animator anim;
    public float speed = 5;
    Gun gun;
    private float ScreenHalfSizeInWorldUnits;

    //Eventos
    public delegate void PlayerInputDelegate(KeyCode k);
    public event PlayerInputDelegate playerInputEvent;
    public delegate void DeathDelegate();
    public event DeathDelegate playerDeath;



    void Start()
    {
        lasHitKey = KeyCode.None;
        gun = FindObjectOfType<Gun>();
        anim = GetComponent<Animator>();
        ScreenHalfSizeInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        FindObjectOfType<EnemySpawner>().spawnEvent += OnEnemySpawn;

    }


    void Update()
    {

        //Control de movimiento
        float directon = Input.GetAxis("Horizontal");
        Vector2 move = Vector2.right * directon * speed * Time.deltaTime;
        transform.Translate(move);

        //Indica el estado de la nave al Animator 
        anim.SetFloat("direction", directon);
        //almacena la última tecla pulsada


        //Evita que el jugadore se salga de la pantalla
        if (transform.position.x < -ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(-ScreenHalfSizeInWorldUnits, transform.position.y);
        }
        if (transform.position.x > ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(ScreenHalfSizeInWorldUnits, transform.position.y);
        }


        lasHitKey = KeyCode.None;

        if (Input.GetKey("left"))
        {
            if (playerInputEvent != null)
                playerInputEvent(KeyCode.LeftArrow);
        }

        if (Input.GetKey("right"))
        {
            if (playerInputEvent != null)
                playerInputEvent(KeyCode.RightArrow);
        }


        if (Input.GetKeyDown("space"))
        {
            if (playerInputEvent != null)
            {
                playerInputEvent(KeyCode.Space);
                gun.Shoot();
            }

        }



    }

    void onPassTrough()
    {
        Debug.Log("Death");
        if (playerDeath != null)
        {
            playerDeath();
        }
    }


    //Evento que es llamado cuando aparece un enemigo
    //Es se suscribe a la lista de enventos para ver cuando es destruido
    void OnEnemySpawn(Enemy e)
    {
        e.passTroughEvent += onPassTrough;
    }
}
