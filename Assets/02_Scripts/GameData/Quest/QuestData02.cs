using UnityEngine;
[CreateAssetMenu(fileName = "QuestData02", menuName = "ScriptableObjects/Quest/questData02", order = 1)]
public class QuestData02 : ScriptableObject
{
    [Header("퀘스트 이미지")]
    public Sprite Image;
    [Header("퀘스트 이름")]
    public string Name;
    [Header("퀘스트 내용")]
    public string Content;
    [Header("잡은 횟수")]
    public int killCount;
    [Header("잡아야 하는 횟수")]
    public int clearCount;
    [Header("퀘스트 보상")]
    public int exp;
    public int gold;
    [Header("퀘스트 위치")]
    public int Idx;
    [Header("퀘스트 성공여부")]
    public bool Result;
}
