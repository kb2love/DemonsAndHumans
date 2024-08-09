using UnityEngine;
using static NPCDialogue;

public class NPCLeader : NPCDialogue
{
    [SerializeField] private DialougeQuest leaderQuest;
    DataManager dataManager;
    public override void Initialize()
    {
        dataManager = DataManager.dataInst;
        base.Initialize();
        UpdateQuestMarkers();
    }

    private void UpdateQuestMarkers()
    {
        switch (dataManager.leaderQuestDataJson.questState)
        {
            case QuestState.QuestHave:
                QuestAdd();
                break;
            case QuestState.QuestTake:
                QuestEnd();
                break;
            case QuestState.None:
                QuestEnd();
                break;
        }
    }

    protected override void StartDialogue()
    {
        switch (dataManager.leaderQuestDataJson.questState)
        {
            case QuestState.QuestHave:
                dialogueIdx = 0;
                break;
            case QuestState.QuestTake:
                dialogueIdx = 1;
                break;
            case QuestState.None:
                dialogueIdx = 2;
                break;
        }

        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();

        if (dataManager.leaderQuestDataJson.questState == QuestState.QuestHave)
        {
            QuestManager.questInst.AddQuest_01(ref leaderQuest,ref dataManager.leaderQuestDataJson);
            QuestEnd();
            FindObjectOfType<NPCMaria>().QuestAdd();
        }
    }
}
