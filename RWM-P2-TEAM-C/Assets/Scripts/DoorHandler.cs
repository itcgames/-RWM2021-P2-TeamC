using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
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
                Animator anim = GetComponent<Animator>();
                anim.SetBool("opening", true);
                anim.SetBool("closing", false);
            }
        }
    }
}
