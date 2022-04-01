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
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;//brings it to player
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {
                Debug.Log("Hit!");

                col.gameObject.GetComponent<PlayerController>().decreseHealth(3, transform.position);

                if (col.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                {
                    AnalyticsManager.instance.data.killedBy = "Boss";
                }
              
                Destroy(this.gameObject);
            }
        }
    }

}
