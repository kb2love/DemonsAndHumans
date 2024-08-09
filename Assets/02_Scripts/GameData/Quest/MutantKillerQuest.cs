
using UnityEngine;

[CreateAssetMenu(fileName = "MutantKillerQuest", menuName = "ScriptableObjects/Quest/MutantKillerQuest", order = 3)]
public class MutantKillerQuest : ScriptableObject
{
    [Header("����Ʈ �̹���")]
    public Sprite Image;
    [Header("����Ʈ �̸�")]
    public string Name;
    [Header("����Ʈ ����")]
    public string Content;
    [Header("��ƾ��ϴ� �����̸�")]
    public string MutantName;
    public string BossName;
    [Header("��ƾ� �ϴ� Ƚ��")]
    public int ClearCount;
    public int BossClearCount;
    [Header("����Ʈ ����")]
    public Sprite RewardImage_01;
    public Sprite RewardImage_02;
    public int Exp;
    public int Gold;
}
