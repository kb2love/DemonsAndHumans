using UnityEngine;
public class NPCLeader : NPCDialogue
{
    [SerializeField] private QuestData01 questData;
    protected override void Start()
    {
        if (questData.Result || questData.Take)
        {
            QuestEnd();
        }
        base.Start();
    }
    protected override void StartDialogue()
    {
        // �ʿ������� 1. ����Ʈ�� ó�� �޴� ��Ȳ 2. ����Ʈ�� ���� ��Ȳ 3. ����Ʈ�� Ŭ������ ��Ȳ 4. ����Ʈ�� ���� ��Ȳ
        //����Ʈ�� �������� �ʴ´ٸ�
        if (questData.Result && !questData.Take) { dialogueIdx = 2; }   //����Ʈ�� ������ �� ����Ʈ�� �̹� Ŭ�����߰� ���ֹ��� ����Ʈ ���� ������
        else if (!questData.Result && questData.Take) { dialogueIdx = 1; }    // ����Ʈ�� �ް� �� ���� 
        else if (!questData.Result && !questData.Take) { dialogueIdx = 0; } // ����Ʈ�� ó�� ������ �� ����Ʈ�� Ŭ���������ʰ� ����Ʈ�� �����ϰ� ����Ʈ�� �������� �������
        base.StartDialogue();
    }
    protected override void EndDialogue()
    {
        base.EndDialogue();
        if(questData.Take)
        {
            QuestManager.questInst.QuestPlus(ref questData.Idx, questData.Image, questData.Name, questData.content, questData.exp.ToString() + " exp", ref questData.Take);
            QuestEnd();
            FindObjectOfType<NPCMaria>().QuestAdd();
        }
    }
}
