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
        {
            soundInst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (soundInst != this)
        {
            Destroy(gameObject);
        }
        bgAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if(GameObject.FindWithTag("Player").gameObject != null)
        {
            effAudioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        }
    }
    public void BackGroundVolume(float volum)
    {
        bgAudioSource.volume = volum;
    }
    public void BackGroundMusic(AudioSource bgAudio,AudioClip audioClip)
    {
        bgAudio.clip = audioClip;
        bgAudio.loop = true;
        bgAudioSource = bgAudio;
        bgAudioSource.Play();
    }
    public void EffectSoundVolum(float volum)
    {
        effAudioSource.volume = volum;
    }
    public void EffectSoundPlay(AudioClip audioClip)
    {
        effAudioSource.PlayOneShot(audioClip);
    }
}
