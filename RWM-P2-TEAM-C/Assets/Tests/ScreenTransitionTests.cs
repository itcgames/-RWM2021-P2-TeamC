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
            SceneManager.LoadScene("CameraTestScene", LoadSceneMode.Single);
        }

        [TearDown]
        public void Teardown()
        {
            SceneManager.UnloadSceneAsync("CameraTestScene");
        }

        [UnityTest]
        public IEnumerator TransitionStarts()
        {
            setupCamera();
            Vector3 pos = mainCam.transform.position;
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(100.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().StartMovement(0, ScreenTransition.transitionTypes.HORIZONTAL);

            yield return new WaitForSeconds(0.1f);

            Assert.AreNotEqual(pos, mainCam.transform.position);

        }

        [UnityTest]
        public IEnumerator TransitionEnds()
        {
            setupCamera();
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(1.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().StartMovement(0, ScreenTransition.transitionTypes.HORIZONTAL);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(false, mainCam.GetComponent<CameraMover>().m_moving);

        }

        [UnityTest]
        public IEnumerator AddingPoints()
        {
            setupCamera();
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(1.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(2.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(3.0f, 0.0f));
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(3, mainCam.GetComponent<CameraMover>().transitionPoints.Count);

        }

        [UnityTest]
        public IEnumerator RemovingLastAddedPoint()
        {
            setupCamera();
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(1.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(2.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().RemoveLastPoint();
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(1, mainCam.GetComponent<CameraMover>().transitionPoints.Count);

        }

        [UnityTest]
        public IEnumerator MovesToCorrectPoint()
        {
            setupCamera();
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(1.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().AddPoint(new Vector2(1000.0f, 0.0f));
            mainCam.GetComponent<CameraMover>().StartMovement(0, ScreenTransition.transitionTypes.HORIZONTAL);

            yield return new WaitForSeconds(0.5f);

            Assert.Less(mainCam.transform.position.x, mainCam.GetComponent<CameraMover>().transitionPoints[1].x);
            Assert.Less(mainCam.transform.position.x, mainCam.GetComponent<CameraMover>().transitionPoints[1].x + 0.1f);
            Assert.GreaterOrEqual(mainCam.transform.position.x, mainCam.GetComponent<CameraMover>().transitionPoints[0].x);

        }

        private void setupCamera()
        {
            mainCam = Camera.main;

            // remove any other points that may exist on the camera for our tests
            mainCam.GetComponent<CameraMover>().transitionPoints.Clear();
        }
    }
}
