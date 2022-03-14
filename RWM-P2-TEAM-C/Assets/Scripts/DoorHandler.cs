using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField]
    public bool playerthrough = false;
    public void CloseDoor()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("closing", true);
        anim.SetBool( "opening", false);
    }

    // change animation on collision
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (!Camera.main.GetComponent<CameraMover>().m_moving)
            {
                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
             
                player.GetComponent<Animator>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false;
                Animator anim = GetComponent<Animator>();
                anim.SetBool("opening", true);
                anim.SetBool("closing", false);
                playerthrough = true;
            }
        }
    }
}
