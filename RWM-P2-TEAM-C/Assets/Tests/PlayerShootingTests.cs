using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class PlayerShootingTests
    {
        private GameObject _player;
        private Animator _animator;
        private Runtime2DMovement _2dMovement;
        private PlayerController _playerController;

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("PlayerTestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("PlayerTestScene");
        }

        [UnityTest]
        public IEnumerator playerIdlePlayerShootingTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
            _playerController.handleIdlePlayerShooting();
            yield return new WaitForSeconds(0.01f);
            Assert.AreEqual(true, _animator.GetBool("isShooting"));
            Assert.AreEqual(true, (_player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 0));
        }

        [UnityTest]
        public IEnumerator playerWalkingRightShootingTest()
        {
            setUpPlayer();
            _2dMovement.handleRightInput();
            _2dMovement.moveRight();
            _playerController.handleRightAnimation();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("movingRight"));
            _playerController.handleMovingPlayerShooting();
            Assert.AreEqual(true, _animator.GetBool("isShooting"));
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(true, (_player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0));
        }

        [UnityTest]
        public IEnumerator playerWalkingLeftShootingTest()
        {
            setUpPlayer();
            _2dMovement.handleLeftInput();
            _2dMovement.moveLeft();
            _playerController.handleLeftAnimation();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("movingLeft"));
            _playerController.handleMovingPlayerShooting();
            Assert.AreEqual(true, _animator.GetBool("isShooting"));
            yield return new WaitForSeconds(0.01f);
            Assert.AreEqual(true, (_player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0));
        }

        [UnityTest]
        public IEnumerator playerJumpingtShootingTest()
        {
            setUpPlayer();
            _2dMovement.handleJumpInput();
            _playerController.handleJumpAnimationWhileIdle();
            yield return new WaitForSeconds(0.05f);
            Assert.AreEqual(false, _animator.GetBool("grounded"));
            _playerController.handleMovingPlayerShooting();
            Assert.AreEqual(true, _animator.GetBool("isShooting"));
            yield return new WaitForSeconds(0.05f);
            Assert.AreEqual(true, (_player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0));
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
