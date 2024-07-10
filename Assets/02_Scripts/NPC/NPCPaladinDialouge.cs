using DG.Tweening;
using UnityEngine;
public class NPCPaladinDialouge : NPCDialogue
{
    [SerializeField] private Transform gate01;
    [SerializeField] private Transform gate02;
    [SerializeField] QuestData03 questData;
    private PlayerAttack playerAttack;
    private bool isGive = false;

    protected override void Start()
    {
        base.Start();
        playerAttack = playerController.GetComponent<PlayerAttack>();
    }

    protected override void StartDialogue()
    {
        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        if (playerAttack.isSword && playerAttack.isShield)
        {
            dialogueIdx = 1;
            if (!isGive)
            {
                ItemManger.itemInst.GetSword01();
                isGive = true;
                QuestManager.questInst.QustPlus(ref questData.Idx, questData.Image, questData.Name, questData.Content, questData.exp.ToString() + " exp");
            }
        }
        else
        {
            dialogueIdx = 3;
            Sequence seq = DOTween.Sequence();
            seq.Append(gate01.DOBlendableRotateBy(new Vector3(0, 90, 0), 5.0f));
            seq.Join(gate02.DOBlendableRotateBy(new Vector3(0, -90, 0), 5.0f));
            if (my3DQuesMark != null)
                my3DQuesMark.SetActive(true);
            QuestManager.questInst.QuestClear(questData.Idx);
            GameManager.GM.ExpUp(questData.exp);
        }
        base.EndDialogue();
    }
}
