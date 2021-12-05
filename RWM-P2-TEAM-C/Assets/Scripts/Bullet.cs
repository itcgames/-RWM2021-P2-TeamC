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
