using UnityEngine;
public class NPCMaria : NPCDialogue
{
    [SerializeField] private QuestData01 questData01;
    [SerializeField] private QuestData02 questData02;
    protected override void Start()
    {
        if (questData02.Result || questData02.Take)
        {
            QuestEnd();
        }
        else if(!questData02.Take)
        {
            QuestAdd();
        }
        base.Start();
    }
    protected override void StartDialogue()
    {
        if (questData02.Result && !questData02.Take) { dialogueIdx = 3; }        // ���� ����Ʈ�� �Ѱ��� ��
        else if (questData02.Result && questData02.Take) { dialogueIdx = 2; }    // ���� ����Ʈ�� �Ѱ��ٶ�
        else if (!questData02.Result && questData02.Take) { dialogueIdx = 1; }   // ����Ʈ�� �Ѱ� �� ��
        else if (!questData01.Result && questData01.Take)
        {
            dialogueIdx = 0;
            QuestManager.questInst.QuestClear(questData01.Idx, ref questData01.Result, ref questData01.Take);
            GameManager.GM.ExpUp(questData01.exp);
        }  // ����Ʈ�� �ް� �Ѱ��ֱ�

        base.StartDialogue();
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();
        if (!questData02.Take && !questData02.Result)
        {
            QuestManager.questInst.QuestPlus(ref questData02.Idx, questData02.Image, questData02.Name, questData02.Content, questData02.gold.ToString() + " gold, " + questData02.exp.ToString() + " exp", questData02.killCount, questData02.clearCount, ref questData02.Take);
            QuestEnd();
            
        }
        // �߰����� �۾�
    }
}
