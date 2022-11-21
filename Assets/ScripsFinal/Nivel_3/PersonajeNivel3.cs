using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeNivel3 : MonoBehaviour
{
    public int velocity = 8, velSalto = 5, salto = 3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    public GameObject humo;
    private Nivel3Controller gameManager;


    const int ANI_QUIETO = 0;
    const int ANI_CAMINAR = 1;
    const int ANI_SALTO = 3;
    const int ANI_ATAQUE = 5;
    const int ANI_TREPAR = 6;
    const int ANI_DANIO = 7;
    const int ANI_MUERTO = 8;

    const int ANI_JETPACK = 9;
    const int ANI_OTHER = 12;

    int ani = 0, cont;
    Vector3 lastCheckpointPosition;

    void Start()
    {
        cont = salto;
        Debug.Log("Iniciando script de player");
        gameManager = FindObjectOfType<Nivel3Controller>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ani == 0)
        {
            if (Input.GetKeyDown("z"))
            {
                ChangeAnimation(ANI_ATAQUE);
            }
            else
            {
                Movimientos();
            }
        }
        else if (ani == 1) Libre();

        if(gameManager.check==2){
            if(transform.position.x <= 213f){
                gameManager.PerX=transform.position.x;
                Debug.Log("Pos: " + gameManager.PerX + " - " + transform.position.x);
            }
        }

    }

    void Movimientos()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sr.flipX = false;
            ChangeAnimation(ANI_CAMINAR);
            rb.velocity = new Vector2(velocity, rb.velocity.y);

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            sr.flipX = true;
            ChangeAnimation(ANI_CAMINAR);
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANI_QUIETO);
            ChangeAnimation(ANI_OTHER);
        }
        if (Input.GetKeyDown(KeyCode.Space) && cont > 0)
        {
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(ANI_SALTO);
            cont--;
        }
    }
    void Libre()
    {
        ChangeAnimation(ANI_JETPACK);
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sr.flipX = false;
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);

            var humoPosition = transform.position + new Vector3(-0.4f, -1.5f, 0);
            var gb = Instantiate(humo, humoPosition, Quaternion.identity);
        }


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        cont = salto;

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Limites")
        {
            ani = 0;
            gameManager.inicio = false;
            gameManager.muerto = true;
            rb.velocity = new Vector2(0, 0);
            if(gameManager.check==0) transform.position = new Vector3(-9.5f, -2, 0);
            else if(gameManager.check==1) transform.position = new Vector3(135.5f, -2, 0);
            else if(gameManager.check==2) transform.position = new Vector3(212f, -2, 0);
        }

    }
    void OnTriggerEnter2D(Collider2D other)//para reconocer el checkponit(transparente)
    {
        if (other.gameObject.tag == "JetPack")
        {
            ani = 1;
            gameManager.inicio = true;
            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<SpriteRenderer>().enabled = false;
            //Destroy(other.gameObject);
        }
        if (other.gameObject.name == "Flag")
        {
            ani = 0;
            lastCheckpointPosition = transform.position;
            other.GetComponent<Collider2D>().enabled = false;
            gameManager.inicio = false;
            gameManager.check = 1;
        }
        if (other.gameObject.name == "Flag2")
        {
            ani = 0;
            lastCheckpointPosition = transform.position;
            other.GetComponent<Collider2D>().enabled = false;
            gameManager.inicio = false;
            gameManager.check = 2;
        }
    }
    private void ChangeAnimation(int a)
    {
        animator.SetInteger("Estado", a);
    }
}