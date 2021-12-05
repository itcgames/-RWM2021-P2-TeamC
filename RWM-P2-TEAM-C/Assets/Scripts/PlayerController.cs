﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private Runtime2DMovement _2dMovement;
    private bool _isShooting = false;
    public float _timeBetweenShots;
    bool idleshoot;
    void Start()
    {
        setUpPlayer();
    }

    void setUpPlayer()
    {
        _animator = this.GetComponent<Animator>();
        _2dMovement = this.GetComponent<Runtime2DMovement>();
        _rb = this.GetComponent<Rigidbody2D>();
        _rb = this.GetComponent<Rigidbody2D>();
        if (!_rb)
        {
            this.gameObject.AddComponent<Rigidbody2D>();
            _rb = this.GetComponent<Rigidbody2D>();
            _rb.angularDrag = 0;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rb.gravityScale = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerAnimationStates();
    }

    void updatePlayerAnimationStates()
    {
        if (_2dMovement.getIsMovingLeft() && _2dMovement.getIsGrounded() && !_animator.GetBool("movingLeft"))
        {
            handleLeftAnimation();
        }
        else if (_2dMovement.getIsMovingRight() && _2dMovement.getIsGrounded() && !_animator.GetBool("movingRight"))
        {
            handleRightAnimation();
        }
        else if (!_2dMovement.getIsGrounded() && _animator.GetBool("idle"))
        {
            handleJumpAnimationWhileIdle();
        }
        else if ((_2dMovement.getIsMovingRight() || _2dMovement.getIsMovingLeft()) && !_2dMovement.getIsGrounded() && _animator.GetBool("grounded"))
        {
            handleJunpAnimationWhileWalking();
        }
        else if ((_2dMovement.getIsMovingRight() || _2dMovement.getIsMovingLeft()) && !_animator.GetBool("grounded"))
        {
            handleDirectionWhileJumping();
        }
        else if ((_rb.velocity.SqrMagnitude() <= 0 && !_animator.GetBool("idle")))
        {
            handleIdleAnimation();
        }
        getShootInput();
    }

    public void handleLeftAnimation()
    {
        Vector3 temp = transform.localScale;
        if (temp.x < 0) { temp.x *= -1; }
        transform.localScale = temp;
        _animator.SetBool("movingLeft", true);
        _animator.SetBool("movingRight", false);
        _animator.SetBool("grounded", true);
        _animator.SetBool("idle", false);
    }

    public void handleRightAnimation()
    {
        Vector3 temp = transform.localScale;
        if (temp.x > 0) { temp.x *= -1; }
        transform.localScale = temp;
        _animator.SetBool("movingRight", true);
        _animator.SetBool("movingLeft", false);
        _animator.SetBool("grounded", true);
        _animator.SetBool("idle", false);
    }

    public void handleJumpAnimationWhileIdle()
    {
        _animator.SetBool("grounded", false);
        _animator.SetBool("idle", false);
    }

    public void handleIdleAnimation()
    {
        _animator.SetBool("movingRight", false);
        _animator.SetBool("movingLeft", false);
        _animator.SetBool("grounded", true);
        _animator.SetBool("idle", true);
    }

    public void handleJunpAnimationWhileWalking()
    {
        _animator.SetBool("movingLeft", false);
        _animator.SetBool("movingRight", false);
        _animator.SetBool("grounded", false);
        _animator.SetBool("idle", false);
    }

    public void handleDirectionWhileJumping()
    {
        if (_2dMovement.getIsMovingLeft())
        {
            Vector3 temp = transform.localScale;
            if (temp.x < 0) { temp.x *= -1; }
            transform.localScale = temp;
        }
        else if (_2dMovement.getIsMovingRight())
        {
            Vector3 temp = transform.localScale;
            if (temp.x > 0) { temp.x *= -1; }
            transform.localScale = temp;
        }
    }

    void getShootInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (_rb.velocity.SqrMagnitude() <= 0 && _animator.GetBool("grounded"))
            {
                handleIdlePlayerShooting();
            }
            else if (_rb.velocity.SqrMagnitude() > 0 && !idleshoot)
            {
                handleMovingPlayerShooting();
            }
        }
    }

    public void handleIdlePlayerShooting()
    {
        _isShooting = true;
        idleshoot = true;
        _2dMovement.setStopMovement(true);
        _animator.SetBool("isShooting", _isShooting);
        StartCoroutine("shootingCooldown");
    }

    public void handleMovingPlayerShooting()
    {
        _isShooting = true;
        _animator.SetBool("isShooting", _isShooting);
        StartCoroutine("shootingCooldown");
    }


    IEnumerator shootingCooldown()
    {
        yield return new WaitForSeconds(_timeBetweenShots);
        _isShooting = false;
        idleshoot = false;
        _2dMovement.setStopMovement(false);
        yield return new WaitForSeconds(_timeBetweenShots);
        _animator.SetBool("isShooting", _isShooting);
    }
}
