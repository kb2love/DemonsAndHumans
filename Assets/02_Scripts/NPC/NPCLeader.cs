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
        // 필요한조건 1. 퀘스트를 처음 받는 상황 2. 퀘스트를 받은 상황 3. 퀘스트를 클리어한 상황 4. 퀘스트가 없는 상황
        //퀘스트가 존재하지 않는다면
        if (questData.Result && !questData.Take) { dialogueIdx = 2; }   //퀘스트가 없을때 즉 퀘스트를 이미 클리어했고 수주받은 퀘스트 또한 없을떄
        else if (!questData.Result && questData.Take) { dialogueIdx = 1; }    // 퀘스트를 받고 난 다음 
        else if (!questData.Result && !questData.Take) { dialogueIdx = 0; } // 퀘스트를 처음 받을떄 즉 퀘스트를 클리어하지않고 퀘스트가 존재하고 퀘스트를 수주하지 않은경우
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
