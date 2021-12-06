using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip buster, death, landing, dink;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        buster = Resources.Load<AudioClip>("buster");
        death = Resources.Load<AudioClip>("death");
        landing = Resources.Load<AudioClip>("land");
        dink = Resources.Load<AudioClip>("dink");

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
            case "buster":
                audioSrc.PlayOneShot(buster);
                break;
            case "death":
                audioSrc.PlayOneShot(death);
                break;
            case "land":
                audioSrc.PlayOneShot(landing);
                break;
            case "dink":
                audioSrc.PlayOneShot(dink);
                break;
        }
    }
}
