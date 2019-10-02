using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private int score;

    // public float mass;
    float enemyMass;
    public float speed;

    float massToScaleRatio; // -josh experiment..

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        massToScaleRatio = rb2d.transform.localScale.x / rb2d.mass;  // -josh experiment..
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("NPC");
        float closest = 1000000;
        int index = 0;
        for (int i = 0; i < gos.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, gos[i].transform.position);
            if (closest > dist)
            {
                index = i;
                closest = dist;
            }
        }
        // Mass to distance check here to enforce gravity
        enemyMass = gos[index].GetComponent<NPCBehaviour>().mass;
        Vector3 gravforce = (gos[index].GetComponent<NPCBehaviour>().transform.position - transform.position).normalized;

        float gravDist = Vector3.Distance(transform.position, gos[index].transform.position);
        if (gravDist < enemyMass*1.2)
         rb2d.AddForce(gravforce * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("NPC"))
        {
            rb2d.mass += collision.GetComponent<Rigidbody2D>().mass;  // add npc mass to player mass
            rb2d.transform.localScale += collision.transform.localScale; // add npc scale to player scale

            collision.gameObject.SetActive(false);

           // transform.localScale = Vector3(1.0f, 1.0f, 1.0f);
           //  mass += 10;

            score++;

        }
    }

}
