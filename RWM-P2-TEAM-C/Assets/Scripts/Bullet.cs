﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletManager bulletManager;
    public float bulletDirection;
    public float speed;
    public float lifetime;
    public int damage;
    private GameObject player;
    

    // Update is called once per frame

    void Start()
    {
        StartCoroutine("livingTime");

        if (null == bulletManager) { bulletManager = GameObject.Find("Player").GetComponent<BulletManager>(); }
        player = GameObject.Find("Player");
        if(player.GetComponent<PlayerController>().direction == -1)
        {
            transform.position = new Vector3(player.transform.position.x - 1.25f, player.transform.position.y - 0.8f, player.transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x + 1.25f, player.transform.position.y - 0.8f, player.transform.position.z);
        }
        bulletDirection = GameObject.Find("Player").GetComponent<PlayerController>().direction;
        bulletManager.increaseBullets();
    }

    void Update()
    {
        transform.position += new Vector3((bulletDirection * speed) * Time.deltaTime, 0);
    }

     void OnTriggerEnter2D(Collider2D t_other)
    {
        bool collSuccess = false;
        if (t_other.tag != "Player")
        {
            if (t_other.gameObject.tag == "Bomb") 
            { 
                t_other.gameObject.GetComponent<ItemDrop>().drop(); 
                Destroy(t_other.gameObject); 
                SoundManagerScript.instance.PlaySound("bhit"); 
                collSuccess = true;
                AnalyticsManager.instance.data.defeatedEnemies++;
            }
            else if(t_other.gameObject.tag == "bossbullet") 
            { 
                Destroy(t_other.gameObject); 
                SoundManagerScript.instance.PlaySound("bhit"); 
                collSuccess = true;
                AnalyticsManager.instance.data.defeatedEnemies++;
            }
            else if (t_other.gameObject.tag == "Shrapnel") 
            { 
                Destroy(t_other.gameObject); 
                SoundManagerScript.instance.PlaySound("bhit"); 
                collSuccess = true;
                AnalyticsManager.instance.data.defeatedEnemies++;
            }
            else if (t_other.gameObject.tag == "Bomber") 
            { 
                t_other.gameObject.GetComponent<Bomber>().Damage(damage);
                if (t_other.gameObject.GetComponent<Bomber>().getHealth() <= 0){AnalyticsManager.instance.data.defeatedEnemies++;}
                t_other.gameObject.GetComponent<ItemDrop>().drop(); 
                SoundManagerScript.instance.PlaySound("bhit"); 
                collSuccess = true; 
            }
            else if (t_other.gameObject.tag == "Runner")
            {
                t_other.gameObject.GetComponent<Runner>().Damage(damage);
                if (t_other.gameObject.GetComponent<Runner>().getHealth() <= 0) { AnalyticsManager.instance.data.defeatedEnemies++; }
                t_other.gameObject.GetComponent<ItemDrop>().drop();
                SoundManagerScript.instance.PlaySound("bhit");
                collSuccess = true;
            }
            else if (t_other.gameObject.tag == "Follower" && !t_other.gameObject.GetComponent<FlyingFollower>().invincible) 
            { 
                t_other.gameObject.GetComponent<FlyingFollower>().damage(damage);
                if (t_other.gameObject.GetComponent<FlyingFollower>().getHealth() <= 0) { AnalyticsManager.instance.data.defeatedEnemies++; }
                t_other.gameObject.GetComponent<ItemDrop>().drop(); 
                SoundManagerScript.instance.PlaySound("bhit"); 
                collSuccess = true; 
            }
            else if (t_other.gameObject.tag == "Follower" && t_other.gameObject.GetComponent<FlyingFollower>().invincible) 
            { 
                SoundManagerScript.instance.PlaySound("dink"); 
                collSuccess = true; 
            }
            else if (t_other.gameObject.tag == "Boss") 
            { 
                t_other.gameObject.GetComponent<Boss>().damage(damage);
                if (t_other.gameObject.GetComponent<Boss>().getHealth() <= 0) { AnalyticsManager.instance.data.defeatedEnemies++; }
                t_other.gameObject.GetComponent<ItemDrop>().drop(); 
                SoundManagerScript.instance.PlaySound("bhit"); 
                collSuccess = true; 
            }
            else if (t_other.gameObject.tag == "dummy") {
                if(!t_other.GetComponent<TutorialDummyScript>()._invincible)
                {
                    t_other.gameObject.GetComponent<TutorialDummyScript>().beginDamagedState();
                    SoundManagerScript.instance.PlaySound("dummy");
                    collSuccess = true;
                }
            }
            else if(t_other.gameObject.tag == "Ledge")
            {
                SoundManagerScript.instance.PlaySound("dink");
                collSuccess = true;
            }

            if(collSuccess)
            {
                bulletManager.decreaseBullets(); // decrease total number of bullets
                StopCoroutine("livingTime"); // stop the co-routine before destroying
                Destroy(gameObject);
            }
            
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
