using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public KeyCode lasHitKey;

    // Animator anim;
    public float speed = 5;
    GameObject[] guns;
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

        //Control de movimiento
        float directon = Input.GetAxis("Horizontal");
        Vector2 move = Vector2.right * directon * speed * Time.deltaTime;
        transform.Translate(move);


        //Evita que el jugadore se salga de la pantalla
        if (transform.position.x < -ScreenHalfSizeInWorldUnits - 5)
        {
            transform.position = new Vector2(-ScreenHalfSizeInWorldUnits - 5, transform.position.y);
        }
        if (transform.position.x > ScreenHalfSizeInWorldUnits - 5)
        {
            transform.position = new Vector2(ScreenHalfSizeInWorldUnits - 5, transform.position.y);
        }


        lasHitKey = KeyCode.None;

        if (Input.GetKeyDown("space"))
        {

            guns[0].GetComponent<Gun>().Shoot();
            guns[1].GetComponent<Gun>().Shoot();

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
