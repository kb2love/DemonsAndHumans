using UnityEngine;
using static NPCDialogue;

public class NPCLeader : NPCDialogue
{
    [SerializeField] private QuestData01 questData01;

    public override void Initialize()
    {
        base.Initialize();
        UpdateQuestMarkers();
    }

    private void UpdateQuestMarkers()
    {
        switch (questData01.questState)
        {
            case QuestState_01.QuestHave:
                QuestAdd();
                break;
            case QuestState_01.QuestTake:
                QuestEnd();
                break;
            case QuestState_01.None:
                QuestEnd();
                break;
        }
    }

    protected override void StartDialogue()
    {
        switch (questData01.questState)
        {
            case QuestState_01.QuestHave:
                dialogueIdx = 0;
                break;
            case QuestState_01.QuestTake:
                dialogueIdx = 1;
                break;
            case QuestState_01.None:
                dialogueIdx = 2;
                break;
        }

        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();

        if (questData01.questState == QuestState_01.QuestHave)
        {
            QuestManager.questInst.AddQuest(ref questData01.Idx, questData01.Image, questData01.Name, questData01.content, questData01.exp.ToString() + " Exp", null, null, null, 0, 0, ref questData01.questState, QuestState_01.QuestTake);
            QuestEnd();
            FindObjectOfType<NPCMaria>().QuestAdd();
        }
    }
}
