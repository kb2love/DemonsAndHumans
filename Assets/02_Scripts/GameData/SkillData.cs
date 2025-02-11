using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 4)]
public class SkillData : ScriptableObject
{
    [Header("데이터 별로 구분할 이름")]
    public string DataName;
    [Header("아이스 스킬")]
    [Header("스킬 이미지")]
    public Sprite IceImage;
    [Header("스킬 이름")]
    public string IceSkillName;
    [Header("스킬 사운드")]
    public AudioClip IceClip;
    [Header("파이어 스킬")]
    [Header("스킬 이미지")]
    public Sprite FireImage;
    [Header("스킬 이름")]
    public string FireSkillName;
    [Header("스킬 사운드")]
    public AudioClip FireClip;
    [Header("번개 스킬")]
    [Header("스킬 이미지")]
    public Sprite ElectroImage;
    [Header("스킬 이름")]
    public string ElectroSkillName;
    [Header("스킬 사운드")]
    public AudioClip ElectroClip;
}
