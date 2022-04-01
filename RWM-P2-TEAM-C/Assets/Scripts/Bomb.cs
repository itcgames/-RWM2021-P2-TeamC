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
    private bool _timerStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_timerStarted)
        {
            if (dropped && rb.gravityScale != 2.0f)
            {
                rb.gravityScale = 2.0f;
            }
        }

        if (_timerStarted)
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                _timerStarted = false;
                Destroy(this.gameObject);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            timer = Time.deltaTime + 0.5f;
            _timerStarted = true;
            rb.velocity = new Vector2(0, 0);
            rb.gravityScale = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = cracked;
            for (int i = 0; i < 3; i++)
            {
                shrapnel = Instantiate(ShrapnelPassed, new Vector3(rb.position.x, (rb.position.y + 1) + i, 0), Quaternion.identity);
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
        }
        else
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite != cracked)
            {
                if (col.gameObject.tag == "Player")
                {
                    if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
                    {
                        col.gameObject.GetComponent<PlayerController>().decreseHealth(3, transform.position);
                        if (col.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                        {
                            AnalyticsManager.instance.data.killedBy = "Bomb";
                        }
                        Destroy(this.gameObject);
                    }
                }
                else if (col.gameObject.tag == "Bullet")
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    void OnBecameVisible()
    {
#if UNITY_EDITOR
        if (Camera.current)
            if (Camera.current.name == "SceneCamera")
                return;
#endif

        this.enabled = true;
        rb.velocity = new Vector2(-this.GetComponentInParent<Bomber>().speed, rb.velocity.y);
    }

    void OnBecameInvisible()
    {
#if UNITY_EDITOR
        if (Camera.current)
            if (Camera.current.name == "SceneCamera")
                return;
#endif

        this.gameObject.SetActive(false);
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
