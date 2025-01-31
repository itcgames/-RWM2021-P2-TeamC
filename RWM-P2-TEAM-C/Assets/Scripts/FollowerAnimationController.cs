﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerAnimationController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject.GetComponent<FlyingFollower>().invincible)
        {
            animator.SetBool("unfurl", true);
        }
    }
}
