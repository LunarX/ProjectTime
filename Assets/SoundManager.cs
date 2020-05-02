using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip SkelHit, TargetHit;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        SkelHit = Resources.Load<AudioClip>("Explosion+1");
        TargetHit = Resources.Load<AudioClip>("TargetHit");

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
            case "skeletton":
                audioSrc.PlayOneShot(SkelHit);
                break;
            case "targetHit":
                audioSrc.PlayOneShot(TargetHit);
                break;
        }
    }
}
