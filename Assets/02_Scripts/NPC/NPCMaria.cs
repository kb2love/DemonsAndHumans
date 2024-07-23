using UnityEngine;

public class NPCMaria : NPCDialogue
{
    [SerializeField] private QuestData01 questData01;
    [SerializeField] private QuestData02 questData02;

    public override void Initialize()
    {
        base.Initialize();
        UpdateQuestMarkers();
    }

    private void UpdateQuestMarkers()
    {
        if (questData01.questState == QuestState_01.QuestTake)
        {
            QuestClear();
        }
        else
        {
            switch (questData02.questState)
            {
                case QuestState_01.QuestHave:
                    QuestAdd();
                    break;
                case QuestState_01.QuestTake:
                    QuestEnd();
                    break;
                case QuestState_01.QuestClear:
                    QuestClear();
                    break;
                case QuestState_01.None:
                    QuestEnd();
                    break;
            }
        }
    }

    protected override void StartDialogue()
    {
        if (questData01.questState == QuestState_01.QuestTake)
        {
            dialogueIdx = 0;
            FindObjectOfType<NPCLeader>().QuestEnd();
            QuestManager.questInst.CompleteQuest(questData01.Idx, ref questData01.questState);
        }
        else
        {
            switch (questData02.questState)
            {
                case QuestState_01.QuestHave:
                    break;
                case QuestState_01.QuestTake:
                    dialogueIdx = 1;
                    break;
                case QuestState_01.QuestClear:
                    dialogueIdx = 2;
                    break;
                case QuestState_01.None:
                    dialogueIdx = 3;
                    break;
            }
        }
        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();
        switch (questData02.questState)
        {
            case QuestState_01.QuestHave:
                QuestManager.questInst.AddQuest(ref questData02.Idx_01, questData02.Image_01, questData02.Name_01, questData02.Content_01, questData02.gold.ToString() + " Gold, " + questData02.exp_01.ToString() + " Exp",
                    null, null, questData02.MutantName, questData02.killCount, questData02.clearCount, ref questData02.questState, QuestState_01.QuestTake);
                QuestEnd();
                break;
            case QuestState_01.QuestClear:
                QuestManager.questInst.CompleteQuest(questData02.Idx_01, ref questData02.questState);
                QuestManager.questInst.AddQuest_02(ref questData02.Idx_02, questData02.Image_02, questData02.Name_02, questData02.Content_02, questData02.exp_02.ToString() + " Exp",
                    null, null, null,null, 0, 0, ref questData02.questState_02, QuestState_02.QuestTake);
                ItemManager.itemInst.GoldPlus(questData02.gold);
                GameManager.GM.ExpUp(questData02.exp_01);
                QuestEnd();
                break;
        }
    }
}
