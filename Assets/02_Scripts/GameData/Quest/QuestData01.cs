using UnityEngine;

[CreateAssetMenu(fileName = "QuestData01", menuName = "ScriptableObjects/Quest/QuestData01", order = 0)]
public class QuestData01 : ScriptableObject
{
    public NPCDialogue.QuestState_01 questState;
    [Header("����Ʈ �̹���")]
    public Sprite Image;
    [Header("����Ʈ �̸�")]
    public string Name;
    [Header("����Ʈ ����")]
    public string content;
    [Header("����Ʈ ����")]
    public int exp;
    [Header("����Ʈ ��ġ")]
    public int Idx;
}

