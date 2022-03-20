using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D t_other)
    {
        if (t_other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
