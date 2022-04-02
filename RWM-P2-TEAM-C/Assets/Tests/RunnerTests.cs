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
        // Runner Enemy Object
        GameObject Enemy;
        // Runner Animation Controller
        Animator EnemyAnimator;
        // player Object
        GameObject Player;
        // Bullet Object
        GameObject bullet;

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

        [UnityTest]
        public IEnumerator RunnerDamageTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Runner");
            float initialHealth = Enemy.GetComponent<Runner>().getHealth();
            bullet = GameObject.FindGameObjectWithTag("Bullet");
            bullet.transform.position = Enemy.transform.position;
            yield return new WaitForSeconds(0.1f);
            Assert.Less(Enemy.GetComponent<Runner>().getHealth(), initialHealth);
        }

        [UnityTest]
        public IEnumerator RunningAnimationTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Runner");
            EnemyAnimator = Enemy.GetComponent<Animator>();
            yield return new WaitForSeconds(0.2f);
            Assert.AreEqual(false, EnemyAnimator.GetBool("jumped"));
        }

        [UnityTest]
        public IEnumerator JumpAnimationTest()
        {
            Enemy = GameObject.FindGameObjectWithTag("Runner");
            EnemyAnimator = Enemy.GetComponent<Animator>();
            yield return new WaitForSeconds(0.6f);
            Assert.AreEqual(true, EnemyAnimator.GetBool("jumped"));
        }
    }
}
