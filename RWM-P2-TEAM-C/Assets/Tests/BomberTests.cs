﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class BomberTests
    {
        // Bomber Enemy Object
        GameObject Enemy;
        // Enemy Bomb Object
        GameObject bomb;
        // Bomb Shrapnel Object
        GameObject shrapnel;
        // player Object
        GameObject Player;

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("AITestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void TearDown()
        {
            SceneManager.UnloadSceneAsync("AITestScene");
        }

        [UnityTest]
        public IEnumerator MovementTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            float initialPos = Enemy.GetComponent<Rigidbody2D>().position.x;
            yield return new WaitForSeconds(0.5f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos);
        }

        [UnityTest]
        public IEnumerator BombSpawnTest()
        {
            bomb = GameObject.FindGameObjectWithTag("Bomb");
            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(bomb);
        }

        [UnityTest]
        public IEnumerator BombCarryingTest()
        {
            bomb = GameObject.FindGameObjectWithTag("Bomb");
            float initialPos = bomb.GetComponent<Rigidbody2D>().position.x;
            yield return new WaitForSeconds(0.5f);
            Assert.Less(bomb.GetComponent<Rigidbody2D>().position.x, initialPos);
        }

        [UnityTest]
        public IEnumerator BombDropTest()
        {
            bomb = GameObject.FindGameObjectWithTag("Bomb");
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.transform.position = new Vector3(bomb.transform.position.x - 3, bomb.transform.position.y - 1, Player.transform.position.z);
            float initialPos = bomb.GetComponent<Rigidbody2D>().position.x;
            yield return new WaitForSeconds(0.1f);
            Assert.Less(bomb.GetComponent<Rigidbody2D>().position.y, initialPos);
        }

        [UnityTest]
        public IEnumerator BombExplosionTest()
        {
            bomb = GameObject.FindGameObjectWithTag("Bomb");
            bomb.GetComponent<Bomb>().dropped = true;
            yield return new WaitForSeconds(1.0f);
            shrapnel = GameObject.FindGameObjectWithTag("Shrapnel");
            Assert.IsNotNull(shrapnel);
        }

        [UnityTest]
        public IEnumerator BombDamageTest()
        {
            bomb = GameObject.FindGameObjectWithTag("Bomb");
            float initialHealth = bomb.GetComponent<Bomb>().getHealth();
            bomb.GetComponent<Bomb>().Damage(0.5f);
            yield return new WaitForSeconds(0.1f);
            Assert.Less(bomb.GetComponent<Bomb>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator BomberDamageTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Bomber");
            float initialHealth = Enemy.GetComponent<Bomber>().getHealth();
            Enemy.GetComponent<Bomber>().Damage(0.5f);
            yield return new WaitForSeconds(0.1f);
            Assert.Less(Enemy.GetComponent<Bomber>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator EggCrackTest()
        {
            bomb = GameObject.FindGameObjectWithTag("Bomb");
            bomb.GetComponent<Bomb>().dropped = true;
            yield return new WaitForSeconds(0.94f);
            Assert.AreEqual("pipi copipi spritesheet_2", bomb.GetComponent<SpriteRenderer>().sprite.name);
        }
    }
}
