using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundInst;
    private AudioSource bgAudioSource;
    [SerializeField] AudioSource effAudioSource;
    [SerializeField] AudioClip[] clips;
    private void Awake()
    {
        if (soundInst == null)
        {
            soundInst = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(effAudioSource);
        }
        else if (soundInst != this)
        {
            Destroy(gameObject);
        }
        bgAudioSource = GetComponent<AudioSource>();
    }
    public void BackGroundVolume(float volum)
    {
        bgAudioSource.volume = volum;
    }
    public void BackGroundMusic(int clipsIdx)
    {
        bgAudioSource.clip = clips[clipsIdx];
        bgAudioSource.loop = true;
        bgAudioSource.Play();
    }
    public void EffectSoundVolum(float _volum)
    {
        effAudioSource.volume = _volum;
    }
    public void EffectSoundPlay(AudioClip audioClip)
    {
        effAudioSource.PlayOneShot(audioClip);
    }
    public void EffectSoundPlay(AudioSource audioSource,AudioClip audioClip)
    {
        audioSource.volume = effAudioSource.volume;
        audioSource.PlayOneShot(audioClip);
    }
}
