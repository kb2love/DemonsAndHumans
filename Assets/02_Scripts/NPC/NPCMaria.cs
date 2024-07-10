using UnityEngine;
public class NPCMaria : NPCDialogue
{
    [SerializeField] private QuestData01 questData01;
    [SerializeField] private QuestData02 questData02;

    protected override void StartDialogue()
    {
        base.StartDialogue();
        QuestManager.questInst.QuestClear(questData01.Idx);
        GameManager.GM.ExpUp(questData01.exp);
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();
        QuestManager.questInst.QustPlus(ref questData02.Idx, questData02.Image, questData02.Name, questData02.Content, questData02.gold.ToString() + " gold, " + questData02.exp.ToString() + " exp", questData02.killCount, questData02.clearCount);
        // 추가적인 작업
    }
}
