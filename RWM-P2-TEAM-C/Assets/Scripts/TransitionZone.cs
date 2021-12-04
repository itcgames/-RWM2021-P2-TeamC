﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public int pointOne;
    public int pointTwo;
    public ScreenTransition.transitionTypes type;
    bool swap = false;

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
                    swap = true;
                }
                else
                {
                    Camera.main.GetComponent<CameraMover>().StartMovement(pointTwo, type);
                    swap = false;
                }
            }   
        }
    }
}
