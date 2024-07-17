using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public enum PlayerStat { Level, HP, MP, MaxHP, MaxMP, AttackValue, DefenceValue, FatalProbability, FatalValue}
    public float HP;                //체력
    public float MaxHP;             //최대체력
    public float MP;                //마나
    public float MaxMP;             //최대마나
    public float AttackValue;            //데미지
    public float MagicAttackValue;
    public float expValue;        //경험치
    public float maxExpValue;     //최대경험치
    public int Level;             //레벨
    public float DefenceValue;      //방어력
    public float FatalProbability;  //치명타확률
    public float FatalValue;        //치명타 공격력
    public float level05MagicDamage;
    public float level10MagicDamage;
    public float level15_1MagicDamage;
    public float level15_2MagicDamage;
    public float level25MagicDamage;
    public int GoldValue;
    public int levelSkillPoint;
    public int level05SkillIdx;
    public int level10SkillIdx;
    public int level15_1SkillIdx;
    public int level15_2SkillIdx;
    public int level25SkillIdx;
    public int playerSceneIdx;
    public AudioClip AttackDownClip;
    public AudioClip AttackUpClip;
    public AudioClip AttackFinishClip;
    public AudioClip SkillIce;
    public AudioClip SkillElectro;
    public AudioClip SkillFire;
    public GameObject hitEff;
    public GameObject iceSpear;
}
