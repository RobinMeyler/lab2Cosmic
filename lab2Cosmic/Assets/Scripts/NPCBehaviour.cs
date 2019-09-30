using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector3 moveDirection;
    public float mass;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3( Random.Range(-10,10), Random.Range(-10, 10), Random.Range(-10, 10));
        moveDirection.Normalize();
        mass = Random.Range(10, 50);
    }

    
    void FixedUpdate()
    {
        rb2d.AddForce(moveDirection);
        transform.Rotate(new Vector3(0, 0, Random.Range(10,60)) * Time.deltaTime);
    }
}
