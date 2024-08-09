using UnityEngine;

public class NPCMaria : NPCDialogue
{
    [SerializeField] private DialougeQuest leaderQuest;
    [SerializeField] private MutantKillQuest mariaQuest_01;
    [SerializeField] private DialougeQuest mariaQuest_02;
    DataManager dataManager;
    public override void Initialize()
    {
        dataManager = DataManager.dataInst;
        base.Initialize();
        UpdateQuestMarkers();
    }

    private void UpdateQuestMarkers()
    {
        if (dataManager.leaderQuestDataJson.questState == QuestState.QuestTake)
        {
            QuestClear();
        }
        else
        {
            switch (dataManager.mariaQuest_01DataJson.questState)
            {
                case QuestState.QuestHave:
                    QuestAdd();
                    break;
                case QuestState.QuestTake:
                    QuestEnd();
                    break;
                case QuestState.QuestClear:
                    QuestClear();
                    break;
                case QuestState.None:
                    QuestEnd();
                    break;
            }
        }
    }

    protected override void StartDialogue()
    {
        if (dataManager.leaderQuestDataJson.questState == QuestState.QuestTake)
        {
            dialogueIdx = 0;
            FindObjectOfType<NPCLeader>().QuestEnd();
            QuestManager.questInst.CompleteQuest(leaderQuest.Exp, leaderQuest.Exp, leaderQuest.Name,ref dataManager.leaderQuestDataJson);
        }
        else
        {
            switch (dataManager.mariaQuest_01DataJson.questState)
            {
                case QuestState.QuestHave:
                    break;
                case QuestState.QuestTake:
                    dialogueIdx = 1;
                    break;
                case QuestState.QuestClear:
                    dialogueIdx = 2;
                    break;
                case QuestState.None:
                    dialogueIdx = 3;
                    break;
            }
        }
        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();
        switch (dataManager.mariaQuest_01DataJson.questState)
        {
            case QuestState.QuestHave:
                QuestManager.questInst.AddQuest_02(ref mariaQuest_01, ref dataManager.mariaQuest_01DataJson);
                QuestEnd();
                break;
            case QuestState.QuestClear:
                QuestManager.questInst.CompleteQuest( mariaQuest_01.Exp, mariaQuest_01.Gold, mariaQuest_01.Name,ref dataManager.mariaQuest_01DataJson);
                QuestManager.questInst.AddQuest_01(ref mariaQuest_02, ref dataManager.mariaQuest_02DataJson);
                QuestEnd();
                break;
        }
    }
}
