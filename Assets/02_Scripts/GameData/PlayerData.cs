using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public enum PlayerStat { Level, HP, MP, MaxHP, MaxMP, AttackValue, DefenceValue, FatalProbability, FatalValue}
    public float HP;                //ü��
    public float MaxHP;             //�ִ�ü��
    public float MP;                //����
    public float MaxMP;             //�ִ븶��
    public float AttackValue;            //������
    public float MagicAttackValue;
    public float expValue;        //����ġ
    public float maxExpValue;     //�ִ����ġ
    public int Level;             //����
    public float DefenceValue;      //����
    public float FatalProbability;  //ġ��ŸȮ��
    public float FatalValue;        //ġ��Ÿ ���ݷ�
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
