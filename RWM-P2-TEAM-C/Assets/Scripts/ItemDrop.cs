using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public bool alwaysHealth;   //If true, health always drops
    public GameObject healthDrop;
    public bool alwaysAmmo;   //If true, Ammo always drops
    public GameObject AmmoDrop;
    public int dropChance = 5;  // Higher numbers mean less of a chance of an item dropping


    private Rigidbody2D rb;
    private int droppedItem;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void drop()
    {
        droppedItem = Random.Range(0, dropChance);
        if (this.GetComponent<FlyingFollower>() != null && GetComponent<FlyingFollower>().getHealth() <= 0)
        {
            if(alwaysHealth)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (alwaysAmmo)
            {
                Instantiate(AmmoDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if(droppedItem == 1)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (droppedItem == 2)
            {
                Instantiate(AmmoDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
        }
        else if (this.GetComponent<Bomber>() != null && GetComponent<Bomber>().getHealth() <= 0)
        {
            if (alwaysHealth)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (alwaysAmmo)
            {
                Instantiate(AmmoDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (droppedItem == 1)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (droppedItem == 2)
            {
                Instantiate(AmmoDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
        }
        else if (this.GetComponent<Bomb>() != null)
        {
            if (alwaysHealth)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (alwaysAmmo)
            {
                Instantiate(AmmoDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (droppedItem == 1)
            {
                Instantiate(healthDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
            else if (droppedItem == 2)
            {
                Instantiate(AmmoDrop, new Vector3(rb.position.x, rb.position.y, 0), Quaternion.identity);
            }
        }
    }
}
