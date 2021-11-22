using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera mainCam;
    public Vector2 transitionPoint;
    public float speed = 0.1f;

    public void StartMovement()
    {
        mainCam = Camera.main;
        transitionPoint = GetComponent<ScreenTransition>().transitionPoint;
        StartCoroutine(movement()); 
    }

    IEnumerator movement()
    {
        if(GetComponent<ScreenTransition>().type == ScreenTransition.transitionTypes.HORIZONTAL)
        {
            if (mainCam.transform.position.x < transitionPoint.x)
            {
                while(mainCam.transform.position.x <= transitionPoint.x)
                {
                    mainCam.transform.position = new Vector3(mainCam.transform.position.x + speed, mainCam.transform.position.y, mainCam.transform.position.z);

                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (mainCam.transform.position.x > transitionPoint.x)
            {
                while (mainCam.transform.position.x >= transitionPoint.x)
                {
                    mainCam.transform.position = new Vector3(mainCam.transform.position.x - speed, mainCam.transform.position.y, mainCam.transform.position.z);

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        if (GetComponent<ScreenTransition>().type == ScreenTransition.transitionTypes.HORIZONTAL)
        {
            if (mainCam.transform.position.y < transitionPoint.y)
            {
                while (mainCam.transform.position.y <= transitionPoint.y)
                {
                    mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + speed, mainCam.transform.position.z);

                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (mainCam.transform.position.y > transitionPoint.y)
            {
                while (mainCam.transform.position.y >= transitionPoint.y)
                {
                    mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - speed, mainCam.transform.position.z);

                    yield return new WaitForSeconds(0.1f);
                }
            }
        }


        Debug.Log("camera reached end");
        yield break;
    }
}
