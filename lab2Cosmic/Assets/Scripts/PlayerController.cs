using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private GameObject enemy;
    private int score;
    
    public float mass;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("NPC");
        float closest = 1000000;
        int index = 0;
        for(int i = 0; i < gos.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, gos[i].transform.position);
            if(closest > dist)
            {
                index = i;
                closest = dist;
            }
        }
        // Mass to distance check here to enforce gravity
       // gos[index].GetComponent<NPCBehaviour>.mass;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("NPC"))
        {
            collision.gameObject.SetActive(false);

           // transform.localScale = Vector3(1.0f, 1.0f, 1.0f);
            mass += 10;

            score++;
        }
    }

}
