using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    float moveSpeed = 20f;

    public Rigidbody2D rb;

    public PlayerController target;
    Vector2 moveDirection;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(0f, 0f, 180f);
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<PlayerController>();
        transform.position = new Vector3(transform.position.x - 1.25f, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.velocity = new Vector2(-transform.position.x, transform.position.y);
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {
                Debug.Log("Hit!");

                col.gameObject.GetComponent<PlayerController>().decreseHealth(3, transform.position);
                AnalyticsManager.instance.data.enemyDamage[3] += 3;

                if (col.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                {
                    AnalyticsManager.instance.data.killedBy = "Boss";
                }
              
                Destroy(this.gameObject);
            }
        }
    }

}
