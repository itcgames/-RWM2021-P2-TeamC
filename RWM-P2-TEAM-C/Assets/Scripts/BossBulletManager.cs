using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletMoveSpeed;
    public float bulletLifeTime;
    private const float MAX_BULLETS = 6;
    public float currentBulletTotal;

    /// <summary>
    /// Decrease the number of active bullets
    /// </summary>
    public void decreaseBullets()
    {
        if (currentBulletTotal <= 0)
        {
            currentBulletTotal = 0;
        }
        else
        {
            currentBulletTotal--;
        }
    }

    public void increaseBullets()
    {
        currentBulletTotal++;
    }

    /// <summary>
    /// Shoot a bullet in the direction the  Boss is looking.
    /// -1 for Left, 1 for Right.
    /// </summary>
    public void shootBullet()
    {
        if (currentBulletTotal < MAX_BULLETS)
        {
            Instantiate(bulletPrefab);
            //bulletPrefab.GetComponent<BossBullet>().bulletManager = this;
        //    bulletPrefab.GetComponent<BossBullet>().speed = bulletMoveSpeed;
           // bulletPrefab.GetComponent<BossBullet>().lifetime = bulletLifeTime;
            SoundManagerScript.instance.PlaySound("buster");
            
        }
    }

    public bool canFire()
    {
        return currentBulletTotal < MAX_BULLETS;
    }
}
