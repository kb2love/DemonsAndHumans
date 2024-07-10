using UnityEngine;
public class NPCLeader : NPCDialogue
{
    [SerializeField] private QuestData01 questData;

    protected override void EndDialogue()
    {
        base.EndDialogue();
        QuestManager.questInst.QustPlus(ref questData.Idx, questData.Image, questData.Name, questData.content, questData.exp.ToString() + " exp");
        
        // 추가적인 작업
    }
}
