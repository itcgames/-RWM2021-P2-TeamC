using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public int pointOne;
    public int pointTwo;
    public ScreenTransition.transitionTypes type;
    bool swap = false;

    public void BeginTransition()
    {
        if (!Camera.main.GetComponent<CameraMover>().m_moving)
        {
            if (!swap)
            {
                Camera.main.GetComponent<CameraMover>().StartMovement(pointOne, type);
            }
            else
            {
                Camera.main.GetComponent<CameraMover>().StartMovement(pointTwo, type);
            }

            Camera.main.GetComponent<CameraMover>().m_lastDoor = this.gameObject;
            swap = !swap;
        }
    }

    /*
    // begin transition on collision with player only
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if(!Camera.main.GetComponent<CameraMover>().m_moving)
            {
                if (!swap)
                {
                    Camera.main.GetComponent<CameraMover>().StartMovement(pointOne, type);
                }
                else
                {
                    Camera.main.GetComponent<CameraMover>().StartMovement(pointTwo, type);
                }

                swap = !swap;
            }   
        }
    }*/
}
