using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundInst;
    private AudioSource bgAudioSource;
    private AudioSource effAudioSource;
    private void Awake()
    {
        if (soundInst == null)
            soundInst = this;
        else if(soundInst != this)
            Destroy(soundInst);
        DontDestroyOnLoad(soundInst);
    }
    public void BackGroundMusic(float volum)
    {
        bgAudioSource.volume = volum;
    }
    public void BackGroundMusic(AudioSource bgAudio,AudioClip audioClip)
    {
        bgAudio.clip = audioClip;
        bgAudio.loop = true;
        bgAudioSource = bgAudio;
        bgAudio.Play();
    }
    public void EffectSound(float volum)
    {
        effAudioSource.volume = volum;
    }
    public void EffectSound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
        effAudioSource = audioSource;
    }
}
