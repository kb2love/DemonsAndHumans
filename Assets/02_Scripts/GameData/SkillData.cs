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
    [Header("��ų ����")]
    public AudioClip IceClip;
    [Header("���̾� ��ų")]
    [Header("��ų �̹���")]
    public Sprite FireImage;
    [Header("��ų �̸�")]
    public string FireSkillName;
    [Header("��ų ����")]
    public AudioClip FireClip;
    [Header("���� ��ų")]
    [Header("��ų �̹���")]
    public Sprite ElectroImage;
    [Header("��ų �̸�")]
    public string ElectroSkillName;
    [Header("��ų ����")]
    public AudioClip ElectroClip;
}
