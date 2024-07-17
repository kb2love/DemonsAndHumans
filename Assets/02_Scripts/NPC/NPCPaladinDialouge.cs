using DG.Tweening;
using UnityEngine;
public class NPCPaladinDialouge : NPCDialogue
{
    [SerializeField] private Transform gate01;
    [SerializeField] private Transform gate02;
    [SerializeField] GameObject nextDoor;
    [SerializeField] QuestData03 questData;
    private bool isGive = false;

    protected override void Start()
    {
        if (questData.Result)
        {
            OpenDoor();
        }
        base.Start();
    }

    protected override void StartDialogue()
    {
        if (questData.Result)
        {
            dialogueIdx = 3;
        }
        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        if (!questData.Result)
        {
            dialogueIdx = 1;
            if (!isGive)
            {
                ItemManger.itemInst.GetSword01();
                ItemManger.itemInst.GetShield01();
                ItemManger.itemInst.AllItemTrSave();
                isGive = true;
                QuestManager.questInst.QuestPlus(ref questData.Idx, questData.Image, questData.Name, questData.Content, questData.exp.ToString() + " exp", ref questData.Take);
                QuestAdd();
            }
        }
        else
        {
            dialogueIdx = 3;
            if(questData.Take)
            {
                OpenDoor();
                QuestEnd();
                QuestManager.questInst.QuestClear(questData.Idx, ref questData.Result, ref questData.Take);
                GameManager.GM.ExpUp(questData.exp);
                isGive = false;
            }
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
        if(questData.Take)
            QuestClear();
    }
}
