using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCBehaviour : MonoBehaviour
{
    Rigidbody2D rb2d;
    SpriteRenderer m_SpriteRenderer;
    public float mass;
    Vector3 moveDirection;
    float speed;
    private Sprite s;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3( Random.Range(-10,10), Random.Range(-10, 10), 0);
        moveDirection.Normalize();
       
        speed = Random.Range(10, 20);
        float r = Random.Range(50, 700);
        mass = r;

        transform.position = new Vector3(Random.Range(-400, 400), Random.Range(-400, 400), 0);
        rb2d.transform.localScale = new Vector3(mass / 50, mass / 50, mass / 50);

        rb2d.velocity = (moveDirection * speed);

        //change collider size to match new sprite size..
        Vector3 spriteHalfSize = this.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        this.GetComponent<CircleCollider2D>().radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        s = this.GetComponent<SpriteRenderer>().sprite;

        if(mass < 150)
        {
            m_SpriteRenderer.color = Color.cyan;
        }
        else if(mass >= 150 && mass < 300)
        {
            m_SpriteRenderer.color = Color.green;
        }
        else if (mass >= 300 && mass < 500)
        {
            m_SpriteRenderer.color = Color.yellow;
        }
        else if (mass >= 500 && mass < 700)
        {
            m_SpriteRenderer.color = Color.red;
        }
        this.GetComponent<CircleCollider2D>().isTrigger = true;
    }

    void Update()
    {
        if(transform.position.x > 495 || transform.position.x < -495 || transform.position.y > 495 || transform.position.y < -495 )
        {
            Debug.Log("False");
            this.GetComponent<CircleCollider2D>().isTrigger = false;
        }
        else
        {
            Debug.Log("true");
            this.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
    void FixedUpdate()
    {
        //rb2d.AddForce(moveDirection);
       // rb2d.velocity = (moveDirection * speed);
        transform.Rotate(new Vector3(0, 0, Random.Range(10,60)) * Time.deltaTime);
    }
}
