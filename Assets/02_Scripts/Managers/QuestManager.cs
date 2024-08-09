using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    // Singleton �ν��Ͻ�
    public static QuestManager questInst;

    // ����Ʈ ������ ������
    [SerializeField] private DialougeQuest leaderQuest;
    [SerializeField] private DialougeQuest paladinQuest;
    [SerializeField] private MutantKillQuest mariaQuest_01;
    [SerializeField] private DialougeQuest mariaQuest_02;
    [SerializeField] private MutantKillQuest mutantKillerQuest_01;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_02;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_03;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_04;
    // UI ��ҵ�
    [SerializeField] private RectTransform content;
    private Vector2 plusContent;
    private List<Transform> questList = new List<Transform>();
    DataManager dataManager;
    private void Awake()
    {
        questInst = this; // Singleton �ν��Ͻ� ����
    }

    private void Start()
    {
        dataManager = DataManager.dataInst;
        plusContent = content.sizeDelta; // content�� �ʱ� ������ ����
        // content�� �ڽĵ��� questList�� �߰�
        for (int i = 1; i < content.childCount; i++)
        {
            questList.Add(content.GetChild(i));
        }
    }

    // ����Ʈ�� �˻��Ͽ� ����Ʈ ��Ͽ� �߰�
    public void QuestSearch()
    {
        QuestCheck_01(ref leaderQuest, dataManager.leaderQuestDataJson);  // ���� ����Ʈ
        QuestCheck_02(ref mariaQuest_01, dataManager.mariaQuest_01DataJson); //������ ù��° ����Ʈ
        QuestCheck_01(ref mariaQuest_02, dataManager.mariaQuest_02DataJson);  // ������ �ι�° ����Ʈ
        QuestCheck_01(ref paladinQuest, dataManager.paladinQuestDataJson);    //�ȶ�� ����Ʈ
        QuestCheck_02(ref mutantKillerQuest_01, dataManager.mutantKillerQuest_01DataJson);//������ɴ���� ù��° ����Ʈ
        QuestCheck_03(ref mutantKillerQuest_02, dataManager.mutantKillerQuest_02DataJson);   //������ɴ���� �ι�° ����Ʈ
        QuestCheck_03(ref mutantKillerQuest_03, dataManager.mutantKillerQuest_03DataJson);   //������ɴ���� ����° ����Ʈ
        QuestCheck_03(ref mutantKillerQuest_04, dataManager.mutantKillerQuest_04DataJson);    //������ɴ���� �׹�° ����Ʈ
    }
    #region ����Ʈ�� �ִ��� Ȯ��
    void QuestCheck_01(ref DialougeQuest quest, QuestDataJson questDataJson)
    {
        if (questDataJson.questState == QuestState.QuestTake || questDataJson.questState == QuestState.QuestClear)
        {
            QuestCheckMethod_01(ref quest, questDataJson);
        }
    }
    void QuestCheck_02(ref MutantKillQuest killQuest, QuestDataJson questDataJson)
    {
        if (questDataJson.questState == QuestState.QuestTake || questDataJson.questState == QuestState.QuestClear)
        {
            QuestCheckMethod_02(ref killQuest, questDataJson);
        }

    }

    void QuestCheck_03(ref MutantKillerQuest mutantKillerQuest, QuestDataJson questDataJson)
    {
        if (questDataJson.questState == QuestState.QuestTake || questDataJson.questState == QuestState.QuestClear)
        {
            QuestCheckMethod_03(ref mutantKillerQuest, questDataJson);
        }

    }
    private void QuestCheckMethod_01(ref DialougeQuest quest, QuestDataJson questDataJson)
    {
        questDataJson.Idx = GetAvailableQuestIdx();
        Transform questTransform = questList[questDataJson.Idx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = quest.Image;
        questTransform.GetChild(1).GetComponent<Text>().text = quest.Name;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = quest.Content;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = quest.Exp.ToString() + " Exp";
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questTransform.gameObject.SetActive(true);
    }

    private void QuestCheckMethod_02(ref MutantKillQuest killQuest, QuestDataJson questDataJson)
    {
        questDataJson.Idx = GetAvailableQuestIdx();
        Transform questTransform = questList[questDataJson.Idx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = killQuest.Image;
        questTransform.GetChild(1).GetComponent<Text>().text = killQuest.Name;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = killQuest.Content;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = $"{killQuest.Gold} Gold {killQuest.Exp} Exp";
        questBox.GetChild(1).GetComponent<Text>().text = $"{questDataJson.KillCount} / {killQuest.ClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = killQuest.MutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questTransform.gameObject.SetActive(true);
    }


    private void QuestCheckMethod_03(ref MutantKillerQuest mutantKillerQuest, QuestDataJson questDataJson)
    {
        questDataJson.Idx = GetAvailableQuestIdx();
        Transform questTransform = questList[questDataJson.Idx];

        questTransform.GetChild(2).GetComponent<Image>().sprite = mutantKillerQuest.Image;
        questTransform.GetChild(1).GetComponent<Text>().text = mutantKillerQuest.Name;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = mutantKillerQuest.Content;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = $"{mutantKillerQuest.Gold} Gold {mutantKillerQuest.Exp} Exp";
        questBox.GetChild(3).GetChild(0).GetComponent<Image>().sprite = mutantKillerQuest.RewardImage_01;
        questBox.GetChild(3).GetChild(1).GetComponent<Image>().sprite = mutantKillerQuest.RewardImage_02;
        questBox.GetChild(3).GetChild(0).gameObject.SetActive(true);
        questBox.GetChild(3).GetChild(1).gameObject.SetActive(true);
        questBox.GetChild(1).GetComponent<Text>().text = $"{questDataJson.KillCount} / {mutantKillerQuest.ClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantKillerQuest.MutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        questBox.GetChild(2).GetComponent<Text>().text = $"{questDataJson.bossKillCount}  /  {mutantKillerQuest.BossClearCount}";
        questBox.GetChild(2).GetChild(0).GetComponent<Text>().text = mutantKillerQuest.BossName;
        questBox.GetChild(2).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questTransform.gameObject.SetActive(true);
    }
    #endregion
    // ��� ����Ʈ ���¸� �ʱ�ȭ
    public void QuestReset()
    {
        dataManager.leaderQuestDataJson.questState = QuestState.QuestHave;
        dataManager.mariaQuest_01DataJson.questState = QuestState.QuestHave;
        dataManager.mariaQuest_02DataJson.questState = QuestState.QuestHave;
        dataManager.paladinQuestDataJson.questState = QuestState.QuestHave;
        dataManager.mutantKillerQuest_01DataJson.questState = QuestState.QuestHave;
        dataManager.mutantKillerQuest_02DataJson.questState = QuestState.QuestHave;
        dataManager.mutantKillerQuest_03DataJson.questState = QuestState.QuestHave;
        dataManager.mutantKillerQuest_04DataJson.questState = QuestState.QuestNormal;
    }

    // ����Ʈ�� �߰��ϴ� �޼ҵ� (QuestState)
    public void AddQuest_01(ref DialougeQuest dialougeQuest, ref QuestDataJson questDataJson)
    {
        questDataJson.Idx = GetAvailableQuestIdx();
        Transform questTransform = questList[questDataJson.Idx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = dialougeQuest.Image;
        questTransform.GetChild(1).GetComponent<Text>().text = dialougeQuest.Name;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = dialougeQuest.Content;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = dialougeQuest.Exp.ToString() + " Exp";
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questDataJson.questState = QuestState.QuestTake;
        DataManager.dataInst.SaveQuestData(QuestState.QuestTake, dialougeQuest.Name, questDataJson.Idx);
        questTransform.gameObject.SetActive(true);
    }

    public void AddQuest_02(ref MutantKillQuest mutantKillQuest, ref QuestDataJson questDataJson)
    {
        questDataJson.Idx = GetAvailableQuestIdx();
        Transform questTransform = questList[questDataJson.Idx];
        questTransform.GetChild(2).GetComponent<Image>().sprite = mutantKillQuest.Image;
        questTransform.GetChild(1).GetComponent<Text>().text = mutantKillQuest.Name;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = mutantKillQuest.Content;
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = $"{mutantKillQuest.Gold} Gold {mutantKillQuest.Exp} Exp";
        questBox.GetChild(1).GetComponent<Text>().text = $"{questDataJson.KillCount} / {mutantKillQuest.ClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantKillQuest.MutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questDataJson.questState = QuestState.QuestTake;
        DataManager.dataInst.SaveQuestData(QuestState.QuestTake, mutantKillQuest.Name, questDataJson.Idx);
        questTransform.gameObject.SetActive(true);
    }
    public void AddQuest_03(ref MutantKillerQuest mutantKillerQuest, ref QuestDataJson questDataJson)
    {
        questDataJson.Idx = GetAvailableQuestIdx();
        Transform questTransform = questList[questDataJson.Idx];

        questTransform.GetChild(2).GetComponent<Image>().sprite = mutantKillerQuest.Image;
        questTransform.GetChild(1).GetComponent<Text>().text = mutantKillerQuest.Name;

        Transform questBox = questTransform.GetChild(0);
        questBox.GetChild(0).GetComponent<Text>().text = mutantKillerQuest.Content;
        questBox.GetChild(3).GetChild(0).GetComponent<Image>().sprite = mutantKillerQuest.RewardImage_01;
        questBox.GetChild(3).GetChild(1).GetComponent<Image>().sprite = mutantKillerQuest.RewardImage_02;
        questBox.GetChild(3).GetChild(0).gameObject.SetActive(true);
        questBox.GetChild(3).GetChild(1).gameObject.SetActive(true);
        questBox.GetChild(3).GetChild(2).GetComponent<Text>().text = $"{mutantKillerQuest.Gold} Gold {mutantKillerQuest.Exp} Exp";
        questBox.GetChild(1).GetComponent<Text>().text = $"{questDataJson.KillCount} / {mutantKillerQuest.ClearCount}";
        questBox.GetChild(1).GetChild(0).GetComponent<Text>().text = mutantKillerQuest.MutantName;
        questBox.GetChild(1).gameObject.SetActive(true);
        questBox.GetChild(2).GetComponent<Text>().text = $"{questDataJson.bossKillCount} / {mutantKillerQuest.BossClearCount}";
        questBox.GetChild(2).GetChild(0).GetComponent<Text>().text = mutantKillerQuest.BossName;
        questBox.GetChild(2).gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        questDataJson.questState = QuestState.QuestTake;
        DataManager.dataInst.SaveQuestData(QuestState.QuestTake, mutantKillerQuest.Name, questDataJson.Idx);
        questTransform.gameObject.SetActive(true);
    }
    // ��� ������ ����Ʈ �ε����� ��ȯ
    public int GetAvailableQuestIdx()
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (!questList[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1; // ��� ������ �ε����� ���� ��� -1 ��ȯ
    }


    // ����Ʈ �Ϸ� ó�� (QuestState)
    public void CompleteQuest(int exp, string questName, ref QuestDataJson questDataJson)
    {
        questList[questDataJson.Idx].gameObject.SetActive(false);
        questDataJson.questState = QuestState.None;
        DataManager.dataInst.SaveQuestData(QuestState.None, questName, questDataJson.Idx);
        GameManager.GM.ExpUp(exp);
    }
    public void CompleteQuest(int exp, int gold, string questName, ref QuestDataJson questDataJson)
    {
        Transform quest = questList[questDataJson.Idx].GetChild(0);
        quest.GetChild(1).gameObject.SetActive(false);
        quest.GetChild(2).gameObject.SetActive(false);
        quest.GetChild(3).GetChild(0).gameObject.SetActive(false);
        quest.GetChild(3).GetChild(1).gameObject.SetActive(false);
        questList[questDataJson.Idx].gameObject.SetActive(false);
        questDataJson.questState = QuestState.None;
        DataManager.dataInst.SaveQuestData(QuestState.None, questName, questDataJson.Idx);
        GameManager.GM.ExpUp(exp);
        ItemManager.itemInst.GoldPlus(gold);
    }

    // ����Ʈ�� ų ī��Ʈ ������Ʈ
    public void UpdateKillCount(int questIdx, bool boss = false)
    {
        switch (questIdx)
        {
            case 0:
                IncrementKillCount(ref dataManager.mariaQuest_01DataJson, mariaQuest_01.ClearCount);
                break;
            case 1:
                IncrementKillCount(ref dataManager.mutantKillerQuest_01DataJson, mutantKillerQuest_01.ClearCount);
                break;
            case 2:
                IncrementBossKillCount(ref dataManager.mutantKillerQuest_02DataJson, mutantKillerQuest_02.ClearCount, mutantKillerQuest_02.BossClearCount, boss);
                break;
            case 3:
                IncrementBossKillCount(ref dataManager.mutantKillerQuest_03DataJson, mutantKillerQuest_03.ClearCount, mutantKillerQuest_03.BossClearCount, boss);
                break;
            case 4:
                IncrementBossKillCount(ref dataManager.mutantKillerQuest_04DataJson, mutantKillerQuest_04.ClearCount, mutantKillerQuest_04.BossClearCount, boss);
                break;

        }
    }

    // ����Ʈ�� ų ī��Ʈ�� ������Ű�� �޼ҵ� (QuestState)
    private void IncrementKillCount(ref QuestDataJson questDataJson, int clearCount)
    {
        if (questDataJson.questState == QuestState.QuestTake)
        {
            questDataJson.KillCount++;

            UpdateQuestProgress(questDataJson.Idx, questDataJson.KillCount, clearCount);
            DataManager.dataInst.SaveQuestData(questDataJson.questState, questDataJson.Name, questDataJson.Idx, questDataJson.KillCount);

            if (questDataJson.KillCount >= clearCount)
            {
                questDataJson.KillCount = clearCount;
                DataManager.dataInst.SaveQuestData(QuestState.QuestClear, questDataJson.Name, questDataJson.Idx, questDataJson.KillCount);
            }
        }
    }


    private void IncrementBossKillCount(ref QuestDataJson questData, int clearCount, int bossClearCount, bool boss = false)
    {
        if (questData.questState == QuestState.QuestTake)
        {
            if (!boss) questData.KillCount++;
            else questData.bossKillCount++;

            UpdateQuestProgress(questData.Idx, questData.KillCount, clearCount, questData.bossKillCount, bossClearCount);
            DataManager.dataInst.SaveQuestData(questData.questState, questData.Name, questData.Idx, questData.KillCount, questData.bossKillCount);

            if (questData.KillCount >= clearCount && questData.bossKillCount >= bossClearCount)
            {
                questData.KillCount = clearCount;
                questData.bossKillCount = bossClearCount;
                questData.questState = QuestState.QuestClear;
                DataManager.dataInst.SaveQuestData(questData.questState, questData.Name, questData.Idx, questData.KillCount, questData.bossKillCount);
            }
        }
    }
    // ����Ʈ ���� ���� ������Ʈ
    public void UpdateQuestProgress(int questIdx, int questGoalCount, int questClearCount)
    {
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = $"{questGoalCount} / {questClearCount}";
    }
    public void UpdateQuestProgress(int questIdx, int mutantKillCount, int mutantClearCount, int bossKillCount, int bossClearCount)
    {
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = $"{mutantKillCount} / {mutantClearCount}";
        questList[questIdx].GetChild(0).GetChild(2).GetComponent<Text>().text = $"{bossKillCount} / {bossClearCount}";
    }
    public void PlayerLevel30() { dataManager.mutantKillerQuest_04DataJson.questState = QuestState.QuestNormal; }
}
