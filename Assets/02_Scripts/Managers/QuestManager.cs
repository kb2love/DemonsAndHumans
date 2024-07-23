using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NPCDialogue;

public class QuestManager : MonoBehaviour
{
    // Singleton 인스턴스
    public static QuestManager questInst;

    // 퀘스트 데이터 변수들
    [SerializeField] private QuestData01 questData01;
    [SerializeField] private QuestData02 questData02;
    [SerializeField] private QuestData03 questData03;
    [SerializeField] private QuestData04 questData04;

    // UI 요소들
    [SerializeField] private RectTransform content;

    private Vector2 plusContent;
    private List<Transform> questList = new List<Transform>();

    private void Awake()
    {
        questInst = this; // Singleton 인스턴스 설정
    }

    private void Start()
    {
        plusContent = content.sizeDelta; // content의 초기 사이즈 저장

        // content의 자식들을 questList에 추가
        for (int i = 1; i < content.childCount; i++)
        {
            questList.Add(content.GetChild(i));
        }
    }

    // 퀘스트를 검색하여 퀘스트 목록에 추가
    public void QuestSearch()
    {
        if (questData01.questState == QuestState_01.QuestTake)
        {
            AddQuest(ref questData01.Idx, questData01.Image, questData01.Name, questData01.content, questData01.exp.ToString() + " Exp", null, null, null, 0, 0, ref questData01.questState, QuestState_01.QuestTake);
        }
        else if (questData01.questState == QuestState_01.QuestClear)
        {
            AddQuest(ref questData01.Idx, questData01.Image, questData01.Name, questData01.content, questData01.exp.ToString() + " Exp", null, null, null, 0, 0, ref questData01.questState, QuestState_01.QuestClear);
        }
        if (questData02.questState == QuestState_01.QuestTake)
        {
            AddQuest(ref questData02.Idx_01, questData02.Image_01, questData02.Name_01, questData02.Content_01, questData02.gold.ToString() + " Gold, " + questData02.exp_01.ToString() + " Exp", null, null, null,
                questData02.killCount, questData02.clearCount, ref questData02.questState, QuestState_01.QuestTake);
        }
        else if (questData02.questState == QuestState_01.QuestClear)
        {
            AddQuest(ref questData02.Idx_01, questData02.Image_01, questData02.Name_01, questData02.Content_01, questData02.gold.ToString() + " Gold, " + questData02.exp_01.ToString() + " Exp", null, null, null,
                questData02.killCount, questData02.clearCount, ref questData02.questState, QuestState_01.QuestClear);
        }
        else if (questData02.questState_02 == QuestState_02.QuestTake)
        {
            AddQuest_02(ref questData02.Idx_02, questData02.Image_02, questData02.Name_02, questData02.Content_02, questData02.exp_02.ToString() + " Exp", null, null, questData02.MutantName, null, 0, 0, ref questData02.questState_02, QuestState_02.QuestTake);
        }
        else if (questData02.questState_02 == QuestState_02.QuestClear) { AddQuest_02(ref questData02.Idx_02, questData02.Image_02, questData02.Name_02, questData02.Content_02, questData02.exp_02.ToString() + " Exp", null, null, questData02.MutantName, null, 0, 0, ref questData02.questState_02, QuestState_02.QuestClear); }
        if (questData03.questState == QuestState_01.QuestTake) { AddQuest(ref questData03.Idx, questData03.Image, questData03.Name, questData03.Content, questData03.exp.ToString() + " Exp", null, null, null, 0, 0, ref questData03.questState, QuestState_01.QuestTake); }
        else if(questData03.questState == QuestState_01.QuestClear) { AddQuest(ref questData03.Idx, questData03.Image, questData03.Name, questData03.Content, questData03.exp.ToString() + " Exp", null, null, null, 0, 0, ref questData03.questState, QuestState_01.QuestClear); }

        if (questData04.questState_01 == QuestState_01.QuestTake)
        {
            AddQuest(ref questData04.Idx_01, questData04.Image_01, questData04.Name_01, questData04.Content_01, questData04.gold_01.ToString() + " Gold, " + questData04.exp_01.ToString() + " Exp",
                questData04.rewardImage_01_01, questData04.rewardImage_01_02, questData04.MutantName_01, questData04.killCount_01, questData04.clearCount_02_01, ref questData04.questState_01, QuestState_01.QuestTake);
        }
        else if(questData04.questState_01 == QuestState_01.QuestClear)
        {
            AddQuest(ref questData04.Idx_01, questData04.Image_01, questData04.Name_01, questData04.Content_01, questData04.gold_01.ToString() + " Gold, " + questData04.exp_01.ToString() + " Exp",
                questData04.rewardImage_01_01, questData04.rewardImage_01_02, questData04.MutantName_01, questData04.killCount_01, questData04.clearCount_02_01, ref questData04.questState_01, QuestState_01.QuestClear);
        }
        else if (questData04.questState_02 == QuestState_02.QuestTake)
        {
            AddQuest_02(ref questData04.Idx_02, questData04.Image_02, questData04.Name_02, questData04.Content_02, questData04.gold_02.ToString() + " Gold, " + questData04.exp_01.ToString() + " Exp", questData04.rewardImage_02_01,
                questData04.rewardImage_02_02, questData04.MutantName_02, questData04.bossName_02, questData04.killCount_02_01, questData04.clearCount_02_01, ref questData04.questState_02, QuestState_02.QuestTake, questData04.killCount_02_02, questData04.clearCount_02_02);
        }
        else if(questData04.questState_02 == QuestState_02.QuestClear)
        {
            AddQuest_02(ref questData04.Idx_02, questData04.Image_02, questData04.Name_02, questData04.Content_02, questData04.gold_02.ToString() + " Gold, " + questData04.exp_01.ToString() + " Exp", questData04.rewardImage_02_01,
                questData04.rewardImage_02_02, questData04.MutantName_02, questData04.bossName_02, questData04.killCount_02_01, questData04.clearCount_02_01, ref questData04.questState_02, QuestState_02.QuestClear, questData04.killCount_02_02, questData04.clearCount_02_02);
        }
        else if (questData04.questState_03 == QuestState_03.QuestTake)
        {
            AddQuest_03(QuestState_03.QuestTake);
        }
        else if(questData04.questState_03 != QuestState_03.QuestClear) { AddQuest_03(QuestState_03.QuestClear); }
    }

