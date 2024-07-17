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
        if (questData02.Result && !questData02.Take) { dialogueIdx = 3; }        // 다음 퀘스트를 넘겨준 뒤
        else if (questData02.Result && questData02.Take) { dialogueIdx = 2; }    // 다음 퀘스트를 넘겨줄때
        else if (!questData02.Result && questData02.Take) { dialogueIdx = 1; }   // 퀘스트를 넘겨 준 뒤
        else if (!questData01.Result && questData01.Take)
        {
            dialogueIdx = 0;
            QuestManager.questInst.QuestClear(questData01.Idx, ref questData01.Result, ref questData01.Take);
            GameManager.GM.ExpUp(questData01.exp);
        }  // 퀘스트를 받고 넘겨주기

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
        // 추가적인 작업
    }
}
