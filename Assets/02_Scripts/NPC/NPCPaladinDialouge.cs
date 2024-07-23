using DG.Tweening;
using UnityEngine;
public class NPCPaladinDialouge : NPCDialogue
{
    [SerializeField] private Transform gate01;
    [SerializeField] private Transform gate02;
    [SerializeField] GameObject nextDoor;
    [SerializeField] QuestData03 questData03;

    public override void Initialize()
    {
        switch(questData03.questState)
        {
            case QuestState_01.QuestHave:
                QuestAdd();
                break;
            case QuestState_01.QuestTake:
                QuestEnd();
                QuestManager.questInst.AddQuest(ref questData03.Idx, questData03.Image, questData03.Name, questData03.Content, questData03.exp.ToString() + " exp_01", null, null,null, 0, 0, ref questData03.questState, QuestState_01.QuestTake);
                break;
            case QuestState_01.QuestClear:
                QuestClear();
                break;
            case QuestState_01.None:
                OpenDoor();
                QuestEnd();
                break;
        }
        base.Initialize();
    }

    protected override void StartDialogue()
    {
        switch (questData03.questState)
        {
            case QuestState_01.QuestHave: dialogueIdx = 0; break;
            case QuestState_01.QuestTake: dialogueIdx = 1; break;
            case QuestState_01.QuestClear: dialogueIdx = 2; break;
            case QuestState_01.None: dialogueIdx = 3; break;
        }
        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        switch (questData03.questState)
        {
            case QuestState_01.QuestHave:
                ItemManager.itemInst.GetSword01();
                ItemManager.itemInst.GetShield01();
                ItemManager.itemInst.AllItemTrSave();
                QuestManager.questInst.AddQuest(ref questData03.Idx, questData03.Image, questData03.Name, questData03.Content, questData03.exp.ToString() + " exp_01", null, null,null, 0, 0, ref questData03.questState, QuestState_01.QuestTake);
                QuestAdd();
                break;
            case QuestState_01.QuestTake:

                break;
            case QuestState_01.QuestClear:
                OpenDoor();
                QuestEnd();
                QuestManager.questInst.CompleteQuest(questData03.Idx, ref questData03.questState);
                GameManager.GM.ExpUp(questData03.exp);
                break;
            case QuestState_01.None: break;
        }
        base.EndDialogue();
    }

    private void OpenDoor()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(gate01.DOBlendableRotateBy(new Vector3(0, 90, 0), 5.0f));
        seq.Join(gate02.DOBlendableRotateBy(new Vector3(0, -90, 0), 5.0f));
        nextDoor.SetActive(true);
    }

    public void WeaponWear()
    {
        if (questData03.questState == QuestState_01.QuestTake)
        {
            questData03.questState = QuestState_01.QuestClear;
            QuestClear();
        };
    }
}
