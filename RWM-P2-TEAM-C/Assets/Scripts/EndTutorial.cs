using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D t_other)
    {
        if (t_other.gameObject.tag == "Player")
        {
            Debug.Log("Player touched exit door");
            //SceneManager.LoadScene("Game");
        }
    }
}
