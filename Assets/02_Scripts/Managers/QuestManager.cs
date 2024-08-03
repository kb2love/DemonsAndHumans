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
    [SerializeField] private MutantKillerQuest mutantKillerQuest_04;

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
        QuestCheck_01(ref leaderQuest.Idx, leaderQuest.Image, leaderQuest.Name, leaderQuest.Content, leaderQuest.Exp.ToString() + " Exp", ref leaderQuest.questState);  // 리더 퀘스트
        QuestCheck_02(ref mariaQuest_01.Idx, mariaQuest_01.Image, mariaQuest_01.Name, mariaQuest_01.Content, mariaQuest_01.Gold.ToString() + " Gold, " + mariaQuest_01.Exp.ToString() + " Exp", mariaQuest_01.MutantName,
          ref mariaQuest_01.KillCount, mariaQuest_01.ClearCount, ref mariaQuest_01.questState); //마리아 첫번째 퀘스트
        QuestCheck_01(ref mariaQuest_02.Idx, mariaQuest_02.Image, mariaQuest_02.Name, mariaQuest_02.Content, mariaQuest_02.Exp.ToString() + " Exp", ref mariaQuest_02.questState);  // 마리아 두번째 퀘스트
        QuestCheck_01(ref paladinQuest.Idx, paladinQuest.Image, paladinQuest.Name, paladinQuest.Content, paladinQuest.Exp.ToString() + " Exp", ref paladinQuest.questState);    //팔라딘 퀘스트
        QuestCheck_02(ref mutantKillerQuest_01.Idx, mutantKillerQuest_01.Image, mutantKillerQuest_01.Name, mutantKillerQuest_01.Content, mutantKillerQuest_01.Gold.ToString() + " Gold, " +
            mutantKillerQuest_01.Exp.ToString() + " Exp", mutantKillerQuest_01.MutantName, ref mutantKillerQuest_01.KillCount, mutantKillerQuest_01.ClearCount, ref mutantKillerQuest_01.questState);//마족사냥대대장 첫번째 퀘스트
        QuestCheck_03(ref mutantKillerQuest_02.Idx, mutantKillerQuest_02.Image, mutantKillerQuest_02.Name, mutantKillerQuest_02.Content, mutantKillerQuest_02.Gold.ToString() +
            " Gold, " + mutantKillerQuest_02.Exp.ToString() + " Exp", mutantKillerQuest_02.RewardImage_01, mutantKillerQuest_02.RewardImage_02, mutantKillerQuest_02.MutantName,
            mutantKillerQuest_02.BossName, ref mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, ref mutantKillerQuest_02.BossKillCount, mutantKillerQuest_02.BossClearCount,
            ref mutantKillerQuest_02.questState);   //마족사냥대대장 두번째 퀘스트
        QuestCheck_03(ref mutantKillerQuest_03.Idx, mutantKillerQuest_03.Image, mutantKillerQuest_03.Name, mutantKillerQuest_03.Content, mutantKillerQuest_03.Gold.ToString() +
            " Gold, " + mutantKillerQuest_03.Exp.ToString() + " Exp", mutantKillerQuest_03.RewardImage_01, mutantKillerQuest_03.RewardImage_02, mutantKillerQuest_03.MutantName,
            mutantKillerQuest_03.BossName, ref mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, ref mutantKillerQuest_03.BossKillCount, mutantKillerQuest_03.BossClearCount,
            ref mutantKillerQuest_03.questState);   //마족사냥대대장 세번째 퀘스트
        QuestCheck_03(ref mutantKillerQuest_04.Idx, mutantKillerQuest_04.Image, mutantKillerQuest_04.Name, mutantKillerQuest_04.Content, mutantKillerQuest_04.Gold.ToString() +
           " Gold, " + mutantKillerQuest_04.Exp.ToString() + " Exp", mutantKillerQuest_04.RewardImage_01, mutantKillerQuest_04.RewardImage_02, mutantKillerQuest_04.MutantName,
           mutantKillerQuest_04.BossName, ref mutantKillerQuest_04.KillCount, mutantKillerQuest_04.ClearCount, ref mutantKillerQuest_04.BossKillCount, mutantKillerQuest_04.BossClearCount,
           ref mutantKillerQuest_03.questState);    //마족사냥대대장 네번째 퀘스트
    }
    void QuestCheck_01(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, ref QuestState questState)
    {
        QuestDataJson questDataJson = DataManager.dataInst.FindQuest(questState, questName);
        if (questDataJson != null)
        {
            if (questDataJson.questState == QuestState.QuestTake)
            {
                QuestCheckMethod_01(out questIdx, questSprite, questName, questContent, questRewards, questState);
                questState = QuestState.QuestTake;
            }
            else if (questDataJson.questState == QuestState.QuestClear)
            {
                QuestCheckMethod_01(out questIdx, questSprite, questName, questContent, questRewards, questState);
                questState = QuestState.QuestClear;
            }
        }
    }
    void QuestCheck_02(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, string mutantName, ref int mutantKillCount, int mutantClearCount, ref QuestState questState)
    {
        QuestDataJson questDataJson = DataManager.dataInst.FindQuest(questState, questName);
        if (questDataJson != null)
        {
            if (questDataJson.questState == QuestState.QuestTake)
            {
                QuestCheckMethod_02(out questIdx, questSprite, questName, questContent, questRewards, mutantName, out mutantKillCount, mutantClearCount, questState, questDataJson);
                questState = QuestState.QuestTake;
            }
            else if (questDataJson.questState == QuestState.QuestClear)
            {
                QuestCheckMethod_02(out questIdx, questSprite, questName, questContent, questRewards, mutantName, out mutantKillCount, mutantClearCount, questState, questDataJson);
                questState = QuestState.QuestClear;
            }
        }
    }

    private void QuestCheckMethod_01(out int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, QuestState questState)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        DataManager.dataInst.SaveQuestData(questState, questName);
        questTransform.gameObject.SetActive(true);
    }

    private void QuestCheckMethod_02(out int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, string mutantName, out int mutantKillCount, int mutantClearCount,
        QuestState questState, QuestDataJson questDataJson)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        mutantKillCount = questDataJson.KillCount;
        questBox.GetChild(1).GetComponent<Text>().text = $"{mutantKillCount} / {mutantClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        DataManager.dataInst.SaveQuestData(questState, questName);
        questTransform.gameObject.SetActive(true);
    }

    void QuestCheck_03(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, Sprite questReward01, Sprite questReward02, string mutantName, string bossName,
        ref int mutantKillCount, int mutantClearCount, ref int bossKillCount, int bossClearCount, ref QuestState questState)
    {
        QuestDataJson questDataJson = DataManager.dataInst.FindQuest(questState, questName);
        if (questDataJson != null)
        {
            if (questDataJson.questState == QuestState.QuestTake)
            {
                QuestCheckMethod_03(out questIdx, questSprite, questName, questContent, questRewards, questReward01, questReward02, mutantName, bossName, out mutantKillCount, mutantClearCount, out bossKillCount, bossClearCount, questState, questDataJson);
                questState = QuestState.QuestTake;
            }
            else if (questDataJson.questState == QuestState.QuestClear)
            {
                QuestCheckMethod_03(out questIdx, questSprite, questName, questContent, questRewards, questReward01, questReward02, mutantName, bossName, out mutantKillCount, mutantClearCount, out bossKillCount, bossClearCount, questState, questDataJson);
                questState = QuestState.QuestClear;
            }

        }
    }

    private void QuestCheckMethod_03(out int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, Sprite questReward01, Sprite questReward02, string mutantName,
        string bossName, out int mutantKillCount, int mutantClearCount, out int bossKillCount, int bossClearCount, QuestState questState, QuestDataJson questDataJson)
    {
        questIdx = GetAvailableQuestIdx();
        Transform questTransform = questList[questIdx];

        questTransform.GetChild(2).GetComponent<Image>().sprite = questSprite;
        questTransform.GetChild(1).GetComponent<Text>().text = questName;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = questContent;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        questBox.GetChild(3).GetChild(0).GetComponent<Image>().sprite = questReward01;
        questBox.GetChild(3).GetChild(1).GetComponent<Image>().sprite = questReward02;
        mutantKillCount = questDataJson.KillCount;
        bossKillCount = questDataJson.bossKillCount;
        questBox.GetChild(1).GetComponent<Text>().text = $"{mutantKillCount} / {mutantClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        questBox.GetChild(2).GetComponent<Text>().text = $"{bossKillCount} / {bossClearCount}";
        questBox.GetChild(2).GetChild(0).GetComponent<Text>().text = bossName;
        questBox.GetChild(2).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        DataManager.dataInst.SaveQuestData(questState, questName);
        questTransform.gameObject.SetActive(true);
    }

    // 모든 퀘스트 상태를 초기화
    public void QuestReset()
    {
        leaderQuest.questState = QuestState.QuestHave;
        mariaQuest_01.questState = QuestState.QuestHave;
        mariaQuest_02.questState = QuestState.QuestHave;
        paladinQuest.questState = QuestState.QuestHave;
        mutantKillerQuest_01.questState = QuestState.QuestHave;
        mutantKillerQuest_02.questState = QuestState.QuestHave;
        mutantKillerQuest_03.questState = QuestState.QuestHave;
        mutantKillerQuest_04.questState = QuestState.QuestNormal;
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
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
        DataManager.dataInst.SaveQuestData(questState, questName);
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
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        questBox.GetChild(3).gameObject.SetActive(true);
        questBox.GetChild(1).GetComponent<Text>().text = $"{mutantKillCount} / {mutantClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
        DataManager.dataInst.SaveQuestData(questState, questName);
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
        questBox.GetChild(3).GetChild(0).GetComponent<Image>().sprite = questReward01;
        questBox.GetChild(3).GetChild(1).GetComponent<Image>().sprite = questReward02;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = questRewards;
        questBox.GetChild(3).gameObject.SetActive(true);
        questBox.GetChild(1).GetComponent<Text>().text = $"{mutatntKillCount} / {mutantClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        questBox.GetChild(2).GetComponent<Text>().text = $"{bossKillCount} / {bossClearCount}";
        questBox.GetChild(2).GetChild(0).GetComponent<Text>().text = bossName;
        questBox.GetChild(2).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questState = newState;
        DataManager.dataInst.SaveQuestData(questState, questName);
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


    // 퀘스트 완료 처리 (QuestState)
    public void CompleteQuest(int questIdx, ref QuestState questState, int exp, string questName)
    {
        questList[questIdx].gameObject.SetActive(false);
        questState = QuestState.None;
        DataManager.dataInst.SaveQuestData(questState, questName);
        GameManager.GM.ExpUp(exp);
    }
    public void CompleteQuest(int questIdx, ref QuestState questState, int exp, int gold, string questName)
    {
        Transform quest = questList[questIdx].GetChild(0);
        quest.GetChild(1).gameObject.SetActive(false);
        quest.GetChild(2).gameObject.SetActive(false);
        quest.GetChild(3).GetChild(0).gameObject.SetActive(false);
        quest.GetChild(3).GetChild(1).gameObject.SetActive(false);
        quest.GetChild(3).gameObject.SetActive(false);
        questList[questIdx].gameObject.SetActive(false);
        questState = QuestState.None;
        DataManager.dataInst.SaveQuestData(questState, questName);
        GameManager.GM.ExpUp(exp);
        ItemManager.itemInst.GoldPlus(gold);
    }

    // 퀘스트의 킬 카운트 업데이트
    public void UpdateKillCount(int questIdx, bool boss = false)
    {
        switch (questIdx)
        {
            case 0:
                IncrementKillCount(ref mariaQuest_01.KillCount, mariaQuest_01.ClearCount, mariaQuest_01.Idx, ref mariaQuest_01.questState, mariaQuest_01.Name);
                break;
            case 1:
                IncrementKillCount(ref mutantKillerQuest_01.KillCount, mutantKillerQuest_01.ClearCount, mutantKillerQuest_01.Idx, ref mutantKillerQuest_01.questState, mutantKillerQuest_01.Name);
                break;
            case 2:
                IncrementBossKillCount(ref mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, ref mutantKillerQuest_02.BossKillCount, mutantKillerQuest_02.BossClearCount, mutantKillerQuest_02.Idx, ref mutantKillerQuest_02.questState, mutantKillerQuest_02.Name, boss);
                break;
            case 3:
                IncrementBossKillCount(ref mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, ref mutantKillerQuest_03.BossKillCount, mutantKillerQuest_03.BossClearCount, mutantKillerQuest_03.Idx, ref mutantKillerQuest_03.questState, mutantKillerQuest_03.Name, boss);
                break;
            case 4:
                IncrementBossKillCount(ref mutantKillerQuest_04.KillCount, mutantKillerQuest_04.ClearCount, ref mutantKillerQuest_04.BossKillCount, mutantKillerQuest_04.BossClearCount, mutantKillerQuest_04.Idx, ref mutantKillerQuest_04.questState, mutantKillerQuest_04.Name, boss);
                break;

        }
    }

    // 퀘스트의 킬 카운트를 증가시키는 메소드 (QuestState)
    private void IncrementKillCount(ref int killCount, int clearCount, int idx, ref QuestState state, string questName)
    {
        if (state == QuestState.QuestTake)
        {
            killCount++;
            UpdateQuestProgress(idx, killCount, clearCount);
            DataManager.dataInst.SaveQuestData(state, questName);

            if (killCount >= clearCount)
            {
                state = QuestState.QuestClear;
            }
        }
    }

    private void IncrementBossKillCount(ref int killCount, int clearCount, ref int bossKillCount, int bossClearCount, int idx, ref QuestState state, string questName, bool boss = false)
    {
        if (state == QuestState.QuestTake)
        {
            if (!boss) killCount++;
            else bossKillCount++;

            UpdateQuestProgress(idx, killCount, clearCount, bossKillCount, bossClearCount);
            DataManager.dataInst.SaveQuestData(state, questName);

            if (killCount >= clearCount && bossKillCount >= bossClearCount)
            {
                state = QuestState.QuestClear;
            }
        }
    }
    // 퀘스트 진행 상태 업데이트
    public void UpdateQuestProgress(int questIdx, int questGoalCount, int questClearCount)
    {
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = $"{questGoalCount} / {questClearCount}";
    }
    public void UpdateQuestProgress(int questIdx, int mutantKillCount, int mutantClearCount, int bossKillCount, int bossClearCount)
    {
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = $"{mutantKillCount} / {mutantClearCount}";
        questList[questIdx].GetChild(0).GetChild(2).GetComponent<Text>().text = $"{bossKillCount} / {bossClearCount}";
    }
    public void PlayerLevel30() { mutantKillerQuest_04.questState = QuestState.QuestHave; }
}
