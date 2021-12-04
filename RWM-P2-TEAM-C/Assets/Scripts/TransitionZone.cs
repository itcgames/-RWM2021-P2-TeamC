using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public int transitionPoint;
    public ScreenTransition.transitionTypes type;

    // begin transition on collision with player only
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Camera.main.GetComponent<CameraMover>().StartMovement(transitionPoint, type);
        }
    }
}
