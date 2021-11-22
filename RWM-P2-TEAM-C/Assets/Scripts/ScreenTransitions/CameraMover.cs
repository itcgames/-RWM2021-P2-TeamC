using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Vector2 transitionPoint;

    void StartMovement()
    {
        transitionPoint = this.gameObject.GetComponent<ScreenTransition>().transitionPoint;
    }

    IEnumerator movement()
    {

    }
}
