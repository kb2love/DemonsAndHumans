using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Option : MonoBehaviour
{
    [SerializeField] AudioClip effectClip;
    [SerializeField] Slider bgSlider;
    [SerializeField] Slider effSlider;
    public void SaveGame()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
        DataManager.dataInst.DataSave();
        ItemManager.itemInst.AllItemSave();
        CanvasGroup ga = GameObject.Find("Text (Legacy)_Save").GetComponent<CanvasGroup>();
        ga.alpha = 1.0f;
        ga.GetComponent<CanvasGroup>().DOFade(0, 2.0f);
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
