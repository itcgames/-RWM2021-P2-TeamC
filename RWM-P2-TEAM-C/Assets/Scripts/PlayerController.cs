using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private Runtime2DMovement _2dMovement;
    void Start()
    {
        _animator =   this.GetComponent<Animator>();
        _2dMovement = this.GetComponent<Runtime2DMovement>();
        _rb =         this.GetComponent<Rigidbody2D>();
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
        if (_2dMovement.getIsMovingLeft() && !_animator.GetBool("movingLeft"))
        {
            handleLeftAnimation();
        }
        else if (_2dMovement.getIsMovingRight())
        {
            handleRightAnimation();
        }
        else if(_rb.velocity.SqrMagnitude() <= 0 && !_animator.GetBool("idle"))
        {
            handleIdleAnimation();
        }
    }
    public void handleLeftAnimation()
    {
        if (_2dMovement.getIsMovingLeft() && !_animator.GetBool("movingLeft"))
        {
            Vector3 temp = transform.localScale;
            if (temp.x < 0) { temp.x *= -1; }
            transform.localScale = temp;
            _animator.SetBool("movingLeft", true);
            _animator.SetBool("movingRight", false);
            _animator.SetBool("idle", false);
        }
    }

    public void handleRightAnimation()
    {
        if (_2dMovement.getIsMovingRight())
        {
            Vector3 temp = transform.localScale;
            if (temp.x > 0) { temp.x *= -1; }
            transform.localScale = temp;
            _animator.SetBool("movingRight", true);
            _animator.SetBool("movingLeft", false);
            _animator.SetBool("idle", false);
        }
    }

    public void handleIdleAnimation()
    {
        if (_rb.velocity.SqrMagnitude() <= 0 && !_animator.GetBool("idle"))
        {
            _animator.SetBool("movingRight", false);
            _animator.SetBool("movingLeft", false);
            _animator.SetBool("idle", true);
        }
    }
}
