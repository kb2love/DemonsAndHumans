using UnityEngine;
[CreateAssetMenu(fileName = "QuestData02", menuName = "ScriptableObjects/Quest/questData02", order = 1)]
public class QuestData02 : ScriptableObject
{
    [Header("첫번째 퀘스트")]
    public NPCDialogue.QuestState_01 questState;
    [Header("퀘스트 이미지")]
    public Sprite Image_01;
    [Header("퀘스트 이름")]
    public string Name_01;
    [Header("퀘스트 내용")]
    public string Content_01;
    [Header("잡아야하는 몬스터이름")]
    public string MutantName;
    [Header("잡은 횟수")]
    public int killCount;
    [Header("잡아야 하는 횟수")]
    public int clearCount;
    [Header("퀘스트 보상")]
    public int exp_01;
    public int gold;
    [Header("퀘스트 위치")]
    public int Idx_01;
    [Header("두번째 퀘스트")]
    public NPCDialogue.QuestState_02 questState_02;
    [Header("퀘스트 이미지")]
    public Sprite Image_02;
    [Header("퀘스트 이름")]
    public string Name_02;
    [Header("퀘스트 내용")]
    public string Content_02;
    [Header("퀘스트 보상")]
    public int exp_02;
    [Header("퀘스트 위치")]
    public int Idx_02;
}
