using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public KeyCode lasHitKey;

    public bool botActive;
    SocketController sc;
    // Animator;
    public Sprite[] animSprites;


    //propiedades del jugador
    public GameObject destroyAnimation;
    public float speed = 5;
    public int lives = 3;
    [HideInInspector]
    public GameObject[] guns;
    public GameObject gunsOP;
    private float opTime = 0;
    [HideInInspector]
    public float heatValue = 0;
    private float ScreenHalfSizeInWorldUnits = 3;


    //Eventos
    public delegate void PlayerInputDelegate(KeyCode k);
    public event PlayerInputDelegate playerShoot;
    public event PlayerInputDelegate playerHorizontal;
    public event PlayerInputDelegate PlayerVertical;
    public delegate void DeathDelegate();
    public event DeathDelegate playerDeath;

    SoundController sounds;

    //Direcciones en las que se van a lanzar los "rayos"
    Vector3[] directions = new Vector3[] {
            new Vector3(0, 1, 0), new Vector3(1, 1, 0),
            new Vector3(1, 0, 0),new Vector3(1, -1, 0),
            new Vector3(0, -1, 0),new Vector3(-1, -1, 0),
            new Vector3(-1, 0, 0),new Vector3(-1, 1, 0)};



    //Se ejecuta antes de todo
    void Awake()
    {
        sounds = FindObjectOfType<SoundController>();
        sc = FindObjectOfType<SocketController>();
        opTime = 0;
        Time.timeScale = 1;
    }

    void Start()
    {

        lasHitKey = KeyCode.None;
        FindGuns();
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
        Vector2 movement = new Vector2(0, 0);
        if (botActive && sc.online)
        {
            if (sc.sPrueba.Length > 0)
            {
                movement = new Vector2(Int32.Parse(sc.sPrueba[1]), Int32.Parse(sc.sPrueba[3]));
                if (Int32.Parse(sc.sPrueba[5]) == 1)
                {
                    shoot();
                }
            }

        }
        else
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }



        eventInput(movement);

        PlayerAnimation(movement.x);
        transform.Translate(movement.normalized * speed * Time.deltaTime);


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


    //Busca enemigos en las cercanías
    public int[] GetPlayerAttacked()
    {

        //Variables para los "hits" y el vector de enemigos
        RaycastHit[] castArray = new RaycastHit[8];
        int[] enemiesProcedence = new int[8];

        //Lanzamiento de los rayos para ver si hay enemigos
        for (int i = 0; i < 8; i++)
        {
            Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(directions[i]), Color.red);
            enemiesProcedence[i] = shootRayCast(transform.position + transform.TransformDirection(directions[i]), castArray[i]);
        }

        //Debug.Log("[" + enemiesProcedence[0] + "," + enemiesProcedence[1] + "," + enemiesProcedence[2] + "," + enemiesProcedence[3] + "," + enemiesProcedence[4] + "," + enemiesProcedence[5] + "," + enemiesProcedence[6] + "," + enemiesProcedence[7] + "]");
        return enemiesProcedence;
    }


    private int shootRayCast(Vector3 dir, RaycastHit hit)
    {
        int retorno = 0;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            if (hit.distance < 2 && hit.transform.tag == "Enemy")
            {
                retorno = 1;
            }
            else if (hit.distance < 2 && hit.transform.tag == "PU1")
            {
                retorno = 2;
            }
            else if (hit.distance < 2 && hit.transform.tag == "PU2")
            {
                retorno = 3;
            }

        }
        return retorno;
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


    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                playerDeath();
                Instantiate(destroyAnimation, transform.position, Quaternion.identity);
                sounds.audio_playerExlosion.Play();
                Destroy(gameObject);
                Destroy(other.gameObject);
                break;
            case "PU1":
                guns[0].GetComponent<Gun>().CoolGun(20);
                guns[1].GetComponent<Gun>().CoolGun(20);
                Destroy(other.gameObject);
                sounds.itemPickUp.Play();
                break;
            case "PU2":
                gunsOP.SetActive(true);
                //gunsOP.GetComponent<Gun>().HeatValue =this.heatValue;
                Destroy(other.gameObject);
                opTime = Time.time + 5;
                FindGuns();
                sounds.itemPickUp.Play();
                break;

        }

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

}
