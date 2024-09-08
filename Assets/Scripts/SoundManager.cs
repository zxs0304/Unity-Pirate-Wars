using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioClip hi;
    public AudioClip bang;
    public AudioClip wall;
    public AudioClip bgm;
    public AudioSource audioSourceBgm;
    public AudioSource audioSourceSound;

    public void PlaySound(AudioClip clip)
    {
        audioSourceSound.clip = clip;
        audioSourceSound.Play();

    }

    public void PlayBgm()
    {
        audioSourceBgm.clip = bgm;
        audioSourceBgm.Play();
    }


}
