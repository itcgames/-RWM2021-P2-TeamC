﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    // Enemy's rigidbody
    Rigidbody2D rb;

    // Enemy Health
    public float maxHealth = 5.0f;
    // Enemy movement speed
    public float speed = 5.0f;
    // Player GameObject
    public GameObject player;
    // Bomb GameObject
    public GameObject bomb;
    // Current Health
    private float health;

    // Bombing Range
    public float range = 5;

    private bool armed = true;

    // Passed in Bomb Object
    public GameObject bombPassed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        health = maxHealth;
        // Movement
        bomb = Instantiate(bombPassed, new Vector3(rb.position.x, rb.position.y - 1.5f, 0), Quaternion.identity);
        bomb.transform.parent = this.transform;
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (armed)
        {
            if(player && bomb)
            {
                if (Vector2.Distance(rb.position, new Vector2(player.transform.position.x, player.transform.position.y)) < range && !bomb.GetComponent<Bomb>().dropped)
                {
                    bomb.GetComponent<Bomb>().dropped = true;
                    armed = false;
                }
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
                AnalyticsManager.instance.data.enemyDamage[0]++;

                if (col.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                {
                    AnalyticsManager.instance.data.killedBy = "Bomber";
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
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void OnBecameInvisible()
    {
#if UNITY_EDITOR
        if(Camera.current)
            if (Camera.current.name == "SceneCamera")
                return;
#endif
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
