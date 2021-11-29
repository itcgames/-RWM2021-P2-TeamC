using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ScreenTransitionTests
    {
        private Camera mainCam;

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
        public IEnumerator TransitionStarts()
        {
            setupCamera();
            Vector3 pos = mainCam.transform.position;
            mainCam.GetComponent<CameraMover>().StartMovement();

            yield return new WaitForSeconds(0.1f);

            Assert.AreNotEqual(pos, mainCam.transform.position);

        }

        [UnityTest]
        public IEnumerator TransitionEnds()
        {
            setupCamera();
            Vector3 pos = mainCam.transform.position;
            mainCam.GetComponent<ScreenTransition>().transitionPoint = new Vector2(1.0f, 0.0f);
            mainCam.GetComponent<CameraMover>().StartMovement();

            yield return new WaitForSeconds(1.5f);

            Assert.AreEqual(true, mainCam.GetComponent<CameraMover>().m_moving);

        }



        private void setupCamera()
        {
            mainCam = Camera.main;
        }
    }
}
