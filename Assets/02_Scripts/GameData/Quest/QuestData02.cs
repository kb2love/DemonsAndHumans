using UnityEngine;
[CreateAssetMenu(fileName = "QuestData02", menuName = "ScriptableObjects/Quest/questData02", order = 1)]
public class QuestData02 : ScriptableObject
{
    [Header("����Ʈ �̹���")]
    public Sprite Image;
    [Header("����Ʈ �̸�")]
    public string Name;
    [Header("����Ʈ ����")]
    public string Content;
    [Header("���� Ƚ��")]
    public int killCount;
    [Header("��ƾ� �ϴ� Ƚ��")]
    public int clearCount;
    [Header("����Ʈ ����")]
    public int exp;
    public int gold;
    [Header("����Ʈ ��ġ")]
    public int Idx;
    [Header("����Ʈ ��������")]
    public bool Result;
}
