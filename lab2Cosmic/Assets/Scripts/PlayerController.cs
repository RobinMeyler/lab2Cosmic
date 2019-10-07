using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Text lost;
    public int score;
    private Sprite s;
    float mass;
    float enemyMass;
    public float speed;
    private int NPCcount;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mass = 150;
        NPCcount = 0;
        rb2d.transform.localScale = new Vector3(mass / 50, mass / 50, 1);
        this.GetComponent<CircleCollider2D>().radius = this.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        Vector3 spriteHalfSize = this.GetComponent<SpriteRenderer>().sprite.bounds.extents;
        this.GetComponent<CircleCollider2D>().radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        s = this.GetComponent<SpriteRenderer>().sprite;
        lost.text = "";
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
        float gravPower = ((enemyMass / gravDist) * 1.5f);

        if (gravDist < enemyMass * 1.5)
        {
            rb2d.AddForce(gravforce * gravPower);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            if (collision.GetComponent<NPCBehaviour>().mass < mass)
            {
                mass += collision.GetComponent<NPCBehaviour>().mass;  // add npc mass to player mass
                rb2d.transform.localScale += collision.transform.localScale; // add npc scale to player scale
                Vector3 spriteHalfSize = this.GetComponent<SpriteRenderer>().sprite.bounds.extents;
                this.GetComponent<CircleCollider2D>().radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
                s = this.GetComponent<SpriteRenderer>().sprite;
                collision.gameObject.SetActive(false);
                ScoreTextBehaviour.scoreCount += (int)collision.GetComponent<NPCBehaviour>().mass;
                NPCcount++;
            }
            else
            {
                gameObject.SetActive(false);
                lost.text = "Lost!";
            }
            // transform.localScale = Vector3(1.0f, 1.0f, 1.0f);
            //  mass += 10;
           
            score += (int)collision.GetComponent<NPCBehaviour>().mass;

            if(NPCcount == 15)
            {
                lost.text = "Win!";
            }
        }
    }

}
