using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("Game");
    }

    public void train()
    {
        Debug.Log("DOESNT EXIST YET");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
