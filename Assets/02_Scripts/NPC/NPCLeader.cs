using UnityEngine;
using static NPCDialogue;

public class NPCLeader : NPCDialogue
{
    [SerializeField] private DialougeQuest leaderQuest;

    public override void Initialize()
    {
        base.Initialize();
        UpdateQuestMarkers();
    }

    private void UpdateQuestMarkers()
    {
        switch (leaderQuest.questState)
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
        switch (leaderQuest.questState)
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

        if (leaderQuest.questState == QuestState.QuestHave)
        {
            QuestManager.questInst.AddQuest_01(ref leaderQuest.Idx, leaderQuest.Image, leaderQuest.Name, leaderQuest.Content, leaderQuest.Exp.ToString() + " Exp", ref leaderQuest.questState, QuestState.QuestTake);
            QuestEnd();
            FindObjectOfType<NPCMaria>().QuestAdd();
        }
    }
}
