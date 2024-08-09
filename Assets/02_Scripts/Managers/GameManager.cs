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

        // UI ������Ʈ �α�
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
                stateText[4].text = "���ݷ� : " + playerDataJson.AttackValue.ToString("0");
                break;
            case PlayerStat.DefenceValue:
                stateText[5].text = "���� : " + playerDataJson.DefenceValue.ToString("0");
                break;
            case PlayerStat.FatalProbability:
                stateText[6].text = "ġ��Ÿ Ȯ�� : " + (playerDataJson.FatalProbability).ToString("0.0") + "%";
                break;
            case PlayerStat.FatalValue:
                stateText[7].text = "ġ��Ÿ ���ݷ� : " + playerDataJson.FatalAttackValue.ToString("0") + "%";
                break;
            case PlayerStat.MagciAttackValue:
                stateText[11].text = "���� ������ : " + playerDataJson.MagicAttackValue.ToString("0");
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
    }//******************�÷��̾� ����ġ �� �� �������ϴ� �޼���****************//
    public void ExpUp(int exp)
    {
        // �÷��̾��� ����ġ�� ������Ŵ
        playerDataJson.expValue += exp;

        // ���� ����ġ�� �ִ� ����ġ���� ū ��� �������� �ݺ������� ����
        while (playerDataJson.expValue >= playerDataJson.maxExpValue)
        {
            LevelUp();
        }

        // ����ġ �̹��� ������Ʈ
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
        if (playerDataJson.Level == 5) { SkillManager.skillInst.Level5(); skillPoint.text = "��ų����Ʈ : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 10) { SkillManager.skillInst.Level10(); skillPoint.text = "��ų����Ʈ : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 20) { SkillManager.skillInst.Level20(); skillPoint.text = "��ų����Ʈ : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 25) { SkillManager.skillInst.Level25(); skillPoint.text = "��ų����Ʈ : " + playerDataJson.levelSkillPoint; }
        else if (playerDataJson.Level == 30) { QuestManager.questInst.PlayerLevel30(); SkillManager.skillInst.Level30(); skillPoint.text = "��ų����Ʈ : " + playerDataJson.levelSkillPoint; }
        for (int i = 0; i < 10; i++)
        {
            AllStatUpdata();
        }
        StatUpdate(PlayerStat.Level);
    }
}
