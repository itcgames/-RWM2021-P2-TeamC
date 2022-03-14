using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


namespace Tests
{
    public class Bosstest
    {
         GameObject _player;
         GameObject _boss;
         GameObject _door;
         Runtime2DMovement _2dMovement;
        // A Test behaves as an ordinary method
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("BossTestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("BossTestScene");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator BossLookRightTest()
        {
            setUpBoss();
            _boss = GameObject.FindGameObjectWithTag("Boss");
            _2dMovement.handleRightInput();
            _2dMovement.moveRight();
            _boss.GetComponent<Boss>().hit = false;
            _boss.GetComponent<Boss>().LookAtPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(false, _boss.GetComponent<Boss>().direction == 1);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
        }

        [UnityTest]
        public IEnumerator BossLookLeftTest()
        {
            setUpBoss();
            _boss = GameObject.FindGameObjectWithTag("Boss");
            _2dMovement.handleLeftInput();
            _2dMovement.moveLeft();
            _boss.GetComponent<Boss>().hit = false;
            _boss.GetComponent<Boss>().LookAtPlayer();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, _boss.GetComponent<Boss>().direction == -1);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
        }
        [UnityTest]
        public IEnumerator BossShootPlayer()
        {
            setUpBoss(); 
            _boss = GameObject.FindGameObjectWithTag("Boss");
            _boss.GetComponent<Boss>().CheckIfTimeToFire();
            _2dMovement.handleLeftInput();
            yield return new WaitForSeconds(1.0f);
            Assert.AreEqual(true, (_boss.GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= 0));
        }

        [UnityTest]
        public IEnumerator BossDamageTest()
        {
            _boss = GameObject.FindGameObjectWithTag("Boss");
            float initialHealth = _boss.GetComponent<Boss>().getHealth();
            _boss.GetComponent<Boss>().damage(0.5f);
            _boss.GetComponent<Boss>().hit = true;
            yield return new WaitForSeconds(0.1f);
            Assert.Less(_boss.GetComponent<Boss>().getHealth(), initialHealth);
        }

        private void setUpBoss()
        {
            _player = GameObject.Find("Player");
            _boss = GameObject.Find("Boss");
            _door = GameObject.Find("Door");
            _2dMovement = _player.GetComponent<Runtime2DMovement>();
            

            GameObject _soundManager = GameObject.Find("SoundManager");

            if (_soundManager.GetComponent<AudioSource>() == null)
            {
                Debug.Log("null");
            }
        }
    }
}
