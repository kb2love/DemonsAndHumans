using UnityEngine;


[CreateAssetMenu(fileName = "QuestData03", menuName = "ScriptableObjects/Quest/QuestData03", order = 2)]
public class QuestData03 : ScriptableObject
{
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
    [Header("����Ʈ ��������")]
    public bool Result;
    [Header("����Ʈ ���ֿ���")]
    public bool Take;
}