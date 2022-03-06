using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletMoveSpeed;
    public float bulletLifeTime;
    private const float MAX_BULLETS = 3;
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
    /// Shoot a bullet in the direction Megaman is looking.
    /// -1 for Left, 1 for Right.
    /// </summary>
    public void shootBullet()
    {
        if (currentBulletTotal < MAX_BULLETS)
        {
            Instantiate(bulletPrefab);
            bulletPrefab.GetComponent<Bullet>().bulletManager = this;
            bulletPrefab.GetComponent<Bullet>().speed = bulletMoveSpeed;
            bulletPrefab.GetComponent<Bullet>().lifetime = bulletLifeTime;

            AnalyticsSingleton.instance.manager.data.bulletsFired++;
        }
    }

    public bool canFire()
    {
        return currentBulletTotal < MAX_BULLETS;
    }
}
