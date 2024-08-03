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
    public static GameManager GM;
    private void Awake()
    {
        GM = this;
    }
    public void Initialize()
    {
        AllStatUpdata();
        expImage.fillAmount = playerData.expValue / playerData.maxExpValue;
    }
    public void StatUpdate(PlayerData.PlayerStat playerStat)
    {
        switch (playerStat)
        {
            case PlayerData.PlayerStat.Level:
                stateText[8].text = "Level : " + playerData.Level.ToString();
                stateText[9].text = "Lev : " + playerData.Level.ToString();
                break;
            case PlayerData.PlayerStat.HP:
                stateText[0].text = "HP : " + playerData.HP.ToString("0");
                break;
            case PlayerData.PlayerStat.MP:
                stateText[1].text = "MP : " + playerData.MP.ToString("0");
                break;
            case PlayerData.PlayerStat.MaxHP:
                stateText[2].text = "MaxHP : " + playerData.MaxHP.ToString("0");
                break;
            case PlayerData.PlayerStat.MaxMP:
                stateText[3].text = "MaxMP : " + playerData.MaxMP.ToString("0");
                break;
            case PlayerData.PlayerStat.AttackValue:
                stateText[4].text = "���ݷ� : " + playerData.AttackValue.ToString("0");
                break;
            case PlayerData.PlayerStat.DefenceValue:
                stateText[5].text = "���� : " + playerData.DefenceValue.ToString("0");
                break;
            case PlayerData.PlayerStat.FatalProbability:
                stateText[6].text = "ġ��Ÿ Ȯ�� : " + (playerData.FatalProbability * 100).ToString("0.0") + "%";
                break;
            case PlayerData.PlayerStat.FatalValue:
                stateText[7].text = "ġ��Ÿ ���ݷ� : " + playerData.FatalValue.ToString("0") + "%";
                break;
        }
    }
    public void AllStatUpdata()
    {
        StatUpdate(PlayerData.PlayerStat.Level);
        StatUpdate(PlayerData.PlayerStat.HP);
        StatUpdate(PlayerData.PlayerStat.MP);
        StatUpdate(PlayerData.PlayerStat.MaxHP);
        StatUpdate(PlayerData.PlayerStat.MaxMP);
        StatUpdate(PlayerData.PlayerStat.AttackValue);
        StatUpdate(PlayerData.PlayerStat.DefenceValue);
        StatUpdate(PlayerData.PlayerStat.FatalValue);
        StatUpdate(PlayerData.PlayerStat.FatalProbability);
    }//******************�÷��̾� ����ġ �� �� �������ϴ� �޼���****************//
    public void ExpUp(int exp)
    {
        // �÷��̾��� ����ġ�� ������Ŵ
        playerData.expValue += exp;

        // ���� ����ġ�� �ִ� ����ġ���� ū ��� �������� �ݺ������� ����
        while (playerData.expValue >= playerData.maxExpValue)
        {
            LevelUp();
        }

        // ����ġ �̹��� ������Ʈ
        expImage.fillAmount = playerData.expValue / playerData.maxExpValue;
    }
    private void LevelUp()
    {
        playerData.expValue -= playerData.maxExpValue;
        playerData.maxExpValue += 50.0f;
        playerData.MaxHP *= 1.05f;
        playerData.MaxMP *= 1.05f;
        playerData.HP = playerData.MaxHP;
        playerData.MP = playerData.MaxMP;
        hpImage.fillAmount = playerData.HP / playerData.MaxHP;
        mpImage.fillAmount = playerData.MP / playerData.MaxMP;
        playerData.AttackValue *= 1.1f;
        playerData.DefenceValue *= 1.1f;
        playerData.FatalValue *= 1.05f;
        playerData.FatalProbability *= 1.1f;
        playerData.Level++;
        SoundManager.soundInst.EffectSoundPlay(playerData.levelUpClip);
        if (playerData.Level == 5) { SkillManager.skillInst.Level5(); }
        else if (playerData.Level == 10) { SkillManager.skillInst.Level10(); }
        else if (playerData.Level == 20) { SkillManager.skillInst.Level20(); }
        else if (playerData.Level == 21) { SkillManager.skillInst.Level20(); }
        else if (playerData.Level == 30) { QuestManager.questInst.PlayerLevel30(); SkillManager.skillInst.Level30(); }
        for (int i = 0; i < 10; i++)
        {
            AllStatUpdata();
        }
        StatUpdate(PlayerData.PlayerStat.Level);
    }
}
