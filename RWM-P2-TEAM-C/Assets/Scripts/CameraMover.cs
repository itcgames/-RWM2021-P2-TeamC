using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera mainCam;
    public List<Vector2> transitionPoints;
    public float speed = 0.1f;
    public bool m_moving;

    private Vector2 chosenPoint;
    private List<Behaviour> heldComponents;

    [SerializeField]
    private GameObject player;

    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // this bool is used to move during a transition
        // so while we're not transitioning, follow megaman
        if(!m_moving)
        {
            mainCam.transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
        }
    }

    public void AddPoint(Vector2 t_point)
    {
        transitionPoints.Add(t_point);
    }

    public void RemoveLastPoint()
    {
        if(transitionPoints.Count > 0) // make sure points exist first
            transitionPoints.RemoveAt(transitionPoints.Count - 1); // removes the last item in the list
    }

    public void StartMovement(int point, ScreenTransition.transitionTypes t_type)
    {
        chosenPoint = transitionPoints[point];
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
        if(!m_moving)
        { // only do this enumerator if it isn't already happening, to stop overlapping
            m_moving = true;
            if (GetComponent<ScreenTransition>().type == ScreenTransition.transitionTypes.HORIZONTAL)
            {
                if (mainCam.transform.position.x < chosenPoint.x)
                {
                    while (mainCam.transform.position.x <= chosenPoint.x)
                    {
                        mainCam.transform.position = new Vector3(mainCam.transform.position.x + speed, mainCam.transform.position.y, mainCam.transform.position.z);
                        player.transform.position = new Vector3(player.transform.position.x + speed, player.transform.position.y, player.transform.position.z);

                        yield return new WaitForSeconds(0.025f);
                    }
                }
                else if (mainCam.transform.position.x > chosenPoint.x)
                {
                    while (mainCam.transform.position.x >= chosenPoint.x)
                    {
                        mainCam.transform.position = new Vector3(mainCam.transform.position.x - speed, mainCam.transform.position.y, mainCam.transform.position.z);
                        player.transform.position = new Vector3(player.transform.position.x - speed, player.transform.position.y, player.transform.position.z);

                        yield return new WaitForSeconds(0.025f);
                    }
                }
            }
            if (GetComponent<ScreenTransition>().type == ScreenTransition.transitionTypes.VERTICAL)
            {
                if (mainCam.transform.position.y < chosenPoint.y)
                {
                    while (mainCam.transform.position.y <= chosenPoint.y)
                    {
                        mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + speed, mainCam.transform.position.z);
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + speed, player.transform.position.z);

                        yield return new WaitForSeconds(0.025f);
                    }
                }
                else if (mainCam.transform.position.y > chosenPoint.y)
                {
                    while (mainCam.transform.position.y >= chosenPoint.y)
                    {
                        mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y - speed, mainCam.transform.position.z);
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - speed, player.transform.position.z);

                        yield return new WaitForSeconds(0.025f);
                    }
                }
            }
        }

        // now that the mover is done, re-enable any disabled components

        foreach (Behaviour behaviour in GetComponents<Behaviour>())
        {
            if (!behaviour.enabled)
            {
                if (behaviour != mainCam)
                {
                    behaviour.enabled = true;
                }
            }
        }

        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        player.GetComponent<Animator>().enabled = true;
        m_moving = false;
        yield break;
    }
}
