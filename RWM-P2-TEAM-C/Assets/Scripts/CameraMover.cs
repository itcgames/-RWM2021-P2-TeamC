using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera mainCam;
    public Vector2 transitionPoint;
    public float speed = 0.1f;

    private bool m_moving;
    private List<Behaviour> heldComponents;

    void Start()
    {
        mainCam = Camera.main;
    }

    public void StartMovement()
    {
        
        transitionPoint = GetComponent<ScreenTransition>().transitionPoint;

        foreach (Behaviour behaviour in GetComponents<Behaviour>())
        {
            if(behaviour.enabled)
            {
                if(behaviour != mainCam)
                {
                    behaviour.enabled = false;
                }
            }
        }

        // re-enable these afterwards
        mainCam.enabled = true;
        GetComponent<AudioListener>().enabled = true;

        StartCoroutine(movement()); 
    }

    IEnumerator movement()
    {
        bool wentThroughMove = false;
        if(!m_moving)
        { // only do this enumerator if it isn't already happening, to stop overlapping
            m_moving = true;
            wentThroughMove = true;
            if (GetComponent<ScreenTransition>().type == ScreenTransition.transitionTypes.HORIZONTAL)
            {
                if (mainCam.transform.position.x < transitionPoint.x)
                {
                    while (mainCam.transform.position.x <= transitionPoint.x)
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
        }

        // now that the mover is done, re-enable any disabled components

        foreach (Behaviour behaviour in GetComponents<Behaviour>())
        {
            if (behaviour.enabled)
            {
                if (behaviour != mainCam)
                {
                    behaviour.enabled = true;
                }
            }
        }

        yield break;
    }
}
