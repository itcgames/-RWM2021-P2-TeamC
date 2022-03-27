using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class EnemyPlayerCollisionTests
    {
        private GameObject _player;
        private Animator _animator;
        private Runtime2DMovement _2dMovement;
        private PlayerController _playerController;

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("PlayerEnemyHealthTest", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("PlayerEnemyHealthTest");
        }

        [UnityTest]
        public IEnumerator playerCollideWithShrapnelTest()
        {
            setUpPlayer();
            int intialHealth = _player.GetComponent<PlayerController>().getHealth();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
            yield return new WaitForSeconds(3.0f);
            Assert.Less(_player.GetComponent<PlayerController>().getHealth(), intialHealth);
            Assert.AreEqual(true, _player.GetComponent<PlayerController>().getIsInvincible());
        }

        [UnityTest]
        public IEnumerator playerCollideWithBattonTest()
        {
            setUpPlayer();
            _player.GetComponent<Rigidbody2D>().position = new Vector2(_player.GetComponent<Rigidbody2D>().position.x + 15.0f, _player.GetComponent<Rigidbody2D>().position.y);
            int intialHealth = _player.GetComponent<PlayerController>().getHealth();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
            yield return new WaitForSeconds(1.5f);
            Assert.Less(_player.GetComponent<PlayerController>().getHealth(), intialHealth);
            Assert.AreEqual(true, _player.GetComponent<PlayerController>().getIsInvincible());
        }

        [UnityTest]
        public IEnumerator playerHasDiedTest()
        {
            setUpPlayer();
            _player.GetComponent<PlayerController>().decreseHealth(20, new Vector2(0,0));
            _player.GetComponent<PlayerController>().setUpDeadAnimation();
            yield return new WaitForSeconds(2.0f);
            Assert.AreEqual(true, _player.GetComponent<OnDeath>().getIsDead());
        }

        private void setUpPlayer()
        {
            _player = GameObject.Find("Player");
            _animator = _player.GetComponent<Animator>();
            _2dMovement = _player.GetComponent<Runtime2DMovement>();
            _playerController = _player.GetComponent<PlayerController>();
        }
    }
}
