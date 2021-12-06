using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDeath : MonoBehaviour
{
    public GameObject deathOrbPrefab;
    public bool willRestartLevel;
    public float timeToRestart;
    public float orbSpeed;

    private bool restartLevel;
    private bool isDead = false;


    public void LateUpdate()
    {
        if (willRestartLevel)
        {
            if (restartLevel)
            { 
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void hasDied()
    {
        StartCoroutine("deathAnimationPlaying");
    }

    IEnumerator deathAnimationPlaying()
    {
        isDead = true;
        for (int i = 0; i < 12; i++)
        {
            GameObject orbInstant = Instantiate(deathOrbPrefab);
            orbInstant.transform.position = transform.position;

            if (i < 8)
            {
                orbInstant.GetComponent<DeathOrbController>().speed = orbSpeed * 2.0f; // outer orbs move twice as fast

                switch (i)
                { // set up first 8 orbs so they move out in a circle formation
                    case 0:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(-1, 0); // left
                        // nudge each of these first few orbs a little bit
                        // this is done to help give off more of a circle shape, rather than a box shape
                        orbInstant.transform.position += new Vector3(-1, 0);
                        break;
                    case 1:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(1, 0); // right
                        orbInstant.transform.position += new Vector3(1, 0);
                        break;
                    case 2:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(0, -1); // down
                        orbInstant.transform.position += new Vector3(0, -1);
                        break;
                    case 3:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(0, 1); // up
                        orbInstant.transform.position += new Vector3(0, 1);
                        break;
                    case 4:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(-1, 1); // top left
                        break;
                    case 5:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(1, -1); // top right
                        break;
                    case 6:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(-1, -1); // bottom left
                        break;
                    case 7:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(1, 1); // bottom right
                        break;
                }

            }
            else
            {
                orbInstant.GetComponent<DeathOrbController>().speed = orbSpeed; // inner orbs move at normal speed

                switch (i)
                { // set up each death orb here in a direction of the compass
                    case 8:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(-1, 0); // up
                        break;
                    case 9:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(1, 0); // down
                        break;
                    case 10:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(0, -1); // left
                        break;
                    case 11:
                        orbInstant.GetComponent<DeathOrbController>().moveDir = new Vector3(0, 1); // right
                        break;
                }
            }
        }

        // now wait a few seconds in order to restart the level
        yield return new WaitForSeconds(timeToRestart);

        restartLevel = willRestartLevel; // if this death script will restart the level, let it do it
        // this is done because the code will be re-used for our boss, and we don't want the level restarting abruptly.

        yield break; // stop the co-routine
    }

    public bool getIsDead()
    {
        return isDead;
    }
}
