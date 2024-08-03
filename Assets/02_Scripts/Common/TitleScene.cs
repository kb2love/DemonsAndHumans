using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject optionWindpw;
    [SerializeField] Slider BG_Slider;
    [SerializeField] Slider effect_Slider;
    [SerializeField] AudioClip effectClip;
    [SerializeField] CanvasGroup loadImage;
    [SerializeField] PlayerData playerData;
    private void Start()
    {
        BG_Slider.value = 0.5f;
        effect_Slider.value = 0.5f;
        SoundManager.soundInst.BackGroundMusic(0);
        DataManager.dataInst.LoadData();
        bool fileEx = DataManager.dataInst.FileExistst();
        if (fileEx)
            loadImage.alpha = 1.0f;
        else
            loadImage.alpha = 0.5f;
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
        if(DataManager.dataInst.gameData.IsSave)
        {
            SoundManager.soundInst.EffectSoundPlay(effectClip);
            DataManager.dataInst.LoadData();
            GameData gameData = DataManager.dataInst.gameData;
            SceneMove.SceneInst.LoadScene(gameData.sceneIdx, gameData);
        }
    }
    public void QuitGame()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
        SceneMove.SceneInst.QuitGame();
    }

    public void OptionOpen()
    {
        SoundManager.soundInst.EffectSoundPlay(effectClip);
        optionWindpw.SetActive(!optionWindpw.activeSelf);
    }
}
