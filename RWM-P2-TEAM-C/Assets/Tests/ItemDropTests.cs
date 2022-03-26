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
        GameObject bomb;
        GameObject bullet;
        GameObject healthDrop;
        private GameObject _player;

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

        private void setUpPlayer()
        {
            _player = GameObject.Find("Player");
        }
    }
}
