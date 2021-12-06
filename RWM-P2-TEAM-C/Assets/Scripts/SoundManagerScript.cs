using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip buster, death;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        buster = Resources.Load<AudioClip>("buster");
        death = Resources.Load<AudioClip>("death");

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
        }
    }
}
