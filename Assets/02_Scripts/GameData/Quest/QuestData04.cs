
using UnityEngine;

[CreateAssetMenu(fileName = "QuestData04", menuName = "ScriptableObjects/Quest/QuestData04", order = 3)]
public class QuestData04 : ScriptableObject
{
    [Header("첫번째 퀘스트")]// 스켈레톤
    public NPCDialogue.QuestState_01 questState_01;
    [Header("퀘스트 이미지")]
    public Sprite Image_01;
    [Header("퀘스트 이름")]
    public string Name_01;
    [Header("퀘스트 내용")]
    public string Content_01;
    [Header("잡아야하는 몬스터이름")]
    public string MutantName_01; 
    [Header("잡은 횟수")]
    public int killCount_01;
    [Header("잡아야 하는 횟수")]
    public int clearCount_01;
    [Header("퀘스트 보상")]
    public Sprite rewardImage_01_01;
    public Sprite rewardImage_02_01;
    public int exp_01;
    public int gold_01;
    [Header("퀘스트 위치")]
    public int Idx_01;
    [Header("두번째 퀘스트")]// 하급귀족, 투르악
    public NPCDialogue.QuestState_02 questState_02;
    [Header("퀘스트 이미지")]
    public Sprite Image_02;
    [Header("퀘스트 이름")]
    public string Name_02;
    [Header("잡아야하는 몬스터이름")]
    public string MutantName_02;
    public string bossName_02;
    [Header("퀘스트 내용")]
    public string Content_02;
    [Header("잡은 횟수")]
    public int killCount_02_01;
    public int killCount_02_02;
    [Header("잡아야 하는 횟수")]
    public int clearCount_02_01;
    public int clearCount_02_02;
    [Header("퀘스트 보상")]
    public Sprite rewardImage_01_02;
    public Sprite rewardImage_02_02;
    public int exp_02;
    public int gold_02;
    [Header("퀘스트 위치")]
    public int Idx_02;
    [Header("세번째 퀘스트")]// 중급마족, 자르간
    public NPCDialogue.QuestState_03 questState_03;
    [Header("퀘스트 이미지")]
    public Sprite Image_03;
    [Header("퀘스트 이름")]
    public string Name_03;
    [Header("퀘스트 내용")]
    public string Content_03;
    [Header("잡아야하는 몬스터이름")]
    public string MutantName_03;
    public string bossName_03;
    [Header("잡은 횟수")]
    public int killCount_03_01;
    public int killCount_03_02;
    [Header("잡아야 하는 횟수")]
    public int clearCount_03_01;
    public int clearCount_03_02;
    [Header("퀘스트 보상")]
    public Sprite rewardImage_01_03;
    public Sprite rewardImage_02_03;
    public int exp_03;
    public int gold_03;
    [Header("퀘스트 위치")]
    public int Idx_03;
}
