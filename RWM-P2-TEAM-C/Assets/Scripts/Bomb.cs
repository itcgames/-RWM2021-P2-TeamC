using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Bomb's rigidbody
    Rigidbody2D rb;

    // Bomber's GameObject
    private GameObject bomber;
    // Bomb Health
    public float maxHealth = 5.0f;
    // Current Health
    private float health;
    // Controls id the bomb is dropped;
    public bool dropped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
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
}
