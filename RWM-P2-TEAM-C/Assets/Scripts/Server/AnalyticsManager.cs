using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;

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
        else if (instance != this) Destroy(gameObject);
    }

    void Start() { }

    void Update() { }
}
