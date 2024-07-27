using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    // Singleton 인스턴스
    public static QuestManager questInst;

    // 퀘스트 데이터 변수들
    [SerializeField] private DialougeQuest leaderQuest;
    [SerializeField] private DialougeQuest paladinQuest;
    [SerializeField] private MutantKillQuest mariaQuest_01;
    [SerializeField] private DialougeQuest mariaQuest_02;
    [SerializeField] private MutantKillQuest mutantKillerQuest_01;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_02;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_03;

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
        if (leaderQuest.questState == QuestState.QuestTake)
        {
            AddQuest_01(ref leaderQuest.Idx, leaderQuest.Image, leaderQuest.Name, leaderQuest.Content, leaderQuest.Exp.ToString() + " Exp", ref leaderQuest.questState, QuestState.QuestTake);
        }
        else if (leaderQuest.questState == QuestState.QuestClear)
        {
            AddQuest_01(ref leaderQuest.Idx, leaderQuest.Image, leaderQuest.Name, leaderQuest.Content, leaderQuest.Exp.ToString() + " Exp", ref leaderQuest.questState, QuestState.QuestClear);
        }
        if (mariaQuest_01.questState == QuestState.QuestTake)
        {// 마리아의 첫번째 퀘스트
            AddQuest_02(ref mariaQuest_01.Idx, mariaQuest_01.Image, mariaQuest_01.Name, mariaQuest_01.Content, mariaQuest_01.Gold.ToString() + " Gold, " + mariaQuest_01.Exp.ToString() + " Exp", mariaQuest_01.MutantName,
                mariaQuest_01.KillCount, mariaQuest_01.ClearCount, ref mariaQuest_01.questState, QuestState.QuestTake);
        }
        else if (mariaQuest_01.questState == QuestState.QuestClear)
        {// 마리아의 첫번쨰 퀘스트 클리어
            AddQuest_02(ref mariaQuest_01.Idx, mariaQuest_01.Image, mariaQuest_01.Name, mariaQuest_01.Content, mariaQuest_01.Gold.ToString() + " Gold, " + mariaQuest_01.Exp.ToString() + " Exp", mariaQuest_01.MutantName,
                mariaQuest_01.KillCount, mariaQuest_01.ClearCount, ref mariaQuest_01.questState, QuestState.QuestClear);
        }
        else if (mariaQuest_02.questState == QuestState.QuestTake)
        {// 마리아의 두번째 퀘스트
            AddQuest_01(ref mariaQuest_02.Idx, mariaQuest_02.Image, mariaQuest_02.Name, mariaQuest_02.Content, mariaQuest_02.Exp.ToString() + " Exp", ref mariaQuest_02.questState, QuestState.QuestTake);
        }
        else if (mariaQuest_02.questState == QuestState.QuestClear)
        {
            AddQuest_01(ref mariaQuest_02.Idx, mariaQuest_02.Image, mariaQuest_02.Name, mariaQuest_02.Content, mariaQuest_02.Exp.ToString() + " Exp", ref mariaQuest_02.questState, QuestState.QuestClear);
        }
        //팔라딘 퀘스트
        if (paladinQuest.questState == QuestState.QuestTake) { AddQuest_01(ref paladinQuest.Idx, paladinQuest.Image, paladinQuest.Name, paladinQuest.Content, paladinQuest.Exp.ToString() + " Exp", ref paladinQuest.questState, QuestState.QuestTake); }
        else if (paladinQuest.questState == QuestState.QuestClear) { AddQuest_01(ref paladinQuest.Idx, paladinQuest.Image, paladinQuest.Name, paladinQuest.Content, paladinQuest.Exp.ToString() + " Exp", ref paladinQuest.questState, QuestState.QuestClear); }

        if (mutantKillerQuest_01.questState == QuestState.QuestTake)
        {// 첫번째 퀘스트 마족병사잡기
            AddQuest_02(ref mutantKillerQuest_01.Idx, mutantKillerQuest_01.Image, mutantKillerQuest_01.Name, mutantKillerQuest_01.Content, mutantKillerQuest_01.Gold.ToString() + " Gold, " + 
                mutantKillerQuest_01.Exp.ToString() + " Exp", mutantKillerQuest_01.MutantName, 
                mutantKillerQuest_01.KillCount, mutantKillerQuest_01.ClearCount, ref mutantKillerQuest_01.questState, QuestState.QuestTake);
        }
        else if (mutantKillerQuest_01.questState == QuestState.QuestClear)
        {
            AddQuest_02(ref mutantKillerQuest_01.Idx, mutantKillerQuest_01.Image, mutantKillerQuest_01.Name, mutantKillerQuest_01.Content, mutantKillerQuest_01.Gold.ToString() + " Gold, " +
                mutantKillerQuest_01.Exp.ToString() + " Exp", mutantKillerQuest_01.MutantName,
                mutantKillerQuest_01.KillCount, mutantKillerQuest_01.ClearCount, ref mutantKillerQuest_01.questState, QuestState.QuestTake);
        }
        else if (mutantKillerQuest_02.questState == QuestState.QuestTake)
        {// 두번째 퀘스트 하급마족 잡기
            AddQuest_03(ref mutantKillerQuest_02.Idx, mutantKillerQuest_02.Image, mutantKillerQuest_02.Name, mutantKillerQuest_02.Content, mutantKillerQuest_02.Gold.ToString() + 
                " Gold, " + mutantKillerQuest_02.Exp.ToString() + " Exp", mutantKillerQuest_02.RewardImage_01, mutantKillerQuest_02.RewardImage_02, mutantKillerQuest_02.MutantName,
                mutantKillerQuest_02.BossName, mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, mutantKillerQuest_02.BossKillCount, mutantKillerQuest_02.BossClearCount, 
                ref mutantKillerQuest_02.questState, QuestState.QuestTake);
        }
        else if (mutantKillerQuest_02.questState == QuestState.QuestClear)
        {
            AddQuest_03(ref mutantKillerQuest_02.Idx, mutantKillerQuest_02.Image, mutantKillerQuest_02.Name, mutantKillerQuest_02.Content, mutantKillerQuest_02.Gold.ToString() +
                " Gold, " + mutantKillerQuest_02.Exp.ToString() + " Exp", mutantKillerQuest_02.RewardImage_01, mutantKillerQuest_02.RewardImage_02, mutantKillerQuest_02.MutantName,
                mutantKillerQuest_02.BossName, mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, mutantKillerQuest_02.BossKillCount, mutantKillerQuest_02.BossClearCount,
                ref mutantKillerQuest_02.questState, QuestState.QuestClear);
        }
        else if (mutantKillerQuest_03.questState == QuestState.QuestTake)
        {// 세번째 퀘스트 중급마족 잡기

            AddQuest_03(ref mutantKillerQuest_03.Idx, mutantKillerQuest_03.Image, mutantKillerQuest_03.Name, mutantKillerQuest_03.Content, mutantKillerQuest_03.Gold.ToString() +
                " Gold, " + mutantKillerQuest_03.Exp.ToString() + " Exp", mutantKillerQuest_03.RewardImage_01, mutantKillerQuest_03.RewardImage_02, mutantKillerQuest_03.MutantName,
                mutantKillerQuest_03.BossName, mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, mutantKillerQuest_03.BossKillCount, mutantKillerQuest_03.BossClearCount,
                ref mutantKillerQuest_03.questState, QuestState.QuestTake);
        }
        else if (mutantKillerQuest_03.questState == QuestState.QuestClear) {
            AddQuest_03(ref mutantKillerQuest_03.Idx, mutantKillerQuest_03.Image, mutantKillerQuest_03.Name, mutantKillerQuest_03.Content, mutantKillerQuest_03.Gold.ToString() +
                " Gold, " + mutantKillerQuest_03.Exp.ToString() + " Exp", mutantKillerQuest_03.RewardImage_01, mutantKillerQuest_03.RewardImage_02, mutantKillerQuest_03.MutantName,
                mutantKillerQuest_03.BossName, mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, mutantKillerQuest_03.BossKillCount, mutantKillerQuest_03.BossClearCount,
                ref mutantKillerQuest_03.questState, QuestState.QuestClear);
        }
    }

    // 모든 퀘스트 상태를 초기화
    public void QuestReset()
    {
        leaderQuest.questState = QuestState.QuestHave;
        mariaQuest_01.questState = QuestState.QuestHave;
        paladinQuest.questState = QuestState.QuestHave;
        mutantKillerQuest_01.questState = QuestState.QuestHave;
        mutantKillerQuest_02.questState = QuestState.QuestHave;
        mutantKillerQuest_03.questState = QuestState.QuestHave;
    }

    // 퀘스트를 추가하는 메소드 (QuestState)
    public void AddQuest_01(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, ref QuestState questState, QuestState newState)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        if (questRewards != null)
            questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;

        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
        questTransform.gameObject.SetActive(true);
    }

    public void AddQuest_02(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, string mutantName, int mutantKillCount,
        int mutantClearCount, ref QuestState questState, QuestState newState)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        if (questRewards != null)
            questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        questBox.GetChild(1).GetComponent<Text>().text = $"{mutantKillCount} / {mutantClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
        questTransform.gameObject.SetActive(true);
    }
    public void AddQuest_03(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, Sprite questReward01, Sprite questReward02, string mutantName, string bossName, int mutatntKillCount,
        int mutantClearCount, int bossKillCount, int bossClearCount, ref QuestState questState, QuestState newState)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];

        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        if (questRewards != null)
            questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        questTransform.GetChild(0).GetComponent<Image>().sprite = questReward01;
        questTransform.GetChild(1).GetComponent<Image>().sprite = questReward02;
        questBox.GetChild(1).GetComponent<Text>().text = $"{mutatntKillCount} / {mutantClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        questBox.GetChild(2).GetComponent<Text>().text = $"{bossKillCount} / {bossClearCount}";
        questBox.GetChild(2).GetChild(0).GetComponent<Text>().text = bossName;
        questBox.GetChild(2).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
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

    // 퀘스트 완료 처리 (QuestState)
    public void CompleteQuest(int questIdx, ref QuestState questState)
    {
        questList[questIdx].gameObject.SetActive(false);
        questState = QuestState.None;
    }

    // 퀘스트의 킬 카운트 업데이트
    public void UpdateKillCount(int questIdx, bool boss = false)
    {
        switch (questIdx)
        {
            case 0:
                IncrementKillCount(ref mariaQuest_01.KillCount, mariaQuest_01.ClearCount, mariaQuest_01.Idx, ref mariaQuest_01.questState);
                break;
            case 1:
                IncrementKillCount(ref mutantKillerQuest_01.KillCount, mutantKillerQuest_01.ClearCount, mutantKillerQuest_01.Idx, ref mutantKillerQuest_01.questState);
                break;
            case 2:
                IncrementBossKillCount(ref mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, ref mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, mutantKillerQuest_02.Idx, ref mutantKillerQuest_02.questState, boss);
                break;
            case 3:
                IncrementBossKillCount(ref mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, ref mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, mutantKillerQuest_03.Idx, ref mutantKillerQuest_03.questState, boss);
                break;

        }
    }

    // 퀘스트의 킬 카운트를 증가시키는 메소드 (QuestState)
    private void IncrementKillCount(ref int killCount, int clearCount, int idx, ref QuestState state)
    {
        if (state == QuestState.QuestTake)
        {
            killCount++;
            UpdateQuestProgress(idx, killCount, clearCount);

            if (killCount >= clearCount)
            {
                state = QuestState.QuestClear;
            }
        }
    }

    private void IncrementBossKillCount(ref int killCount, int clearCount, ref int killCount_02, int clearCount_02, int idx, ref QuestState state, bool boss = false)
    {
        if (state == QuestState.QuestTake)
        {
            if (!boss) killCount++;
            else killCount_02++;

            UpdateQuestProgress(idx, killCount, clearCount, killCount_02, clearCount_02);

            if (killCount >= clearCount && killCount_02 >= clearCount_02)
            {
                state = QuestState.QuestClear;
            }
        }
    }

    // 퀘스트의 상세 정보를 설정하는 메소드
    /*    private void SetQuestDetails(Transform questTransform, Sprite questSprite, string questName, string questContent, string questRewards)
        {
            questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
            questTransform.GetChild(1).GetComponent<Text>().text = questName;

            Transform questBox = questTransform.GetChild(0);
            questBox.GetChild(0).GetComponent<Text>().text = questContent;
            if (questRewards != null)
                questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        }*/
    /*private void SetQuestDetails(Transform questTransform, Sprite questSprite, string questName, string questContent, string questRewards, string mutantName, int mutanttKillCount,
        int mutantClearCount)
    {
        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        if (questRewards != null)
            questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;

        if (mutanttKillCount > 0 || mutantClearCount > 0)
        {
            questBox.GetChild(1).GetComponent<Text>().text = $"{mutanttKillCount} / {mutantClearCount}";
            questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
            questBox.GetChild(1).gameObject.SetActive(true);
        }
    }*/
    /*private void SetQuestDetails(Transform questTransform, Sprite questSprite, string questName, string questContent, string questRewards, string mutantName, int mutanttKillCount,
        int mutantClearCount, int bosstKillCount_02, int bosstClearCount_02, string bossName, Sprite questReward01, Sprite questReward02)
    {
        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        if (questRewards != null)
            questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;

        questTransform.GetChild(0).GetComponent<Image>().sprite = questReward01;
        questTransform.GetChild(1).GetComponent<Image>().sprite = questReward02;

        if (mutanttKillCount > 0 || mutantClearCount > 0)
        {
            questBox.GetChild(1).GetComponent<Text>().text = $"{mutanttKillCount} / {mutantClearCount}";
            questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
            questBox.GetChild(1).gameObject.SetActive(true);
        }
        if (bosstKillCount_02 > 0 || bosstClearCount_02 > 0)
        {
            questBox.GetChild(2).GetComponent<Text>().text = $"{bosstKillCount_02} / {bosstClearCount_02}";
            questBox.GetChild(2).GetChild(0).GetComponent<Text>().text = bossName;
            questBox.GetChild(2).gameObject.SetActive(true);
        }
    }*/

}
