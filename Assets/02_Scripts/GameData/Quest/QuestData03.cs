using UnityEngine;


[CreateAssetMenu(fileName = "QuestData03", menuName = "ScriptableObjects/Quest/QuestData03", order = 2)]
public class QuestData03 : ScriptableObject
{
    public NPCDialogue.QuestState_01 questState;
    [Header("����Ʈ �̹���")]
    public Sprite Image;
    [Header("����Ʈ �̸�")]
    public string Name;
    [Header("����Ʈ ����")]
    public string Content;
    [Header("����Ʈ ����")]
    public int exp;
    [Header("����Ʈ ��ġ")]
    public int Idx;
}