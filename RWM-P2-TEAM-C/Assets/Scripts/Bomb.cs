using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Bomb's rigidbody
    Rigidbody2D rb;

    // Bomber's GameObject
    private GameObject bomber;
    // Temporary Shrapnel GameObject
    private GameObject shrapnel;
    // player gameobject
    private GameObject player;
    // Bomb Health
    public float maxHealth = 1.0f;
    // Current Health
    private float health;
    // Shell visible timer
    private float timer = 0;
    // Controls id the bomb is dropped;
    public bool dropped = false;
    // cracked egg sprite
    public Sprite cracked;
    // shrapnel object
    public GameObject ShrapnelPassed;

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
        timer = Time.deltaTime + 2.0f;
        if (col.gameObject.name == "Ground")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cracked;
            for (int i = 0; i < 3; i++)
            {
                shrapnel = Instantiate(ShrapnelPassed, new Vector3(rb.position.x, rb.position.y + i, 0), Quaternion.identity);
                if(rb.position.x < player.GetComponent<Rigidbody2D>().position.x)
                {
                    shrapnel.GetComponent<Rigidbody2D>().velocity = new Vector2(bomber.GetComponent<Bomber>().speed, rb.velocity.y);
                    shrapnel.GetComponent<Transform>().localScale = new Vector3(shrapnel.GetComponent<Transform>().localScale.x * -1, shrapnel.GetComponent<Transform>().localScale.y, shrapnel.GetComponent<Transform>().localScale.z);
                }
                else if(rb.position.x > player.GetComponent<Rigidbody2D>().position.x)
                {
                    shrapnel.GetComponent<Rigidbody2D>().velocity = new Vector2(-bomber.GetComponent<Bomber>().speed, rb.velocity.y);
                }
            }

            while (timer < 2.0f)
            {
                Destroy(this.gameObject);
                timer += Time.deltaTime;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Damage(float damage)
    {
        if(health > 0)
        {
            health -= damage;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float getHealth()
    {
        return health;
    }
}
