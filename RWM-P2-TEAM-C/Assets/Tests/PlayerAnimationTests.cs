using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class PlayerAnimationTests
    {
        private GameObject _player;
        private Animator _animator;
        private Runtime2DMovement _2dMovement;
        private PlayerController _playerController;

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("Game");
        }

        [UnityTest]
        public IEnumerator LeftAnimationTest()
        {
            setUpPlayer();
            _2dMovement.handleLeftInput();
            _2dMovement.moveLeft();
            _playerController.handleLeftAnimation();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("movingLeft"));
        }

        [UnityTest]
        public IEnumerator RightAnimationTest()
        {
            setUpPlayer();
            _2dMovement.handleRightInput();
            _2dMovement.moveRight();
            _playerController.handleRightAnimation();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("movingRight"));
        }

        [UnityTest]
        public IEnumerator IdleAnimationTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
        }

        [UnityTest]
        public IEnumerator JumpAnimationTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);

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
