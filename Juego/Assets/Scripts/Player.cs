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



    void Start()
    {
        lasHitKey = KeyCode.None;
        gun = FindObjectOfType<Gun>();
        anim = GetComponent<Animator>();
        ScreenHalfSizeInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
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


        lasHitKey = KeyCode.A;

        if (Input.GetKeyDown("space"))
        {
            lasHitKey = KeyCode.Space;
            gun.Shoot();
        }
        if (Input.GetKeyDown("left"))
        {
            lasHitKey = KeyCode.LeftArrow;
        }
        if (Input.GetKeyDown("right"))
        {
            lasHitKey = KeyCode.RightArrow;
        }


    }

}
