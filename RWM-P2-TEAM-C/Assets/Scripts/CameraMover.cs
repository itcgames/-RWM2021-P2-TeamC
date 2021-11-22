using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera mainCam;
    public Vector2 transitionPoint;

    void StartMovement()
    {
        transitionPoint = GetComponent<ScreenTransition>().transitionPoint;
        StartCoroutine(movement());

        mainCam = Camera.main;
    }

    IEnumerator movement()
    {
        while(mainCam.transform.position.x != transitionPoint.x
            && mainCam.transform.position.y != transitionPoint.y)
        {
            Debug.Log("moving camera");

            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("camera reached end");
        yield break;
    }
}
