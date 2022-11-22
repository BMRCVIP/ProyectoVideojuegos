using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolpeScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float velocity = 0;
    public void SetRightDirection()
    {
        velocity = 10;
    }
    public void SetLeftDirection()
    {
        velocity = -10;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
