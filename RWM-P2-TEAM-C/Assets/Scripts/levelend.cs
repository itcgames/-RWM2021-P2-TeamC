using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelend : MonoBehaviour
{
   public void restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void MENU()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
