using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 4)]
public class SkillData : ScriptableObject
{
    [Header("������ ���� ������ �̸�")]
    public string DataName;
    [Header("� ��ų�� �������")]
    public SkillState SkillState;
    [Header("��� ��������")]
    public SkillSelecState SkillSelecState;
    [Header("���̽� ��ų")]
    [Header("��ų �̹���")]
    public Sprite IceImage;
    [Header("��ų �̸�")]
    public string IceSkillName;
    [Header("���̾� ��ų")]
    [Header("��ų �̹���")]
    public Sprite FireImage;
    [Header("��ų �̸�")]
    public string FireSkillName;
    [Header("���� ��ų")]
    [Header("��ų �̹���")]
    public Sprite ElectroImage;
    [Header("��ų �̸�")]
    public string ElectroSkillName;
}
