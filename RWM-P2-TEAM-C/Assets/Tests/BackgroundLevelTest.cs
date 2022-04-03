using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
namespace Tests
{
    public class BackgroundLevelTest
    {
        private GameObject _player;
        private Animator _animator;
        private Runtime2DMovement _2dMovement;
        private PlayerController _playerController;
        private Rigidbody2D _rb;
        private MovingStateMachine _movingStateMachine;
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("PlayerTestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("PlayerTestScene");
            GameObject.Destroy(_player);
        }

        [UnityTest]
        public IEnumerator PlayerIsGroundedOnBackgroundLevelTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
        }

        [UnityTest]
        public IEnumerator PlayerCanWalkRightTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(0.5f);
            _movingStateMachine.ChangeState(_movingStateMachine.movementRight);
            _movingStateMachine.movementRight.moveRight();
            _playerController.handleRightAnimation();
            yield return new WaitForSeconds(0.01f);
            Assert.AreEqual(true, _animator.GetBool("movingRight"));
        }

        [UnityTest]
        public IEnumerator PlayerCanWalkLeftTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(0.5f);
            _movingStateMachine.ChangeState(_movingStateMachine.movementLeft);
            _movingStateMachine.movementLeft.moveLeft();
            _playerController.handleLeftAnimation();
            yield return new WaitForSeconds(0.01f);
            Assert.AreEqual(true, _animator.GetBool("movingLeft"));
        }

        [UnityTest]
        public IEnumerator PlayerCanInJumpTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(0.5f);
            _movingStateMachine.setInitalState(_movingStateMachine.jumping);
            _movingStateMachine.jumping.handleJumpInput();
            _playerController.handleJumpAnimation();
            Assert.AreEqual(false, _animator.GetBool("grounded"));
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(true, _animator.GetBool("grounded"));
        }

        private void setUpPlayer()
        {
            _player = GameObject.Find("Player");
            _animator = _player.GetComponent<Animator>();
            _2dMovement = _player.GetComponent<Runtime2DMovement>();
            _playerController = _player.GetComponent<PlayerController>();
            _rb = _player.GetComponent<Rigidbody2D>();
            _movingStateMachine = _player.GetComponent<MovingStateMachine>();
        }
    }
}
