using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFollwerCollisions : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {

                col.gameObject.GetComponent<PlayerController>().decreseHealth(1, transform.position);
                AnalyticsManager.instance.data.enemyDamage[1]++;

                if (col.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                {
                    AnalyticsManager.instance.data.killedBy = "Flying Follower";
                }
            }
        }
    }
}
