using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    // Enemy's rigidbody
    Rigidbody2D rb;

    // Enemy Health
    public float maxHealth = 5.0f;
    // Enemy movement speed
    public float speed = 10.0f;
    // Current Health
    private float health;
    // Jump Timer
    public float jumpTimer = 0.5f;
    // Jump Height
    public float jumpHeight = 20.0f;
    // Bool to tell if the runner is airborne
    private bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!jumped)
        {
            jumpTimer -= Time.deltaTime;
        }
        if(jumpTimer <= 0.0f)
        {
            jumpTimer = 0.5f;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumped = true;
        }
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {
                col.gameObject.GetComponent<PlayerController>().decreseHealth(1, transform.position);
                if (col.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                {
                    AnalyticsManager.instance.data.killedBy = "Runner";
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumped = false;
        }
    }

    void OnBecameVisible()
    {
        this.enabled = true;
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    public void Damage(float damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            Destroy(this.gameObject);
        }
    }

    public float getHealth()
    {
        return health;
    }
}
