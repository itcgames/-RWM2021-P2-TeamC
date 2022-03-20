//using System.Collections;
//using System.Collections.Generic;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;
//using UnityEngine.SceneManagement;

//namespace Tests
//{
//    public class PlayerAnimationTests
//    {
//        private GameObject _player;
//        private Animator _animator;
//        private Runtime2DMovement _2dMovement;
//        private PlayerController _playerController;

//        [SetUp]
//        public void Setup()
//        {
//            SceneManager.LoadScene("PlayerTestScene", LoadSceneMode.Single);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            SceneManager.UnloadSceneAsync("PlayerTestScene");
//        }

//        [UnityTest]
//        public IEnumerator LeftAnimationTest()
//        {
//            setUpPlayer();
//            _2dMovement.handleLeftInput();
//            _2dMovement.moveLeft();
//            _playerController.handleLeftAnimation();
//            yield return new WaitForSeconds(1.0f);
//            Assert.AreEqual(true, _animator.GetBool("movingLeft"));
//        }

//        [UnityTest]
//        public IEnumerator RightAnimationTest()
//        {
//            setUpPlayer();
//            _2dMovement.handleRightInput();
//            _2dMovement.moveRight();
//            _playerController.handleRightAnimation();
//            yield return new WaitForSeconds(1.0f);
//            Assert.AreEqual(true, _animator.GetBool("movingRight"));
//        }

//        [UnityTest]
//        public IEnumerator IdleAnimationTest()
//        {
//            setUpPlayer();
//            yield return new WaitForSeconds(1.0f);
//            Assert.AreEqual(true, _animator.GetBool("idle"));
//        }

//        [UnityTest]
//        public IEnumerator JumpAnimationTestWhileIdle()
//        {
//            setUpPlayer();
//            yield return new WaitForSeconds(1.0f);
//            _2dMovement.handleJumpInput();
//            _playerController.handleJumpAnimationWhileIdle();
//            yield return new WaitForSeconds(0.1f);
//            Assert.AreEqual(false, _animator.GetBool("grounded"));
//            yield return new WaitForSeconds(0.5f);
//            Assert.AreEqual(true, _animator.GetBool("grounded"));
//            Assert.AreEqual(true, _animator.GetBool("idle"));
//        }

//        [UnityTest]
//        public IEnumerator JumpAnimationTestWhileWalking()
//        {
//            setUpPlayer();
//            yield return new WaitForSeconds(1.0f);
//            _2dMovement.moveRight();
//            _playerController.handleRightAnimation();
//            Assert.AreEqual(true, _animator.GetBool("movingRight"));
//            yield return new WaitForSeconds(0.1f);
//            _2dMovement.handleJumpInput();
//            _playerController.handleJumpAnimationWhileIdle();
//            yield return new WaitForSeconds(0.1f);
//            Assert.AreEqual(false, _animator.GetBool("grounded"));
//            yield return new WaitForSeconds(1.0f);
//            Assert.AreEqual(true, _animator.GetBool("grounded"));
//            Assert.AreEqual(true, _animator.GetBool("idle"));
//        }

//        private void setUpPlayer()
//        {
//            _player = GameObject.Find("Player");
//            _animator = _player.GetComponent<Animator>();
//            _2dMovement = _player.GetComponent<Runtime2DMovement>();
//            _playerController = _player.GetComponent<PlayerController>();
//        }
//    }
//}
