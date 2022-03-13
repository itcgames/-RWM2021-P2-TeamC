using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class GunTests
    {
        private GameObject _player;
        private Animator _animator;
        private Runtime2DMovement _2dMovement;
        private PlayerController _playerController;
        private GunManager _gunManager;

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
        public IEnumerator defaultGunTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(Gun.Normal, _gunManager.getCurrentGun());
        }

        [UnityTest]
        public IEnumerator swapGunTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(Gun.Normal, _gunManager.getCurrentGun());
            _gunManager.handleSwap();
            Assert.AreEqual(Gun.SteamPunk, _gunManager.getCurrentGun());
        }

        [UnityTest]
        public IEnumerator playerShootingWithDefaultGunTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            _playerController.handleIdlePlayerShooting();
            yield return new WaitForSeconds(0.1f);
            GameObject bullet = GameObject.Find("Bullet(Clone)");
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            Assert.AreEqual(20.0f, bulletScript.speed); // Speed of the default bullet.
        }

        [UnityTest]
        public IEnumerator playerShootingWithSteamPunkGunTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            _gunManager.handleSwap();
            yield return new WaitForSeconds(0.5f);
            _playerController.handleIdlePlayerShooting();
            yield return new WaitForSeconds(0.2f);
            GameObject bullet = GameObject.Find("Bullet(Clone)");
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            Assert.AreEqual(40.0f, bulletScript.speed); // Speed of the steam bullet.
        }

        [UnityTest]
        public IEnumerator DefaultBulletLifetimeTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            _playerController.handleIdlePlayerShooting();
            GameObject bullet = GameObject.Find("Bullet(Clone)");
            Assert.NotNull(bullet);
            yield return new WaitForSeconds(2.0f);
            bullet = GameObject.Find("Bullet(Clone)");
            Assert.Null(bullet);
        }

        [UnityTest]
        public IEnumerator SteamPunkBulletLifetimeTest()
        {
            setUpPlayer();
            yield return new WaitForSeconds(1.0f);
            _gunManager.handleSwap();
            _playerController.handleIdlePlayerShooting();
            GameObject bullet = GameObject.Find("Bullet(Clone)");
            Assert.NotNull(bullet);
            yield return new WaitForSeconds(1.0f);
            bullet = GameObject.Find("Bullet(Clone)");
            Assert.Null(bullet);
        }

        private void setUpPlayer()
        {
            _player = GameObject.Find("Player");
            _animator = _player.GetComponent<Animator>();
            _2dMovement = _player.GetComponent<Runtime2DMovement>();
            _playerController = _player.GetComponent<PlayerController>();
            _gunManager = _player.GetComponent<GunManager>();
        }
    }
}
