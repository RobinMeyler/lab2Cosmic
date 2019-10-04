using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector3 moveDirection;
    //public float mass;
    float speed;
    private Sprite s;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3( Random.Range(-10,10), Random.Range(-10, 10), 0);
        moveDirection.Normalize();
        //mass = Random.Range(10, 50);
        speed = Random.Range(5, 10);

        rb2d.mass = Random.Range(50, 450);
        transform.position = new Vector3(Random.Range(-500, 500), Random.Range(-500, 500), 0);
        rb2d.transform.localScale = new Vector3(rb2d.mass / 50, rb2d.mass / 50, rb2d.mass / 50);

        //change collider size to match new sprite size..
        Vector3 spriteHalfSize = this.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        this.GetComponent<CircleCollider2D>().radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        s = this.GetComponent<SpriteRenderer>().sprite;

    }

    
    void FixedUpdate()
    {
        //rb2d.AddForce(moveDirection);
        rb2d.velocity = (moveDirection * speed);
        transform.Rotate(new Vector3(0, 0, Random.Range(10,60)) * Time.deltaTime);
    }
}
