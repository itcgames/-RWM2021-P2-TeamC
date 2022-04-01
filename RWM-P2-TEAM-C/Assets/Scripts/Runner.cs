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
    // Bool to control direction the enemy is facing
    public bool left = true;
    // Child Transform used to check what's in front of the Runner
    private Transform wallDetection;
    private RaycastHit2D wallInfo;
    // Distance from the wall that the enemy turns
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        this.enabled = false;
        wallDetection = this.transform.GetChild(0);
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
        if (left)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        // Right Wall Detection
        wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, distance);
        if (wallInfo.collider != null && (wallInfo.collider.tag == "Ledge" || wallInfo.collider.tag == "Boundary" || wallInfo.collider.name == "Door"))
        {
            if (!left)
            {
                Debug.Log("Hit Right Wall");
                transform.eulerAngles = new Vector3(0, 0, 0);
                left = true;
            }
        }

        // Left Wall Detection
        wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.left, distance);
        if (wallInfo.collider != null && (wallInfo.collider.tag == "Ledge" || wallInfo.collider.tag == "Boundary" || wallInfo.collider.name == "Door"))
        {
            if (left)
            {
                Debug.Log("Hit Left Wall");
                transform.eulerAngles = new Vector3(0, -180, 0);
                left = false;
            }
        }

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
