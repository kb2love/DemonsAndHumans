
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData04", menuName = "ScriptableObjects/Quest/QuestData04", order = 3)]
public class QuestData04 : ScriptableObject
{
    [Header("ù��° ����Ʈ")]// ���̷���
    public NPCDialogue.QuestState_01 questState_01;
    [Header("����Ʈ �̹���")]
    public Sprite Image_01;
    [Header("����Ʈ �̸�")]
    public string Name_01;
    [Header("����Ʈ ����")]
    public string Content_01;
    [Header("��ƾ��ϴ� �����̸�")]
    public string MutantName_01; 
    [Header("���� Ƚ��")]
    public int killCount_01;
    [Header("��ƾ� �ϴ� Ƚ��")]
    public int clearCount_01;
    [Header("����Ʈ ����")]
    public Sprite rewardImage_01_01;
    public Sprite rewardImage_02_01;
    public int exp_01;
    public int gold_01;
    [Header("����Ʈ ��ġ")]
    public int Idx_01;
    [Header("�ι�° ����Ʈ")]// �ϱޱ���, ������
    public NPCDialogue.QuestState_02 questState_02;
    [Header("����Ʈ �̹���")]
    public Sprite Image_02;
    [Header("����Ʈ �̸�")]
    public string Name_02;
    [Header("��ƾ��ϴ� �����̸�")]
    public string MutantName_02;
    public string bossName_02;
    [Header("����Ʈ ����")]
    public string Content_02;
    [Header("���� Ƚ��")]
    public int killCount_02_01;
    public int killCount_02_02;
    [Header("��ƾ� �ϴ� Ƚ��")]
    public int clearCount_02_01;
    public int clearCount_02_02;
    [Header("����Ʈ ����")]
    public Sprite rewardImage_01_02;
    public Sprite rewardImage_02_02;
    public int exp_02;
    public int gold_02;
    [Header("����Ʈ ��ġ")]
    public int Idx_02;
    [Header("����° ����Ʈ")]// �߱޸���, �ڸ���
    public NPCDialogue.QuestState_03 questState_03;
    [Header("����Ʈ �̹���")]
    public Sprite Image_03;
    [Header("����Ʈ �̸�")]
    public string Name_03;
    [Header("����Ʈ ����")]
    public string Content_03;
    [Header("��ƾ��ϴ� �����̸�")]
    public string MutantName_03;
    public string bossName_03;
    [Header("���� Ƚ��")]
    public int killCount_03_01;
    public int killCount_03_02;
    [Header("��ƾ� �ϴ� Ƚ��")]
    public int clearCount_03_01;
    public int clearCount_03_02;
    [Header("����Ʈ ����")]
    public Sprite rewardImage_01_03;
    public Sprite rewardImage_02_03;
    public int exp_03;
    public int gold_03;
    [Header("����Ʈ ��ġ")]
    public int Idx_03;
}
