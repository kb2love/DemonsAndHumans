using UnityEngine;
[CreateAssetMenu(fileName = "QuestData02", menuName = "ScriptableObjects/Quest/questData02", order = 1)]
public class QuestData02 : ScriptableObject
{
    [Header("ù��° ����Ʈ")]
    public NPCDialogue.QuestState_01 questState;
    [Header("����Ʈ �̹���")]
    public Sprite Image_01;
    [Header("����Ʈ �̸�")]
    public string Name_01;
    [Header("����Ʈ ����")]
    public string Content_01;
    [Header("��ƾ��ϴ� �����̸�")]
    public string MutantName;
    [Header("���� Ƚ��")]
    public int killCount;
    [Header("��ƾ� �ϴ� Ƚ��")]
    public int clearCount;
    [Header("����Ʈ ����")]
    public int exp_01;
    public int gold;
    [Header("����Ʈ ��ġ")]
    public int Idx_01;
    [Header("�ι�° ����Ʈ")]
    public NPCDialogue.QuestState_02 questState_02;
    [Header("����Ʈ �̹���")]
    public Sprite Image_02;
    [Header("����Ʈ �̸�")]
    public string Name_02;
    [Header("����Ʈ ����")]
    public string Content_02;
    [Header("����Ʈ ����")]
    public int exp_02;
    [Header("����Ʈ ��ġ")]
    public int Idx_02;
}
