using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gun
{
    Normal,
    SteamPunk
}

public class GunManager : MonoBehaviour
{
    private Gun _currentGun;

    void Start()
    {
        setUpNewGun(Gun.Normal);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            handleSwap();
        }
    }

    public void setUpNewGun(Gun gun)
    {
        _currentGun = gun;
        GameObject player = GameObject.Find("Player");
        BulletManager bulletManager = player.GetComponent<BulletManager>();
        switch(_currentGun)
        { 
            case Gun.Normal:
                bulletManager.MAX_BULLETS = 3;
                bulletManager.bulletMoveSpeed = 20.0f;
                bulletManager.bulletLifeTime = 2.0f;
                bulletManager.steamAmmoText.text = null;
                bulletManager.state = "Normal";
                break;
            case Gun.SteamPunk:
                bulletManager.MAX_BULLETS = 1;
                bulletManager.bulletMoveSpeed = 40.0f;
                bulletManager.bulletLifeTime = 1.0f;
                bulletManager.steamAmmoText.text = "STEAM AMMO: " + bulletManager.steamAmmo;
                bulletManager.state = "Steam";
                break;
        }
    }

    public Gun getCurrentGun()
    {
        return _currentGun;
    }

    public void handleSwap()
    {
        setUpNewGun((_currentGun == Gun.Normal) ? Gun.SteamPunk : Gun.Normal);
    }
}
