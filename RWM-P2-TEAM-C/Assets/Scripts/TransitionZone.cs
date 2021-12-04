using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public int transitionPoint;
    public ScreenTransition.transitionTypes type;

    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            Camera.main.GetComponent<CameraMover>().StartMovement(transitionPoint, type);
        }
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Camera.main.GetComponent<CameraMover>().StartMovement(transitionPoint, type);
        }
    }
}
