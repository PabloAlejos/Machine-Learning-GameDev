using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public KeyCode lasHitKey;

    private bool botActive;

    // Animator;
    public Sprite[] animSprites;

    //propiedades del jugador

    public float speed = 5;

    [HideInInspector]
    public GameObject[] guns;
    public GameObject gunsOP;
    private float opTime = 0;
    [HideInInspector]
    public float heatValue = 0;
    float firing = 0;
    private float ScreenHalfSizeInWorldUnits = 3;
    Vector2 movement;

    //Eventos
    public delegate void PlayerInputDelegate(KeyCode k);
    public event PlayerInputDelegate playerShoot;
    public event PlayerInputDelegate playerHorizontal;
    public event PlayerInputDelegate PlayerVertical;




    SoundController sounds;

    //Se ejecuta antes de todo
    void Awake()
    {
        FindObjectOfType<SocketController>().keyEvent += keypress;
        FindObjectOfType<SocketController>().ToggleOnline += toogleOnlineSocket;
        sounds = FindObjectOfType<SoundController>();
        opTime = 0;
        Time.timeScale = 1;
    }

    void Start()
    {
        movement = new Vector2(0, 0);
        lasHitKey = KeyCode.None;
        FindGuns();
    }

    private void keypress(Vector3 values)
    {
        firing = values.z;
        movement = new Vector2(values.x, values.y);

    }

    void Update()
    {
        InputRead();
        if (Time.time > opTime)
        {
            gunsOP.SetActive(false);
            FindGuns();
        }
    }

    public void InputRead()
    {

        if (!botActive)
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        transform.Translate(movement.normalized * speed * Time.deltaTime);
        if (firing == 1)
        {
            shoot();
        }
        eventInput(movement);
        PlayerAnimation(movement.x); //Indicamos al aniador la direccion en la que vamos



        //Evita que el jugadore se salga de la pantalla
        if (transform.position.x - transform.localScale.x / 2 < -ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(-ScreenHalfSizeInWorldUnits + transform.localScale.x / 2, transform.position.y);

        }
        else if (transform.position.x + transform.localScale.x / 2 > ScreenHalfSizeInWorldUnits)
        {
            transform.position = new Vector2(ScreenHalfSizeInWorldUnits - transform.localScale.x / 2, transform.position.y);
        }

        if (transform.position.y + transform.localScale.y / 2 >= 9)
        {
            transform.position = new Vector2(transform.position.x, 9 - transform.localScale.y / 2);
        }
        else if (transform.position.y - transform.localScale.y / 2 <= -1)
        {
            transform.position = new Vector2(transform.position.x, -1 + transform.localScale.y / 2);
        }

        lasHitKey = KeyCode.None;

        if (Input.GetKeyDown("space"))
        {
            shoot();
        }


    }




    private void shoot()
    {
        foreach (GameObject g in guns)
        {
            g.GetComponent<Gun>().Shoot();
            heatValue = g.GetComponent<Gun>().heatValue;
            playerShoot(KeyCode.Space);
        }
    }

    private void eventInput(Vector2 movement)
    {
        if (movement.x > 0.5)
            playerHorizontal(KeyCode.RightArrow);
        if (movement.x < -0.5)
            playerHorizontal(KeyCode.LeftArrow);
        if (movement.y > 0.5)
            PlayerVertical(KeyCode.UpArrow);
        if (movement.y < -0.5)
            PlayerVertical(KeyCode.DownArrow);
    }




    //Intercambia los srites
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

    void FindGuns()
    {
        guns = GameObject.FindGameObjectsWithTag("gun");
    }


    void toogleOnlineSocket()
    {
        botActive = !botActive;
    }
}
