using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gun
{
    Normal,
    SteamPunk
}

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletMoveSpeed;
    public float bulletLifeTime;
    private int MAX_BULLETS;
    public float currentBulletTotal;
    Gun currentGun = Gun.Normal;

    private void Start()
    {
        setUpGun(Gun.Normal);
    }

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

            SoundManagerScript.instance.PlaySound("buster");
            AnalyticsManager.instance.data.bulletsFired++;
        }
    }

    public bool canFire()
    {
        return currentBulletTotal < MAX_BULLETS;
    }

    public void setUpGun(Gun gunType)
    {
        if(gunType == Gun.Normal)
        {
            MAX_BULLETS = 3;
            bulletLifeTime = 1.0f;
            bulletMoveSpeed = 2.0f;
        }
    }
}
