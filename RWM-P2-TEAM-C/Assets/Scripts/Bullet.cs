using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletManager bulletManager;
    public float bulletDirection;
    public float speed;
    public float lifetime;

    // Update is called once per frame

    void Start()
    {
        StartCoroutine("livingTime");

        if (null == bulletManager) { bulletManager = GameObject.Find("Player").GetComponent<BulletManager>(); }

        transform.position = GameObject.Find("Player").transform.position;
        bulletDirection = GameObject.Find("Player").GetComponent<PlayerController>().direction;
        bulletManager.increaseBullets();
    }

    void Update()
    {
        transform.position += new Vector3((bulletDirection * speed) * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D t_other)
    {
        if (t_other.tag != "Player")
        {
            if (t_other.gameObject.tag == "Bomb") { Destroy(t_other.gameObject); }
            else if (t_other.gameObject.tag == "Shrapnel") { Destroy(t_other.gameObject); }
            else if (t_other.gameObject.tag == "Bomber") { t_other.gameObject.GetComponent<Bomber>().Damage(1); }
            else if (t_other.gameObject.tag == "Follower" && !t_other.gameObject.GetComponent<FlyingFollower>().invincible) { t_other.gameObject.GetComponent<FlyingFollower>().damage(1); }
            else if (t_other.gameObject.tag == "Follower" && t_other.gameObject.GetComponent<FlyingFollower>().invincible) { SoundManagerScript.PlaySound("dink"); }
            bulletManager.decreaseBullets(); // decrease total number of bullets
            StopCoroutine("livingTime"); // stop the co-routine before destroying
            Destroy(gameObject);
        }
    }


    IEnumerator livingTime()
    {
        yield return new WaitForSeconds(lifetime);

        bulletManager.decreaseBullets(); // decrease total number of bullets
        Destroy(gameObject); // destroy this after a set amount of time

        yield break;
    }
}
