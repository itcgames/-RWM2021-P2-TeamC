using System.Collections;
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
        // player Object
        GameObject Player;

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        [TearDown]
        public void TearDown()
        {
            SceneManager.UnloadSceneAsync("Game");
        }

        [UnityTest]
        public IEnumerator MovementTest()
        {
            Enemy = GameObject.Find("Bomber");
            float initialPos = Enemy.GetComponent<Rigidbody2D>().position.x;
            yield return new WaitForSeconds(0.5f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos);
        }

        [UnityTest]
        public IEnumerator BombSpawnTest()
        {
            bomb = GameObject.Find("Bomb");
            yield return new WaitForSeconds(0.5f);
            Assert.IsNotNull(bomb);
        }

        [UnityTest]
        public IEnumerator BombCarryingTest()
        {
            bomb = GameObject.Find("Bomb");
            float initialPos = bomb.GetComponent<Rigidbody2D>().position.x;
            yield return new WaitForSeconds(0.5f);
            Assert.Less(bomb.GetComponent<Rigidbody2D>().position.x, initialPos);
        }
    }
}
