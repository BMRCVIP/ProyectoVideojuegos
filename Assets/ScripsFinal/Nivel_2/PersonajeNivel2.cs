using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeNivel2 : MonoBehaviour
{
    public int velocity = 4, veloCorrer = 8, velSalto = 5, salto = 3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    CapsuleCollider2D cc;
    public GameObject bala;
    public GameObject fuegoBala;

    const int ANI_QUIETO = 0;
    const int ANI_CAMINAR = 1;
    const int ANI_CORRER = 2;
    const int ANI_SALTO = 3;
    const int ANI_SLIDE = 4;
    const int ANI_ATAQUE = 5;
    const int ANI_TREPAR = 6;
    const int ANI_DANIO = 7;
    const int ANI_MUERTO = 8;
    const int ANI_GUN = 11;
    const int ANI_OTHER = 12;
    int ani = 0, cont;
    float dir = 1.2f;
    float gravedadInicial;
    Vector3 lastCheckpointPosition;
    private Nivel2Controller gameManager;
    void Start()
    {
        cont = salto;
        gameManager = FindObjectOfType<Nivel2Controller>();
        gameManager.LoadGame();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        cc = GetComponent<CapsuleCollider2D>();
        gravedadInicial = rb.gravityScale;  
    }
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
        else if (ani == 3){
            ChangeAnimation(ANI_GUN);
            Gun();
        }


        

    }

    void Movimientos()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sr.flipX = false;
            dir = 2;

            if (Input.GetKey("x"))
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ChangeAnimation(ANI_SLIDE);
                    rb.velocity = new Vector2(veloCorrer, rb.velocity.y);
                }
                else
                {
                    ChangeAnimation(ANI_CORRER);
                    rb.velocity = new Vector2(veloCorrer, rb.velocity.y);
                }
            }
            else
            {
                ChangeAnimation(ANI_CAMINAR);
                rb.velocity = new Vector2(velocity, rb.velocity.y);
            }

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            sr.flipX = true;
            dir = -2;

            if (Input.GetKey("x"))
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ChangeAnimation(ANI_SLIDE);
                    rb.velocity = new Vector2(-veloCorrer, rb.velocity.y);
                }
                else
                {
                    ChangeAnimation(ANI_CORRER);
                    rb.velocity = new Vector2(-veloCorrer, rb.velocity.y);
                }
            }
            else
            {
                ChangeAnimation(ANI_CAMINAR);
                rb.velocity = new Vector2(-velocity, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANI_QUIETO);
        }
        if (Input.GetKeyDown(KeyCode.Space) && cont > 0)
        {
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(ANI_SALTO);
            cont--;
        }
    }
    void Gun()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            sr.flipX = false;
            dir = 1.2f;
            ChangeAnimation(ANI_CAMINAR);
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            sr.flipX = true;
            dir = -1.2f;
            ChangeAnimation(ANI_CAMINAR);
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        else if (Input.GetKeyDown("z"))
        {
            var fuegoPosition = transform.position + new Vector3(dir, -0.28f, 0);
            var qw = Instantiate(fuegoBala, fuegoPosition, Quaternion.identity);
            var balaPosition = transform.position + new Vector3(dir, -0.28f, 0);
            var gb = Instantiate(bala, balaPosition, Quaternion.identity);
            var controller = gb.GetComponent<BulletController>();
            if (dir == 1.2f) controller.SetRightDirection();
            else controller.SetLeftDirection();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            ChangeAnimation(ANI_QUIETO);
        }
        if (Input.GetKeyDown(KeyCode.Space) && cont > 0)
        {
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(ANI_SALTO);
            cont--;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        cont = salto;

        if (other.gameObject.tag == "Limites")
        {
            if (lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            if (lastCheckpointPosition != null)
            {
                transform.position = lastCheckpointPosition;
            }
            gameManager.PerderVida();
        }
        if (other.gameObject.tag == "Moneda")
        {
            gameManager.GanarPuntos(1);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Vida")
        {
            gameManager.GanarVida();
            Destroy(other.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)//para reconocer el checkponit(transparente)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            Debug.Log("CheckPoint");//aplicar la pocion isTrigger en la configuracion
            lastCheckpointPosition = transform.position;
            other.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void ChangeAnimation(int a)
    {
        animator.SetInteger("Estado", a);
    }
}
