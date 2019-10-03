using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector3 moveDirection;
    //public float mass;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3( Random.Range(-10,10), Random.Range(-10, 10), Random.Range(-10, 10));
        moveDirection.Normalize();
        //mass = Random.Range(10, 50);
        speed = Random.Range(1, 4);

        rb2d.mass = Random.Range(10, 100);

        rb2d.transform.localScale = new Vector3(rb2d.mass / 50, rb2d.mass / 50, rb2d.mass / 50);

        //change collider size to match new sprite size..
        this.GetComponent<CircleCollider2D>().radius = this.GetComponent<SpriteRenderer>().bounds.size.magnitude / 2;

    }

    
    void FixedUpdate()
    {
        //rb2d.AddForce(moveDirection);
        rb2d.velocity = (moveDirection * speed);
        transform.Rotate(new Vector3(0, 0, Random.Range(10,60)) * Time.deltaTime);
    }
}
