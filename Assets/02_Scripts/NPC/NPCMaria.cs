using UnityEngine;

public class NPCMaria : NPCDialogue
{
    [SerializeField] private DialougeQuest questData01;
    [SerializeField] private MutantKillQuest mariaQuest_01;
    [SerializeField] private DialougeQuest mariaQuest_02;

    public override void Initialize()
    {
        base.Initialize();
        UpdateQuestMarkers();
    }

    private void UpdateQuestMarkers()
    {
        if (questData01.questState == QuestState.QuestTake)
        {
            QuestClear();
        }
        else
        {
            switch (mariaQuest_01.questState)
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
        if (questData01.questState == QuestState.QuestTake)
        {
            dialogueIdx = 0;
            FindObjectOfType<NPCLeader>().QuestEnd();
            QuestManager.questInst.CompleteQuest(questData01.Idx, ref questData01.questState);
        }
        else
        {
            switch (mariaQuest_01.questState)
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
        switch (mariaQuest_01.questState)
        {
            case QuestState.QuestHave:
                QuestManager.questInst.AddQuest_02(ref mariaQuest_01.Idx, mariaQuest_01.Image, mariaQuest_01.Name, mariaQuest_01.Content, mariaQuest_01.Gold.ToString() + " Gold, " + mariaQuest_01.Exp.ToString() + " Exp", mariaQuest_01.MutantName,
                mariaQuest_01.KillCount, mariaQuest_01.ClearCount, ref mariaQuest_01.questState, QuestState.QuestTake);
                QuestEnd();
                break;
            case QuestState.QuestClear:
                QuestManager.questInst.CompleteQuest(mariaQuest_01.Idx, ref mariaQuest_01.questState);
                QuestManager.questInst.AddQuest_01(ref mariaQuest_02.Idx, mariaQuest_02.Image, mariaQuest_02.Name, mariaQuest_02.Content, mariaQuest_02.Exp.ToString() + " Exp", ref mariaQuest_02.questState, QuestState.QuestTake);
                ItemManager.itemInst.GoldPlus(mariaQuest_01.Gold);
                GameManager.GM.ExpUp(mariaQuest_01.Exp);
                QuestEnd();
                break;
        }
    }
}
