using UnityEngine;
[CreateAssetMenu(fileName = "MutantKillQuest", menuName = "ScriptableObjects/Quest/mariaQuest_01", order = 1)]
public class MutantKillQuest : ScriptableObject
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
    [Header("잡은 횟수")]
    public int KillCount;
    [Header("잡아야 하는 횟수")]
    public int ClearCount;
    [Header("퀘스트 보상")]
    public int Exp;
    public int Gold;
    [Header("퀘스트 위치")]
    public int Idx;
}
