using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform inventory;
    [SerializeField] PlayerData playerData;
    [SerializeField] List<Text> stateText = new List<Text>();
    [SerializeField] Image expImage;
    [SerializeField] Image hpImage;
    [SerializeField] Image mpImage;
    [SerializeField] Text skillPoint;
    public static GameManager GM;
    public PlayerDataJson playerDataJson = new PlayerDataJson();
    private void Awake()
    {
        GM = this;
    }
    public void Initialize()
    {

        AllStatUpdata();

        // UI 업데이트 로그
        expImage.fillAmount = playerDataJson.expValue / playerDataJson.maxExpValue;
        hpImage.fillAmount = playerDataJson.HP / playerDataJson.MaxHP;
        mpImage.fillAmount = playerDataJson.MP / playerDataJson.MaxMP;

    }
    public void StatUpdate(PlayerStat playerStat)
    {
        switch (playerStat)
        {
            case PlayerStat.Level:
                stateText[8].text = "Level : " + playerDataJson.Level.ToString();
                stateText[9].text = "Lev : " + playerDataJson.Level.ToString();
                break;
            case PlayerStat.HP:
                stateText[0].text = "HP : " + playerDataJson.HP.ToString("0");
                break;
            case PlayerStat.MP:
                stateText[1].text = "MP : " + playerDataJson.MP.ToString("0");
                break;
            case PlayerStat.MaxHP:
                stateText[2].text = "MaxHP : " + playerDataJson.MaxHP.ToString("0");
                break;
            case PlayerStat.MaxMP:
                stateText[3].text = "MaxMP : " + playerDataJson.MaxMP.ToString("0");
                break;
            case PlayerStat.AttackValue:
                stateText[4].text = "공격력 : " + playerDataJson.AttackValue.ToString("0");
                break;
            case PlayerStat.DefenceValue:
                stateText[5].text = "방어력 : " + playerDataJson.DefenceValue.ToString("0");
                break;
            case PlayerStat.FatalProbability:
                stateText[6].text = "치명타 확률 : " + (playerDataJson.FatalProbability).ToString("0.0") + "%";
                break;
            case PlayerStat.FatalValue:
                stateText[7].text = "치명타 공격력 : " + playerDataJson.FatalAttackValue.ToString("0") + "%";
                break;
            case PlayerStat.MagciAttackValue:
                stateText[11].text = "마법 데미지 : " + playerDataJson.MagicAttackValue.ToString("0");
                break;
        }
    }
    public void AllStatUpdata()
    {
        StatUpdate(PlayerStat.Level);
        StatUpdate(PlayerStat.HP);
        StatUpdate(PlayerStat.MP);
        StatUpdate(PlayerStat.MaxHP);
        StatUpdate(PlayerStat.MaxMP);
        StatUpdate(PlayerStat.AttackValue);
        StatUpdate(PlayerStat.DefenceValue);
        StatUpdate(PlayerStat.FatalValue);
        StatUpdate(PlayerStat.FatalProbability);
        StatUpdate(PlayerStat.MagciAttackValue);
    }//******************플레이어 경험치 업 및 레벨업하는 메서드****************//
    public void ExpUp(int exp)
    {
        // 플레이어의 경험치를 증가시킴
        playerDataJson.expValue += exp;

        // 현재 경험치가 최대 경험치보다 큰 경우 레벨업을 반복적으로 수행
        while (playerDataJson.expValue >= playerDataJson.maxExpValue)
        {
            LevelUp();
        }

        // 경험치 이미지 업데이트
        expImage.fillAmount = playerDataJson.expValue / playerDataJson.maxExpValue;
    }
    public void ReSetGame()
    {
        playerDataJson.HP = 100;
        playerDataJson.MaxHP = 100;
        playerDataJson.MP = 100;
        playerDataJson.MaxMP = 100;
        playerDataJson.AttackValue = 10;
        playerDataJson.MagicAttackValue = 0;
        playerDataJson.expValue = 0;
        playerDataJson.maxExpValue = 100;
        playerDataJson.Level = 1;
        playerDataJson.DefenceValue = 5;
        playerDataJson.FatalProbability = 0.05f;
        playerDataJson.FatalAttackValue = 150f;
        playerDataJson.GoldValue = 10;
        playerDataJson.levelSkillPoint = 0;
        playerDataJson.currentSceneIdx = 0;
    }
    private void LevelUp()
    {
        playerDataJson.expValue -= playerDataJson.maxExpValue;
        playerDataJson.maxExpValue += 50.0f;
        playerDataJson.MaxHP += 15f;
        playerDataJson.MaxMP += 15f;
        playerDataJson.HP = playerDataJson.MaxHP;
        playerDataJson.MP = playerDataJson.MaxMP;
        hpImage.fillAmount = playerDataJson.HP / playerDataJson.MaxHP;
        mpImage.fillAmount = playerDataJson.MP / playerDataJson.MaxMP;
        playerDataJson.AttackValue += 4.5f;
        playerDataJson.DefenceValue += 1.5f;
        playerDataJson.FatalAttackValue += 1f;
        playerDataJson.FatalProbability += 0.25f;
        playerDataJson.MagicAttackValue += 1;
        playerDataJson.Level++;
        DataManager.dataInst.PlayerDataSave(playerDataJson);
        SoundManager.soundInst.EffectSoundPlay(playerData.levelUpClip);
        if (playerDataJson.Level == 5) { SkillManager.skillInst.Level5(); skillPoint.text = "스킬포인트 : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 10) { SkillManager.skillInst.Level10(); skillPoint.text = "스킬포인트 : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 20) { SkillManager.skillInst.Level20(); skillPoint.text = "스킬포인트 : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 25) { SkillManager.skillInst.Level25(); skillPoint.text = "스킬포인트 : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 30) { QuestManager.questInst.PlayerLevel30(); SkillManager.skillInst.Level30(); skillPoint.text = "스킬포인트 : " + playerDataJson.levelSkillPoint; }
        for (int i = 0; i < 10; i++)
        {
            AllStatUpdata();
        }
        StatUpdate(PlayerStat.Level);
    }
}
