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
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
        rb.velocity = new Vector2(-this.GetComponentInParent<Bomber>().speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(dropped && rb.gravityScale != 1)
        {
            rb.gravityScale = 1;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        timer = Time.deltaTime + 2.0f;
        if (col.gameObject.tag == "Ground")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = cracked;
            for (int i = 0; i < 3; i++)
            {
                shrapnel = Instantiate(ShrapnelPassed, new Vector3(rb.position.x, rb.position.y + i, 0), Quaternion.identity);
                if(rb.position.x < player.GetComponent<Rigidbody2D>().position.x)
                {
                    shrapnel.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponentInParent<Bomber>().speed, 0.0f);
                    shrapnel.GetComponent<Transform>().localScale = new Vector3(shrapnel.GetComponent<Transform>().localScale.x * -1, shrapnel.GetComponent<Transform>().localScale.y, shrapnel.GetComponent<Transform>().localScale.z);
                }
                else if(rb.position.x > player.GetComponent<Rigidbody2D>().position.x)
                {
                    shrapnel.GetComponent<Rigidbody2D>().velocity = new Vector2(-this.GetComponentInParent<Bomber>().speed, 0.0f);
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
