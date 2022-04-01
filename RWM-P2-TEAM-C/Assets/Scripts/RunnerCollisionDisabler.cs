using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCollisionDisabler : MonoBehaviour
{
    void Start()
    {
        // for every runner that is a parent of this,
        // we will disable physics collisions between each of them
        foreach (Transform first in transform)
        {
            foreach(Transform second in transform)
            {
                if (first == second) continue; // if this is the same runner, ignore it

                Physics2D.IgnoreCollision(first.gameObject.GetComponents<BoxCollider2D>()[1], second.gameObject.GetComponents<BoxCollider2D>()[1], true);
            }
        }
    }
}
