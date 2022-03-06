using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameState
{
    public int bulletsFired;
    public int deathCount;
    public int defeatedEnemies;
    public int level;
}

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager instance = null;
    public GameState data;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) DestroyImmediate(gameObject);

        data.level = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(gameObject); 
    }

    /// <summary>
    /// Used to reset our Singleton's data back to the start
    /// To be used when a new game is started.
    /// </summary>
    void resetdata()
    {
        data.bulletsFired = 0;
        data.deathCount = 0;
        data.defeatedEnemies = 0;
        data.level = SceneManager.GetActiveScene().buildIndex;
    }

    void Start() { }

    void Update() { }
}
