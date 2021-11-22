using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Runtime2DMovement _2dMovement;
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _2dMovement = this.GetComponent<Runtime2DMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_2dMovement.getIsMovingLeft() && !_animator.GetBool("movingLeft"))
        {
            _animator.SetBool("movingLeft", true);
            _animator.SetBool("idle", false);
        }
        else if(!_2dMovement.getIsMovingLeft() && !_animator.GetBool("idle"))
        {
            _animator.SetBool("movingLeft", false);
            _animator.SetBool("idle", true);
        }
        else if (_2dMovement.getIsMovingRight())
        {
        }
    }
}
