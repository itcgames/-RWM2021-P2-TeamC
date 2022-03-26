using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    //If true, health always drops
    public bool alwaysHealth;
    public GameObject healthDrop;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void drop()
    {
        if(this.GetComponent<FlyingFollower>() != null && GetComponent<FlyingFollower>().getHealth() <= 0)
        {
            if(alwaysHealth)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if(Random.Range(0, 2) == 1)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
        }
        else if (this.GetComponent<Bomber>() != null && GetComponent<Bomber>().getHealth() <= 0)
        {
            if (alwaysHealth)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (Random.Range(0, 2) == 1)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
        }
        else if (this.GetComponent<Bomb>() != null)
        {
            if (alwaysHealth)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (Random.Range(0, 2) == 1)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
        }
    }
}
