using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Camera mainCam;
    public float speed = 0.1f;
    public bool m_moving;
    public GameObject m_farLeftBoundary;
    public GameObject m_farRightBoundary;

    public GameObject m_lastDoor;

    private TransitionPoint chosenPoint;
    private List<Behaviour> heldComponents;
    private ScreenTransition sT;

    [SerializeField]
    private GameObject player;

    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.FindWithTag("Player");
        sT = Camera.main.GetComponent<ScreenTransition>();
    }

    void Update()
    {
        // the m_moving bool is used to move during a transition
        // so while we're not transitioning, follow megaman
        if (m_farLeftBoundary != null && m_farRightBoundary != null)
        {
            if (!m_moving && GameObject.FindWithTag("Player").transform.position.x > m_farLeftBoundary.transform.position.x + 40
                && GameObject.FindWithTag("Player").transform.position.x < m_farRightBoundary.transform.position.x - 40)
            {
                mainCam.transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
            }
        }
        else if(!m_moving)
        {
            mainCam.transform.position = new Vector3(GameObject.FindWithTag("Player").transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
        }
    }

    public void StartMovement(int point)
    {
        chosenPoint = sT.transitionPoints[point];
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
            if (chosenPoint.type == TransitionTypes.HORIZONTAL)
            {
                if (mainCam.transform.position.x < chosenPoint.transitionPoint.x)
                {
                    while (mainCam.transform.position.x <= chosenPoint.transitionPoint.x)
                    {
                        mainCam.transform.position = new Vector3(mainCam.transform.position.x + speed, mainCam.transform.position.y, mainCam.transform.position.z);
                        player.transform.position = new Vector3(player.transform.position.x + speed, player.transform.position.y, player.transform.position.z);

                        yield return new WaitForSeconds(0.025f);
                    }
                }
                else if (mainCam.transform.position.x > chosenPoint.transitionPoint.x)
                {
                    while (mainCam.transform.position.x >= chosenPoint.transitionPoint.x)
                    {
                        mainCam.transform.position = new Vector3(mainCam.transform.position.x - speed, mainCam.transform.position.y, mainCam.transform.position.z);
                        player.transform.position = new Vector3(player.transform.position.x - speed, player.transform.position.y, player.transform.position.z);

                        yield return new WaitForSeconds(0.025f);
                    }
                }
            }
            if (chosenPoint.type == TransitionTypes.VERTICAL)
            {
                if (mainCam.transform.position.y < chosenPoint.transitionPoint.y)
                {
                    while (mainCam.transform.position.y <= chosenPoint.transitionPoint.y)
                    {
                        mainCam.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + speed, mainCam.transform.position.z);
                        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + speed, player.transform.position.z);

                        yield return new WaitForSeconds(0.025f);
                    }
                }
                else if (mainCam.transform.position.y > chosenPoint.transitionPoint.y)
                {
                    while (mainCam.transform.position.y >= chosenPoint.transitionPoint.y)
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
        player.GetComponent<PlayerController>().enabled = true;

        if(m_lastDoor != null)
            m_lastDoor.GetComponent<DoorHandler>().CloseDoor();
        

        m_moving = false;
        yield break;
    }
}
