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

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.mass = 50;
        rb2d.transform.localScale = new Vector3(rb2d.mass / 50, rb2d.mass / 50, rb2d.mass / 50);
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        // rb2d.AddForce(movement * speed);

        // fast movement, JUST 4 TESTIN!
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb2d.transform.Translate(new Vector2(0, -0.1f));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb2d.transform.Translate(new Vector2(0, 0.1f));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.transform.Translate(new Vector2(-0.1f, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.transform.Translate(new Vector2(0.1f, 0));
        }


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
        //enemyMass = gos[index].GetComponent<NPCBehaviour>().mass;
        enemyMass = gos[index].GetComponent<Rigidbody2D>().mass; // change by josh
        Vector3 gravforce = (gos[index].GetComponent<NPCBehaviour>().transform.position - transform.position).normalized;

        float gravDist = Vector3.Distance(transform.position, gos[index].transform.position);
        if (gravDist < enemyMass*1.2)
         rb2d.AddForce(gravforce * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("NPC"))
        {
            if (collision.GetComponent<Rigidbody2D>().mass < rb2d.mass)
            {
                rb2d.mass += collision.GetComponent<Rigidbody2D>().mass;  // add npc mass to player mass
                rb2d.transform.localScale += collision.transform.localScale; // add npc scale to player scale

                collision.gameObject.SetActive(false);
                ScoreTextBehaviour.scoreCount += 1;
            }

            // transform.localScale = Vector3(1.0f, 1.0f, 1.0f);
            //  mass += 10;

            score++;

        }
    }

}