    // 모든 퀘스트 상태를 초기화
    public void QuestReset()
    {
        questData01.questState = QuestState_01.QuestHave;
        questData02.questState = QuestState_01.QuestHave;
        questData03.questState = QuestState_01.QuestHave;
        questData04.questState_01 = QuestState_01.QuestHave;
    }

    // 퀘스트를 추가하는 메소드 (QuestState_01)
    public void AddQuest(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, Sprite questReward01, Sprite questReward02, string mutantName, int questKillCount, int questClearCount, ref QuestState_01 questState, QuestState_01 newState)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];

        SetQuestDetails(questTransform, questSprite, questName, questContent, questRewards, questReward01, questReward02, mutantName, null, questKillCount, questClearCount);

        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
        questTransform.gameObject.SetActive(true);
    }

    // 퀘스트를 추가하는 메소드 (QuestState_02)
    public void AddQuest_02(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, Sprite questReward01, Sprite questReward02, string mutantName, string bossName, int questKillCount, int questClearCount, ref QuestState_02 questState, QuestState_02 newState, int questKillCount_02 = 0, int questClearCount_02 = 0)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];

        SetQuestDetails(questTransform, questSprite, questName, questContent, questRewards, questReward01, questReward02, mutantName, bossName, questKillCount, questClearCount, questKillCount_02, questClearCount_02);

        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
        questTransform.gameObject.SetActive(true);
    }

    // 퀘스트를 추가하는 메소드 (QuestState_03)
    public void AddQuest_03(QuestState_03 questState_03)
    {
        questData04.Idx_03 = GetAvailableQuestIdx();
        Transform questTransform = questList[questData04.Idx_03];

        SetQuestDetails(questTransform, questData04.Image_03, questData04.Name_03, questData04.Content_03, null, questData04.rewardImage_01_03, questData04.rewardImage_02_03, null, null, 0, 0);

        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questData04.questState_03 = questState_03;
        questTransform.gameObject.SetActive(true);
    }

    // 사용 가능한 퀘스트 인덱스를 반환
    public int GetAvailableQuestIdx()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (!questList[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1; // 사용 가능한 인덱스가 없을 경우 -1 반환
    }

    // 퀘스트 진행 상태 업데이트
    public void UpdateQuestProgress(int questIdx, int questGoalCount, int questClearCount, int questGoalCount_02 = 0, int questClearCount_02 = 0)
    {
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = $"{questGoalCount} / {questClearCount}";
        if (questGoalCount_02 > 0 || questClearCount_02 > 0)
            questList[questIdx].GetChild(0).GetChild(2).GetComponent<Text>().text = $"{questGoalCount_02} / {questClearCount_02}";
    }

    // 퀘스트 완료 처리 (QuestState_01)
    public void CompleteQuest(int questIdx, ref QuestState_01 questState)
    {
        questList[questIdx].gameObject.SetActive(false);
        questState = QuestState_01.None;
    }

    // 퀘스트 완료 처리 (QuestState_02)
    public void CompleteQuest(int questIdx, ref QuestState_02 questState)
    {
        questList[questIdx].gameObject.SetActive(false);
        questState = QuestState_02.None;
    }

    public void CompleteQuest(int questIdx, ref QuestState_03 questState)
    {
        questList[questIdx].gameObject.SetActive(false);
        questState = QuestState_03.None;
    }
    // 퀘스트의 킬 카운트 업데이트
    public void UpdateKillCount(int questIdx, bool boss = false)
    {
        switch (questIdx)
        {
            case 0:
                IncrementKillCount(ref questData02.killCount, questData02.clearCount, questData02.Idx_01, ref questData02.questState);
                break;
            case 1:
                IncrementKillCount(ref questData04.killCount_01, questData04.clearCount_01, questData04.Idx_01, ref questData04.questState_01);
                break;
            case 2:
                IncrementBossKillCount(ref questData04.killCount_02_01, questData04.clearCount_02_01, ref questData04.killCount_02_02, questData04.clearCount_02_02, questData04.Idx_02, ref questData04.questState_02, boss);
                break;
            case 3:
                IncrementBossKillCount(ref questData04.killCount_03_01, questData04.clearCount_03_01, ref questData04.killCount_03_02, questData04.clearCount_03_02, questData04.Idx_02, ref questData04.questState_02, boss);
                break;

        }
    }

    // 퀘스트의 킬 카운트를 증가시키는 메소드 (QuestState_01)
    private void IncrementKillCount(ref int killCount, int clearCount, int idx, ref QuestState_01 state)
    {
        if (state == QuestState_01.QuestTake)
        {
            killCount++;
            UpdateQuestProgress(idx, killCount, clearCount);

            if (killCount >= clearCount)
            {
                state = QuestState_01.QuestClear;
            }
        }
    }

    // 퀘스트의 킬 카운트를 증가시키는 메소드 (QuestState_02) 보스가 있는경우
    private void IncrementBossKillCount(ref int killCount, int clearCount, ref int killCount_02, int clearCount_02, int idx, ref QuestState_02 state, bool boss = false)
    {
        if (state == QuestState_02.QuestTake)
        {
            if (!boss) killCount++;
            else killCount_02++;

            UpdateQuestProgress(idx, killCount, clearCount, killCount_02, clearCount_02);

            if (killCount >= clearCount && killCount_02 >= clearCount_02)
            {
                state = QuestState_02.QuestClear;
            }
        }
    }

    // 퀘스트의 상세 정보를 설정하는 메소드
    private void SetQuestDetails(Transform questTransform, Sprite questSprite, string questName, string questContent, string questRewards, Sprite questReward01, Sprite questReward02, string mutantName, string bossName, int questKillCount, int questClearCount, int questKillCount_02 = 0, int questClearCount_02 = 0)
    {
        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        if (questRewards != null)
            questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;

        SetQuestRewards(questBox.GetChild(3), questReward01, questReward02);

        if (questKillCount > 0 || questClearCount > 0)
        {
            questBox.GetChild(1).GetComponent<Text>().text = $"{questKillCount} / {questClearCount}";
            questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
            questBox.GetChild(1).gameObject.SetActive(true);
        }
        if (questKillCount_02 > 0 || questClearCount_02 > 0)
        {
            questBox.GetChild(2).GetComponent<Text>().text = $"{questKillCount_02} / {questClearCount_02}";
            questBox.GetChild(2).GetChild(0).GetComponent<Text>().text = bossName;
            questBox.GetChild(2).gameObject.SetActive(true);
        }
    }

    // 퀘스트의 보상을 설정하는 메소드
    private void SetQuestRewards(Transform rewardsTransform, Sprite questReward01, Sprite questReward02)
    {
        if (questReward01 != null && questReward02 == null)
        {
            rewardsTransform.GetChild(0).GetComponent<Image>().sprite = questReward01;
        }
        else if (questReward01 == null && questReward02 != null)
        {
            rewardsTransform.GetChild(0).GetComponent<Image>().sprite = questReward02;
            rewardsTransform.GetChild(0).gameObject.SetActive(true);
        }
        if (questReward01 != null && questReward02 != null)
        {
            rewardsTransform.GetChild(0).GetComponent<Image>().sprite = questReward01;
            rewardsTransform.GetChild(1).GetComponent<Image>().sprite = questReward02;
            rewardsTransform.GetChild(0).gameObject.SetActive(true);
            rewardsTransform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
