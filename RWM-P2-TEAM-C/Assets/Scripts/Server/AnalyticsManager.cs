using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameState
{
    public int bulletsFired = 0;
    public int deathCount = 0;
    public int defeatedEnemies = 0;
    public int level = 0;
    public string version = "week_1";
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

    public IEnumerator PostMethod()
    {
        Debug.Log("Posting data...");
        string jsonData = JsonUtility.ToJson(data); // uses pre-made data object

        string url = "http://34.242.150.74/upload_data";
        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
                Debug.Log("Data successfully sent to the server");
            else
                Debug.Log("Error sending data to the server: Error " + request.responseCode);
        }
    }

    void Start() { }

    void Update() { }
}
