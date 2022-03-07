using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance = null;
    public static AudioClip bgm, buster;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) DestroyImmediate(gameObject);

        bgm = Resources.Load<AudioClip>("SFX/bgm");
        buster = Resources.Load<AudioClip>("SFX/buster");

        audioSrc = GetComponent<AudioSource>();
        PlaySound("bgm");

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "bgm":
                audioSrc.volume = 0.05f;
                audioSrc.loop = true;
                audioSrc.clip = bgm;
                audioSrc.Play();
                break;
            case "buster":
                audioSrc.PlayOneShot(buster);
                break;
        }
    }
}
