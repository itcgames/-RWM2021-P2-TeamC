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
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
        }

        [UnityTest]
        public IEnumerator PlayerCanWalkOffLedgeTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
        }

        [UnityTest]
        public IEnumerator PlayerCanInJumpOntoLedgeTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _animator.GetBool("idle"));
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
