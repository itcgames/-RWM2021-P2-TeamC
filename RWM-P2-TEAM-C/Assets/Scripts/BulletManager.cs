using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletMoveSpeed;
    public float bulletLifeTime;
    public int MAX_BULLETS;
    public int currentBulletTotal = 0;
    private const int maxSteamAmmo = 5;
    public int steamAmmo = maxSteamAmmo;
    public Text steamAmmoText;
    public string state = "Normal";
    private GunManager _gunManager;

    private void Start()
    {
        _gunManager = this.GetComponent<GunManager>();
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
            int damage = (_gunManager.getCurrentGun() == Gun.SteamPunk) ? 4 : 1;
            if (state == "Normal")
            {
                bulletPrefab.GetComponent<Bullet>().bulletManager = this;
                bulletPrefab.GetComponent<Bullet>().speed = bulletMoveSpeed;
                bulletPrefab.GetComponent<Bullet>().lifetime = bulletLifeTime;
                bulletPrefab.GetComponent<Bullet>().damage = damage;
                Instantiate(bulletPrefab);
                SoundManagerScript.instance.PlaySound("buster");
                AnalyticsManager.instance.data.bulletsFired++;
                AnalyticsManager.instance.data.defaultBulletsShoot++;
            }
            if(state == "Steam" && steamAmmo > 0)
            {
                bulletPrefab.GetComponent<Bullet>().bulletManager = this;
                bulletPrefab.GetComponent<Bullet>().speed = bulletMoveSpeed;
                bulletPrefab.GetComponent<Bullet>().lifetime = bulletLifeTime;
                bulletPrefab.GetComponent<Bullet>().damage = damage;
                Instantiate(bulletPrefab);
                SoundManagerScript.instance.PlaySound("buster");
                AnalyticsManager.instance.data.bulletsFired++;
                steamAmmo--;
                steamAmmoText.text = "STEAM AMMO: " + steamAmmo;
                AnalyticsManager.instance.data.steamPunkBulletsShoot++;
            }
        }
    }

    public bool canFire()
    {
        return currentBulletTotal < MAX_BULLETS;
    }

    public int getMaxSteamAmmo()
    {
        return maxSteamAmmo;
    }
}
