using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip SkelHit, TargetHit, Miss, MGS;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    public float volume = 0.7f;
    void Start()
    {
        SkelHit = Resources.Load<AudioClip>("Explosion");
        TargetHit = Resources.Load<AudioClip>("TargetHit");
        Miss = Resources.Load<AudioClip>("Miss");
        MGS = Resources.Load<AudioClip>("MGS");

        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = volume;
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
            case "missSound":
                audioSrc.PlayOneShot(Miss);
                break;
            case "MGS":
                audioSrc.volume -= 0.3f;
                audioSrc.PlayOneShot(MGS);
                audioSrc.volume += 0.3f;
                break;
        }
    }
}
