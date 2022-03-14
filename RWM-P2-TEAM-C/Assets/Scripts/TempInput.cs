using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInput : MonoBehaviour
{
    public int transitionPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Camera.main.GetComponent<CameraMover>().StartMovement(transitionPoint);
        }
    }
}
