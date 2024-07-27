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
                stateText[4].text = "공격력 : " + playerData.AttackValue.ToString("0");
                break;
            case PlayerData.PlayerStat.DefenceValue:
                stateText[5].text = "방어력 : " + playerData.DefenceValue.ToString("0");
                break;
            case PlayerData.PlayerStat.FatalProbability:
                stateText[6].text = "치명타 확률 : " + (playerData.FatalProbability * 100).ToString("0.0") + "%";
                break;
            case PlayerData.PlayerStat.FatalValue:
                stateText[7].text = "치명타 공격력 : " + playerData.FatalValue.ToString("0") + "%";
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
    }//******************플레이어 경험치 업 및 레벨업하는 메서드****************//
    public void ExpUp(int exp)
    {
        // 플레이어의 경험치를 증가시킴
        playerData.expValue += exp;

        // 현재 경험치가 최대 경험치보다 큰 경우 레벨업을 반복적으로 수행
        while (playerData.expValue >= playerData.maxExpValue)
        {
            LevelUp();
        }

        // 경험치 이미지 업데이트
        expImage.fillAmount = playerData.expValue / playerData.maxExpValue;
    }
    private void LevelUp()
    {
        playerData.expValue -= playerData.maxExpValue;
        playerData.maxExpValue += 50.0f;
        playerData.MaxHP *= 1.1f;
        playerData.MaxMP *= 1.1f;
        playerData.HP = playerData.MaxHP;
        playerData.MP = playerData.MaxMP;
        playerData.AttackValue *= 1.1f;
        playerData.DefenceValue *= 1.1f;
        playerData.FatalValue *= 1.05f;
        playerData.FatalProbability *= 1.1f;
        playerData.Level++;
        if(playerData.Level == 5)
        {
            SkillManager.skillInst.Level5();
        }
        for(int i = 0; i < 10; i++)
        {
            AllStatUpdata();
        }
        StatUpdate(PlayerData.PlayerStat.Level);
    }
}
