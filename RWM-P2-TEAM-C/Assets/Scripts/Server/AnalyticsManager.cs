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
    public int level;
}

public class AnalyticsManager : MonoBehaviour
{
    public static IEnumerator PostMethod(GameState t_data)
    {
        string jsonData = JsonUtility.ToJson(t_data);

        string url = "http://localhost:5000/upload_data";
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
