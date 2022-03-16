using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDummyScript : MonoBehaviour
{
    public bool _invincible;
    public float _damagedFlashRate = 0.15f;
    public float _invincibleTimer = 1.0f;
    public void beginDamagedState()
    {
        if(!_invincible)
        {
            Debug.Log("hit dummy");
            _invincible = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
            StartCoroutine(invincibilityTime());
        }
        
    }
    public bool getIsInvincible()
    {
        return _invincible;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {
                col.gameObject.GetComponent<PlayerController>().decreseHealth(0, transform.position);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!col.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {
                col.gameObject.GetComponent<PlayerController>().decreseHealth(0, transform.position);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D t_other)
    {
        if (t_other.gameObject.tag == "Player")
        {
            if (!t_other.gameObject.GetComponent<PlayerController>().getIsInvincible())
            {
                t_other.gameObject.GetComponent<PlayerController>().decreseHealth(0, transform.position);
            }
        }
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

    IEnumerator invincibilityTime()
    {
        StartCoroutine(invincibilityFlash());

        yield return new WaitForSeconds(_invincibleTimer);

        _invincible = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
