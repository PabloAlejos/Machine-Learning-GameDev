using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
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

        anim.SetFloat("direction", directon);//Indica el estado de la nave al Animator 

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

        if (Input.GetKeyDown("space"))
        {
            gun.Shoot();
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
