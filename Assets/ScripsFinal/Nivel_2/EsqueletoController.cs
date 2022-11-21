using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoController : MonoBehaviour
{
    private float velocity = -3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    private int ani = 1;
    private float time = 1.5f;
    private float cont = 0.0f;

    const int ANI_QUIETO = 0;
    const int ANI_CAMINAR = 1;
    const int ANI_ATAQUE = 2;
    const int ANI_MUERTO = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ani == 1)
        {
            Debug.Log("Caminando");
            ChangeAnimation(ANI_CAMINAR);
            rb.velocity = new Vector2(velocity, rb.velocity.y);//hace que el zombie camine
        }
        else if (ani == 2)
        {
            ChangeAnimation(ANI_ATAQUE);
            rb.velocity = new Vector2(0, rb.velocity.y);//hace que el zombie camine
            Debug.Log("Atacando");
            cont += Time.deltaTime;
            if (cont >= time)
            {
                ani = 1;
                velocity *= -1;
                if (velocity > 0)
                    sr.flipX = true;
                else
                    sr.flipX = false;
            }   
        }
        else if (ani == 3)
        {
            ChangeAnimation(ANI_MUERTO);
            cont += Time.deltaTime;
            if (cont >= time){
                this.GetComponent<Collider2D>().enabled = false;
                this.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Barrera")
        {
            cont = 0.0f;
            ani = 2;
        }
        if (other.gameObject.tag == "Bullet")
        {
            cont = 0.0f;
            ani = 3;
        }
    }
    private void ChangeAnimation(int a)
    {
        animator.SetInteger("Estado", a);
    }
}
