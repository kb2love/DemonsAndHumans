
using UnityEngine;

[CreateAssetMenu(fileName = "MutantKillerQuest", menuName = "ScriptableObjects/Quest/MutantKillerQuest", order = 3)]
public class MutantKillerQuest : ScriptableObject
{
    public QuestState questState;
    [Header("퀘스트 이미지")]
    public Sprite Image;
    [Header("퀘스트 이름")]
    public string Name;
    [Header("퀘스트 내용")]
    public string Content;
    [Header("잡아야하는 몬스터이름")]
    public string MutantName;
    public string BossName;
    [Header("잡은 횟수")]
    public int KillCount;
    public int BossKillCount;
    [Header("잡아야 하는 횟수")]
    public int ClearCount;
    public int BossClearCount;
    [Header("퀘스트 보상")]
    public Sprite RewardImage_01;
    public Sprite RewardImage_02;
    public int Exp;
    public int Gold;
    [Header("퀘스트 위치")]
    public int Idx;
}
