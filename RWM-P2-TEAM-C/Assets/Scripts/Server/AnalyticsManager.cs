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
    public GameState data = new GameState; // new

    public static IEnumerator PostMethod()
    {
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
}