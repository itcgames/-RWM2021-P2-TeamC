using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip temp;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        temp = Resources.Load<AudioClip>("temp");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "temp":
                audioSrc.PlayOneShot(temp);
                break;
        }
    }
}
