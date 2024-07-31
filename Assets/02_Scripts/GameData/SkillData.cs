using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 4)]
public class SkillData : ScriptableObject
{
    [Header("데이터 별로 구분할 이름")]
    public string DataName;
    [Header("어떤 스킬을 찍었는지")]
    public SkillState SkillState;
    [Header("어디에 찍혔는지")]
    public SkillSelecState SkillSelecState;
    [Header("아이스 스킬")]
    [Header("스킬 이미지")]
    public Sprite IceImage;
    [Header("스킬 이름")]
    public string IceSkillName;
    [Header("파이어 스킬")]
    [Header("스킬 이미지")]
    public Sprite FireImage;
    [Header("스킬 이름")]
    public string FireSkillName;
    [Header("번개 스킬")]
    [Header("스킬 이미지")]
    public Sprite ElectroImage;
    [Header("스킬 이름")]
    public string ElectroSkillName;
}
