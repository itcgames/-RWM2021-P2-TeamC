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

            TransitionPoint test = new TransitionPoint();
            test.transitionPoint = new Vector2(5.0f, 0.0f);
            test.type = TransitionTypes.HORIZONTAL;


            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<CameraMover>().StartMovement(0);

            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual(true, mainCam.GetComponent<CameraMover>().m_moving);

            yield return new WaitForSeconds(0.5f);

            Assert.AreNotEqual(pos, mainCam.transform.position);

        }

        [UnityTest]
        public IEnumerator TransitionEnds()
        {
            setupCamera();

            TransitionPoint test = new TransitionPoint();
            test.transitionPoint = new Vector2(2.0f, 0.0f);
            test.type = TransitionTypes.HORIZONTAL;

            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<CameraMover>().StartMovement(0);

            yield return new WaitForSeconds(0.5f);

            Assert.AreEqual(false, mainCam.GetComponent<CameraMover>().m_moving);

        }

        [UnityTest]
        public IEnumerator AddingPoints()
        {
            setupCamera();

            TransitionPoint test = new TransitionPoint();
            test.transitionPoint = new Vector2(1.0f, 0.0f);
            test.type = TransitionTypes.HORIZONTAL;

            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(3, mainCam.GetComponent<ScreenTransition>().transitionPoints.Count);

        }

        [UnityTest]
        public IEnumerator RemovingLastAddedPoint()
        {
            setupCamera();

            TransitionPoint test = new TransitionPoint();
            test.transitionPoint = new Vector2(1.0f, 0.0f);
            test.type = TransitionTypes.HORIZONTAL;

            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<ScreenTransition>().RemoveLastPoint();
            yield return new WaitForSeconds(0.1f);

            Assert.AreEqual(1, mainCam.GetComponent<ScreenTransition>().transitionPoints.Count);

        }

        [UnityTest]
        public IEnumerator MovesToCorrectPoint()
        {
            setupCamera();

            TransitionPoint test = new TransitionPoint();
            test.transitionPoint = new Vector2(2.0f, 0.0f);
            test.type = TransitionTypes.HORIZONTAL;

            TransitionPoint test2 = new TransitionPoint();
            test2.transitionPoint = new Vector2(3.0f, 0.0f);
            test2.type = TransitionTypes.HORIZONTAL;

            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<ScreenTransition>().AddPoint(test2);
            mainCam.GetComponent<CameraMover>().StartMovement(0);

            yield return new WaitForSeconds(0.5f);

            Assert.Less(mainCam.transform.position.x, mainCam.GetComponent<ScreenTransition>().transitionPoints[1].transitionPoint.x);
            Assert.Less(mainCam.transform.position.x, mainCam.GetComponent<ScreenTransition>().transitionPoints[1].transitionPoint.x + 0.1f);
            Assert.GreaterOrEqual(mainCam.transform.position.x, mainCam.GetComponent<ScreenTransition>().transitionPoints[0].transitionPoint.x);

        }

        [UnityTest]
        public IEnumerator DestroysEnemyOnTransition()
        {
            setupCamera();

            TransitionPoint test = new TransitionPoint();
            test.transitionPoint = new Vector2(2.0f, 0.0f);
            test.type = TransitionTypes.HORIZONTAL;

            TransitionPoint test2 = new TransitionPoint();
            test2.transitionPoint = new Vector2(3.0f, 0.0f);
            test2.type = TransitionTypes.HORIZONTAL;

            GameObject enemy = GameObject.FindGameObjectWithTag("Follower");

            mainCam.GetComponent<ScreenTransition>().AddPoint(test);
            mainCam.GetComponent<ScreenTransition>().AddPoint(test2);
            mainCam.GetComponent<CameraMover>().StartMovement(0);

            yield return new WaitForSeconds(0.2f);

            Assert.AreEqual(true, !enemy);

        }

        private void setupCamera()
        {
            mainCam = Camera.main;

            // remove any other points that may exist on the camera for our tests
            mainCam.GetComponent<ScreenTransition>().transitionPoints.Clear();
        }
    }
}
