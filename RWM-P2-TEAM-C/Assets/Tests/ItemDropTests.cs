using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ItemDropTests
    {
        GameObject Enemy;
        GameObject bullet;
        GameObject healthDrop;
        GameObject ammoDrop;
        private GameObject player;

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("AITestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("AITestScene");
        }

        /// <summary>
        /// Ammo Drop Tests
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator FollowerHealthDropTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Follower");
            Enemy.GetComponent<FlyingFollower>().damage(4);
            Enemy.GetComponent<ItemDrop>().alwaysHealth = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.5f);
            healthDrop = GameObject.FindGameObjectWithTag("HealthDrop");
            Assert.IsNotNull(healthDrop);
        }

        [UnityTest]
        public IEnumerator BomberHealthDropTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            Enemy.GetComponent<Bomber>().Damage(4);
            Enemy.GetComponent<ItemDrop>().alwaysHealth = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.5f);
            healthDrop = GameObject.FindGameObjectWithTag("HealthDrop");
            Assert.IsNotNull(healthDrop);
        }

        [UnityTest]
        public IEnumerator BombHealthDropTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomb");
            Enemy.GetComponent<ItemDrop>().alwaysHealth = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.5f);
            healthDrop = GameObject.FindGameObjectWithTag("HealthDrop");
            Assert.IsNotNull(healthDrop);
        }

        [UnityTest]
        public IEnumerator HealingOnPickUp()
        {
            setUpPlayer();
            Enemy = GameObject.FindGameObjectWithTag("Follower");
            Enemy.GetComponent<FlyingFollower>().damage(4);
            Enemy.GetComponent<ItemDrop>().alwaysHealth = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.1f);
            healthDrop = GameObject.FindGameObjectWithTag("HealthDrop");
            player.GetComponent<PlayerController>().decreseHealth(1, new Vector2(0, 0));
            int initialHealth = player.GetComponent<PlayerController>().getHealth();
            healthDrop.transform.position = player.transform.position;
            yield return new WaitForSeconds(0.1f);
            Assert.Greater(player.GetComponent<PlayerController>().getHealth(), initialHealth);
        }

        /// <summary>
        /// Ammo Drop Tests
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator FollowerAmmoDropTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Follower");
            Enemy.GetComponent<FlyingFollower>().damage(4);
            Enemy.GetComponent<ItemDrop>().alwaysAmmo = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.5f);
            ammoDrop = GameObject.FindGameObjectWithTag("AmmoDrop");
            Assert.IsNotNull(ammoDrop);
        }

        [UnityTest]
        public IEnumerator BomberAmmoDropTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            Enemy.GetComponent<Bomber>().Damage(4);
            Enemy.GetComponent<ItemDrop>().alwaysAmmo = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.5f);
            ammoDrop = GameObject.FindGameObjectWithTag("AmmoDrop");
            Assert.IsNotNull(ammoDrop);
        }

        [UnityTest]
        public IEnumerator BombAmmoDropTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomb");
            Enemy.GetComponent<ItemDrop>().alwaysAmmo = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.5f);
            ammoDrop = GameObject.FindGameObjectWithTag("AmmoDrop");
            Assert.IsNotNull(ammoDrop);
        }

        [UnityTest]
        public IEnumerator RecoverAmmoOnPickUp()
        {
            setUpPlayer();
            Enemy = GameObject.FindGameObjectWithTag("Bomb");
            Enemy.GetComponent<ItemDrop>().alwaysAmmo = true;
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.1f);
            ammoDrop = GameObject.FindGameObjectWithTag("AmmoDrop");
            player.GetComponent<BulletManager>().steamAmmo--;
            int initialAmmo = player.GetComponent<BulletManager>().steamAmmo;
            ammoDrop.transform.position = player.transform.position;
            yield return new WaitForSeconds(0.1f);
            Assert.Greater(player.GetComponent<BulletManager>().steamAmmo, initialAmmo);
        }

        private void setUpPlayer()
        {
            player = GameObject.Find("Player");
        }
    }
}
