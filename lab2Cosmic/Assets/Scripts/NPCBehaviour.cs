using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float mass;
    Vector3 moveDirection;
    float speed;
    private Sprite s;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3( Random.Range(-10,10), Random.Range(-10, 10), 0);
        moveDirection.Normalize();
        //mass = Random.Range(10, 50);
        speed = Random.Range(2, 6);

        mass = Random.Range(50, 650);
        transform.position = new Vector3(Random.Range(-300, 300), Random.Range(-300, 300), 0);
        rb2d.transform.localScale = new Vector3(mass / 50, mass / 50, mass / 50);

        rb2d.velocity = (moveDirection * speed);

        //change collider size to match new sprite size..
        Vector3 spriteHalfSize = this.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        this.GetComponent<CircleCollider2D>().radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        s = this.GetComponent<SpriteRenderer>().sprite;

    }

    
    void FixedUpdate()
    {
        //rb2d.AddForce(moveDirection);
       // rb2d.velocity = (moveDirection * speed);
        transform.Rotate(new Vector3(0, 0, Random.Range(10,60)) * Time.deltaTime);
    }
}
