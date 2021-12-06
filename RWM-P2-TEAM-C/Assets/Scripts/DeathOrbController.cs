using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOrbController : MonoBehaviour
{
    public Vector3 moveDir;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // move the death orb
        transform.position += (moveDir * speed) * Time.deltaTime;
    }
}
