using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {



    Animator anim;
    public float speed = 5;
    Gun gun;
    private float ScreenHalfSizeInWorldUnits;



    void Start () {
        gun = FindObjectOfType<Gun>();
        anim = GetComponent<Animator>();
        ScreenHalfSizeInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
    }
	
	
	void Update () {

        //Control de movimiento
        float directon = Input.GetAxis("Horizontal");
        Vector2 move = Vector2.right * directon * speed * Time.deltaTime;
        transform.Translate(move);

       //Indica el estado de la nave al Animator 
       anim.SetFloat("direction",directon);

        //Evita que el jugadore se salga de la pantalla
        if (transform.position.x < -ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(-ScreenHalfSizeInWorldUnits, transform.position.y);
        }
        if (transform.position.x > ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(ScreenHalfSizeInWorldUnits, transform.position.y);
        }

        if (Input.GetKeyDown("space"))
        {
            // Daña a todos los enemigos en pantalla
            gun.Shoot();
        }

    }
}
