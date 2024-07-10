using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject optionWindpw;
    [SerializeField] Slider BG_Slider;
    [SerializeField] Slider effect_Slider;
    [SerializeField] AudioClip bgClip;
    [SerializeField] AudioClip effectClip;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource effectSource;
    private void Start()
    {
        BG_Slider.value = 0.5f;
        effect_Slider.value = 0.5f;
        SoundManager.soundInst.BackGroundMusic(audioSource, bgClip);
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        SoundManager.soundInst.BackGroundVolume(volume);
    }

    // 효과음 볼륨 설정 함수
    public void SetSoundEffectsVolume(float volume)
    {
        SoundManager.soundInst.EffectSoundVolum(volume);
    }
    public void StartGame()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
        SceneMove.SceneInst.CatleScene();
    }
    public void LoadGame()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
    }
    public void QuitGame()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
        SceneMove.SceneInst.QuitGame();
    }
    public void OptionOpen()
    {
        if (optionWindpw.activeSelf)
        {
            SoundManager.soundInst.EffectSoundPlay(effectClip);
            optionWindpw.SetActive(false);
        }
        else
        {
            SoundManager.soundInst.EffectSoundPlay(effectClip);
            optionWindpw.SetActive(true);
        }
    }
}
