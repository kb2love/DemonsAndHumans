using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField] AudioClip effectClip;
    [SerializeField] Slider bgSlider;
    [SerializeField] Slider effSlider;
    public void SaveGame()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
        DataManager.dataInst.SaveData();
    }
    public void QuitGame()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
        SceneMove.SceneInst.QuitGame();
    }

    public void SetBackgroundMusicVolume()
    {
        SoundManager.soundInst.BackGroundVolume(bgSlider.value);
    }

    // 효과음 볼륨 설정 함수
    public void SetSoundEffectsVolume()
    {
        SoundManager.soundInst.EffectSoundVolum(effSlider.value);
    }
}
