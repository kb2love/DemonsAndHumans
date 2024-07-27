using DG.Tweening;
using UnityEngine;
public class NPCPaladinDialouge : NPCDialogue
{
    [SerializeField] private Transform gate01;
    [SerializeField] private Transform gate02;
    [SerializeField] GameObject nextDoor;
    [SerializeField] DialougeQuest paladinQuest;

    public override void Initialize()
    {
        switch(paladinQuest.questState)
        {
            case QuestState.QuestHave:
                QuestAdd();
                break;
            case QuestState.QuestTake:
                QuestEnd();
                QuestManager.questInst.AddQuest_01(ref paladinQuest.Idx, paladinQuest.Image, paladinQuest.Name, paladinQuest.Content, paladinQuest.Exp.ToString() + " Exp", ref paladinQuest.questState, QuestState.QuestTake); 
                break;
            case QuestState.QuestClear:
                QuestClear();
                break;
            case QuestState.None:
                OpenDoor();
                QuestEnd();
                break;
        }
        base.Initialize();
    }

    protected override void StartDialogue()
    {
        switch (paladinQuest.questState)
        {
            case QuestState.QuestHave: dialogueIdx = 0; break;
            case QuestState.QuestTake: dialogueIdx = 1; break;
            case QuestState.QuestClear: dialogueIdx = 2; break;
            case QuestState.None: dialogueIdx = 3; break;
        }
        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        switch (paladinQuest.questState)
        {
            case QuestState.QuestHave:
                ItemManager.itemInst.GetSword01();
                ItemManager.itemInst.GetShield01();
                ItemManager.itemInst.AllItemSave();
                QuestManager.questInst.AddQuest_01(ref paladinQuest.Idx, paladinQuest.Image, paladinQuest.Name, paladinQuest.Content, paladinQuest.Exp.ToString() + " Exp", ref paladinQuest.questState, QuestState.QuestTake);
                QuestAdd();
                break;
            case QuestState.QuestTake:

                break;
            case QuestState.QuestClear:
                OpenDoor();
                QuestEnd();
                QuestManager.questInst.CompleteQuest(paladinQuest.Idx, ref paladinQuest.questState);
                GameManager.GM.ExpUp(paladinQuest.Exp);
                break;
            case QuestState.None: break;
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
        if (paladinQuest.questState == QuestState.QuestTake)
        {
            paladinQuest.questState = QuestState.QuestClear;
            QuestClear();
        };
    }
}
