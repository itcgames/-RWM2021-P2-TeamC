using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : MonoBehaviour
{
    // player gameobject
    private GameObject player;

    // Shrapnel Rigidbody
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {
                col.gameObject.GetComponent<PlayerController>().decreseHealth(1, transform.position);
                if (col.gameObject.GetComponent<PlayerController>().getHealth() <= 0)
                {
                    AnalyticsManager.instance.data.killedBy = "Shrapnel";
                }
                Destroy(this.gameObject);
            }
        }
    }
}
