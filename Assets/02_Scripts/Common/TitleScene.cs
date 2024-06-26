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
        SoundManager.soundInst.BackGroundMusic(volume);
        Debug.Log(volume);
    }

    // 효과음 볼륨 설정 함수
    public void SetSoundEffectsVolume(float volume)
    {
        SoundManager.soundInst.EffectSound(volume);
    }
    public void StartGame()
    {
        SoundManager.soundInst.EffectSound(effectSource, effectClip);
        SceneMove.SceneInst.CatleScene();
    }
    public void LoadGame()
    {
        SoundManager.soundInst.EffectSound(effectSource, effectClip);
    }
    public void QuitGame()
    {
        SoundManager.soundInst.EffectSound(effectSource, effectClip);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 유니티 에디터를 종료합니다.
#else
            Application.Quit(); // 어플리케이션을 종료합니다.
#endif
    }
    public void OptionOpen()
    {
        if (optionWindpw.activeSelf)
        {
            SoundManager.soundInst.EffectSound(effectSource, effectClip);
            optionWindpw.SetActive(false);
        }
        else
        {
            SoundManager.soundInst.EffectSound(effectSource, effectClip);
            optionWindpw.SetActive(true);
        }
    }
}
