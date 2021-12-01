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
        public IEnumerator PlayerIsGroundedOnBackgroundLevelTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
        }

        [UnityTest]
        public IEnumerator PlayerCanWalkIntoWallTest()
        {
            setUpPlayer();
            _2dMovement.handleRightInput();
            _2dMovement.moveRight();
            yield return new WaitForSeconds(5.0f);
            Assert.AreEqual(true, _animator.GetBool("movingRight"));
        }

        [UnityTest]
        public IEnumerator PlayerCanWalkOffLedgeTest()
        {
            setUpPlayer();
            _rb.position = new Vector2(-53.42f, -4.36f);
            _2dMovement.handleLeftInput();
            _2dMovement.moveLeft();
            yield return new WaitForSeconds(0.6f);
            Assert.AreEqual(false, _animator.GetBool("grounded"));
        }

        [UnityTest]
        public IEnumerator PlayerCanInJumpOntoLedgeTest()
        {
            setUpPlayer();
            float intialYPos = _rb.position.y;
            _2dMovement.impluseJumpVel = 30.0f;
            _2dMovement.handleRightInput();
            _2dMovement.moveRight();
            yield return new WaitForSeconds(3.5f);
            _2dMovement.intialJump();
            yield return new WaitForSeconds(1.0f);
            Assert.Greater(_rb.position.y, intialYPos);
            Assert.AreEqual(true, _animator.GetBool("grounded"));
            Assert.AreEqual(false, _animator.GetBool("walkingRight"));
        }

        [UnityTest]
        public IEnumerator PlayerCannotWalkOffLevelTest()
        {
            setUpPlayer();
            _2dMovement.handleLeftInput();
            _2dMovement.moveLeft();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(false, _animator.GetBool("walkingLeft"));
        }

        private void setUpPlayer()
        {
            _player = GameObject.Find("Player");
            _animator = _player.GetComponent<Animator>();
            _2dMovement = _player.GetComponent<Runtime2DMovement>();
            _playerController = _player.GetComponent<PlayerController>();
            _rb = _player.GetComponent<Rigidbody2D>();
        }
    }
}
