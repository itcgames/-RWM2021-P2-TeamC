using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Bomb's rigidbody
    Rigidbody2D rb;

    // Bomber's GameObject
    private GameObject bomber;
    // Shrapnel GameObject
    private GameObject shrapnel;
    // player gameobject
    private GameObject player;
    // Bomb Health
    public float maxHealth = 1.0f;
    // Current Health
    private float health;
    // Controls id the bomb is dropped;
    public bool dropped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        health = maxHealth;
        bomber = GameObject.Find("Bomber");
        rb.velocity = new Vector2(-bomber.GetComponent<Bomber>().speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(dropped && rb.gravityScale != 1)
        {
            rb.gravityScale = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Ground")
        {
            for (int i = 0; i < 3; i++)
            {
                shrapnel = Instantiate(GameObject.Find("Shrapnel"), new Vector3(rb.position.x, rb.position.y + i, 0), Quaternion.identity);
                if(rb.position.x < player.GetComponent<Rigidbody2D>().position.x)
                {
                    shrapnel.GetComponent<Rigidbody2D>().velocity = new Vector2(bomber.GetComponent<Bomber>().speed, rb.velocity.y);
                }
                else if(rb.position.x > player.GetComponent<Rigidbody2D>().position.x)
                {
                    shrapnel.GetComponent<Rigidbody2D>().velocity = new Vector2(-bomber.GetComponent<Bomber>().speed, rb.velocity.y);
                }
            }
        }
        Destroy(this.gameObject);
    }
}
