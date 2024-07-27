using UnityEngine;
[CreateAssetMenu(fileName = "MutantKillQuest", menuName = "ScriptableObjects/Quest/mariaQuest_01", order = 1)]
public class MutantKillQuest : ScriptableObject
{
    public QuestState questState;
    [Header("����Ʈ �̹���")]
    public Sprite Image;
    [Header("����Ʈ �̸�")]
    public string Name;
    [Header("����Ʈ ����")]
    public string Content;
    [Header("��ƾ��ϴ� �����̸�")]
    public string MutantName;
    [Header("���� Ƚ��")]
    public int KillCount;
    [Header("��ƾ� �ϴ� Ƚ��")]
    public int ClearCount;
    [Header("����Ʈ ����")]
    public int Exp;
    public int Gold;
    [Header("����Ʈ ��ġ")]
    public int Idx;
}
