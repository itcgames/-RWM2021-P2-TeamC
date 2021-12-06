using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class FollowerAnimationTests
    {
        private GameObject Player;
        private GameObject Enemy;
        private Animator EnemyAnimator;
        private FlyingFollower Follower;
        private FollowerAnimationController FollowerController;

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("AITestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("AITestScene");
        }

        private void setUpEnemy()
        {
            Enemy = GameObject.FindGameObjectWithTag("Follower");
            EnemyAnimator = Enemy.GetComponent<Animator>();
            Follower = Enemy.GetComponent<FlyingFollower>();
            FollowerController = Enemy.GetComponent<FollowerAnimationController>();
        }

        [UnityTest]
        public IEnumerator FurledTest()
        {
            setUpEnemy();
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(false, EnemyAnimator.GetBool("Unfurl"));
        }

        [UnityTest]
        public IEnumerator UnfurledTest()
        {
            setUpEnemy();
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x - 2, Enemy.GetComponent<Rigidbody2D>().position.y - 2);
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual("Unfurl", EnemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        }

        [UnityTest]
        public IEnumerator FlyingTest()
        {
            setUpEnemy();
            Player = GameObject.FindGameObjectWithTag("Player");
            Player.GetComponent<Rigidbody2D>().position = new Vector2(Enemy.GetComponent<Rigidbody2D>().position.x - 2, Enemy.GetComponent<Rigidbody2D>().position.y - 2);
            yield return new WaitForSeconds(1.5f);
            Assert.AreEqual("Flying", EnemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        }
    }
}
