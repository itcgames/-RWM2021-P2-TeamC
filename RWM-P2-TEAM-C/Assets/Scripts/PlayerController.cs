using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rb;
    private Runtime2DMovement _2dMovement;
    private bool _isShooting = false;
    private bool idleshoot;
    public float _timeBetweenShots;
    public int direction = -1;
    private BulletManager _bulletManager;
    private const int _MAX_HEALTH = 15;
    public int _health = _MAX_HEALTH;
    private bool _invincible = false;
    public float _hurtTimer = 0.25f;
    public float _invincibleTimer = 2.0f;
    public float _damagedFlashRate = 0.25f;
    public Text megaManHealthText;

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
        _bulletManager = gameObject.GetComponent<BulletManager>();
        megaManHealthText.text = "MEGAMAN HEALTH " + _health;
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayerAnimationStates();
        setUpDeadAnimation();
    }
       
    void updatePlayerAnimationStates()
    {
        if (!_invincible) 
        { 
            getShootInput(); 
        }
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

        Vector3 temp = transform.localScale;
        if (temp.x < 0) { direction =  1; }
        if (temp.x > 0) { direction = -1;  }
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
        if (Input.GetKeyDown(KeyCode.P) && _bulletManager.canFire())
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
        _bulletManager.shootBullet();
        _animator.SetBool("isShooting", _isShooting);
        StartCoroutine("shootingCooldown");
    }

    public void handleMovingPlayerShooting()
    {
        _isShooting = true;
        _bulletManager.shootBullet();
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

    public void decreseHealth(int healthReduction)
    {
        if (!_invincible)
        {
            if (_health - healthReduction <= 0)
            {
                _health = 0;
                megaManHealthText.text = "MEGAMAN HEALTH " + _health;
            }
            else
            {
                _health -= healthReduction;
                megaManHealthText.text = "MEGAMAN HEALTH " + _health;
                _invincible = true;
                StartCoroutine(damagedStateTime());
            }
        }

    }

    IEnumerator invincibilityTime()
    {
        StartCoroutine(invincibilityFlash());

        yield return new WaitForSeconds(_invincibleTimer);

        _invincible = false;
    }

    IEnumerator damagedStateTime()
    {
        yield return new WaitForSeconds(_hurtTimer);

        StartCoroutine(invincibilityTime());
    }

    IEnumerator invincibilityFlash()
    {
        while (_invincible)
        {
            GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(_damagedFlashRate);

            GetComponent<SpriteRenderer>().enabled = true;

            yield return new WaitForSeconds(_damagedFlashRate);
        }
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public bool getIsInvincible()
    {
        return _invincible;
    }

    public int getHealth()
    {
        return _health;
    }
    
    public void setUpDeadAnimation()
    {
        if (_health <= 0)
        {
            gameObject.GetComponent<OnDeath>().hasDied();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Runtime2DMovement>().enabled = false;
            gameObject.GetComponent<BulletManager>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
            this.enabled = false;
        }
    }
}
