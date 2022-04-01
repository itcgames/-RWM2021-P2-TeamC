using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class RunnerTests
    {
        // Bomber Enemy Object
        GameObject Enemy;
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
            Enemy = GameObject.FindGameObjectWithTag("Runner");
            float initialPos = Enemy.GetComponent<Rigidbody2D>().position.x;
            yield return new WaitForSeconds(0.2f);
            Assert.Less(Enemy.GetComponent<Rigidbody2D>().position.x, initialPos);
        }

        [UnityTest]
        public IEnumerator JumpTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Runner");
            float initialPos = Enemy.GetComponent<Rigidbody2D>().position.y;
            yield return new WaitForSeconds(0.6f);
            Assert.Greater(Enemy.GetComponent<Rigidbody2D>().position.y, initialPos);
        }

        [UnityTest]
        public IEnumerator WallTurnTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Runner");
            bool initialDirection = Enemy.GetComponent<Runner>().left;
            Enemy.transform.localPosition = new Vector3(Enemy.transform.localPosition.x * -1, Enemy.transform.localPosition.y, Enemy.transform.localPosition.x);
            yield return new WaitForSeconds(0.1f);
            Assert.AreNotEqual(Enemy.GetComponent<Runner>().left, initialDirection);
            initialDirection = Enemy.GetComponent<Runner>().left;
            Enemy.transform.localPosition = new Vector3(Enemy.transform.localPosition.x * -1, Enemy.transform.localPosition.y, Enemy.transform.localPosition.x);
            yield return new WaitForSeconds(0.1f);
            Assert.AreNotEqual(Enemy.GetComponent<Runner>().left, initialDirection);
        }
    }
}
