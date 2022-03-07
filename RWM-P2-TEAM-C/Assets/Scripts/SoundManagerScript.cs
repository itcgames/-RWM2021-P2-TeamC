using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript instance = null;
    public static AudioClip bgm, buster, death, land, dink, bhit, phit;
    AudioSource audioSrc;


    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        bgm = Resources.Load<AudioClip>("SFX/bgm");
        buster = Resources.Load<AudioClip>("SFX/buster");
        death = Resources.Load<AudioClip>("SFX/death");
        land = Resources.Load<AudioClip>("SFX/land");
        dink = Resources.Load<AudioClip>("SFX/dink");
        bhit = Resources.Load<AudioClip>("SFX/bhit");
        phit = Resources.Load<AudioClip>("SFX/phit");


        //audioSrc = GetComponent<AudioSource>();
        PlaySound("bgm");

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string clip)
    {
        if(audioSrc == null)
        {
            Debug.Log("audio source is null!");
        }
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
            case "death":
                audioSrc.PlayOneShot(death);
                break;
            case "land":
                audioSrc.PlayOneShot(land);
                break;
            case "dink":
                audioSrc.PlayOneShot(dink);
                break;
            case "bhit":
                audioSrc.PlayOneShot(bhit);
                break;
            case "phit":
                audioSrc.PlayOneShot(phit);
                break;
        }
    }
}
