using UnityEngine;

public class NPCMutantKillerLeader : NPCDialogue
{
    // 퀘스트 데이터 변수들
    [SerializeField] private QuestData02 questData02;
    [SerializeField] private QuestData04 questData04;

    // NPC 초기화 메소드
    public override void Initialize()
    {
        if(questData04.questState_03 == QuestState_03.QuestHave)
        {
            if (questData04.questState_02 == QuestState_02.QuestHave)
            {
                if (questData02.questState_02 == QuestState_02.QuestTake)
                {
                    QuestClear();
                }
                else
                {
                    switch (questData04.questState_01)
                    {
                        case QuestState_01.QuestHave:
                            break;
                        case QuestState_01.QuestTake:
                            QuestEnd();
                            break;
                        case QuestState_01.QuestClear:
                            QuestClear();
                            break;
                        case QuestState_01.None:
                            QuestEnd();
                            break;
                    }
                }
            }
            else
            {
                switch (questData04.questState_02)
                {
                    case QuestState_02.QuestHave:
                        break;
                    case QuestState_02.QuestTake:
                        QuestEnd();
                        break;
                    case QuestState_02.QuestClear:
                        QuestClear();
                        break;
                    case QuestState_02.None:
                        QuestEnd();
                        break;
                }
            }
        }
        else
        {

            switch (questData04.questState_03)
            {
                case QuestState_03.QuestHave:
                    break;
                case QuestState_03.QuestTake:
                    QuestEnd();
                    break;
                case QuestState_03.QuestClear:
                    QuestClear();
                    break;
                case QuestState_03.None:
                    QuestEnd();
                    break;
            }
        }
       

        base.Initialize();
    }

    // 대화 시작 메소드
    protected override void StartDialogue()
    {
        if (questData04.questState_03 == QuestState_03.QuestHave)
        {
            if (questData04.questState_02 == QuestState_02.QuestHave)
            {
                if (questData02.questState_02 == QuestState_02.QuestTake)
                {
                    dialogueIdx = 0;
                }
                else
                {
                    switch (questData04.questState_01)
                    {
                        case QuestState_01.QuestHave:
                            dialogueIdx = 0;
                            break;
                        case QuestState_01.QuestTake:
                            dialogueIdx = 1;
                            break;
                        case QuestState_01.QuestClear:
                            dialogueIdx = 2;
                            break; 
                        case QuestState_01.None:
                            break;
                    }
                }
            }
            #region 두번째 퀘스트
            else
            {
                switch (questData04.questState_02)
                {
                    case QuestState_02.QuestHave:
                        break;
                    case QuestState_02.QuestTake:
                        dialogueIdx = 3;
                        break;
                    case QuestState_02.QuestClear:
                        dialogueIdx = 4;
                        break;
                    case QuestState_02.None:
                        break;
                }
            }
            #endregion
        }
        #region 세번째 퀘스트
        else
        {
            switch (questData04.questState_03)
            {
                case QuestState_03.QuestHave:
                    break;
                case QuestState_03.QuestTake:
                    dialogueIdx = 5;
                    break;
                case QuestState_03.QuestClear:
                    dialogueIdx = 6;
                    break;
                case QuestState_03.None:
                    break;
            }
        }
        #endregion
        base.StartDialogue();
        Debug.Log(dialogueIdx);
    }

    // 대화 종료 메소드
    protected override void EndDialogue()
    {
        base.EndDialogue();

        // 특정 퀘스트 상태에 따라 퀘스트 완료 및 새로운 퀘스트 추가
        if (questData04.questState_03 == QuestState_03.QuestHave)
        {
            #region 첫번째 퀘스트
            if (questData04.questState_02 == QuestState_02.QuestHave)
            {
                switch (questData04.questState_01)
                {
                    case QuestState_01.QuestHave:
                        questData02.questState_02 = QuestState_02.None;
                        QuestManager.questInst.CompleteQuest(questData02.Idx_02, ref questData02.questState_02);
                        QuestManager.questInst.AddQuest(ref questData04.Idx_01, questData04.Image_01, questData04.Name_01, questData04.Content_01, questData04.gold_01.ToString() + " Gold, " + questData04.exp_01.ToString() + " Exp",
                            questData04.rewardImage_01_01, questData04.rewardImage_01_02, questData04.MutantName_01, questData04.killCount_01, questData04.clearCount_01, ref questData04.questState_01, QuestState_01.QuestTake);
                        QuestEnd();
                        break;
                    case QuestState_01.QuestTake:
                        break;
                    case QuestState_01.QuestClear:
                        QuestManager.questInst.CompleteQuest(questData04.Idx_01, ref questData04.questState_01);
                        ItemManager.itemInst.GetCloth01();
                        ItemManager.itemInst.GetPants01();
                        QuestManager.questInst.AddQuest_02(ref questData04.Idx_02, questData04.Image_02, questData04.Name_02, questData04.Content_02, questData04.gold_02.ToString() + " Gold, " + questData04.exp_02.ToString() + " Exp", questData04.rewardImage_02_02,
                            questData04.rewardImage_02_02, questData04.MutantName_02, questData04.bossName_02, questData04.killCount_02_01, questData04.clearCount_02_01, ref questData04.questState_02, QuestState_02.QuestTake);
                        break;
                    case QuestState_01.None:
                        break;
                }
            }
            #endregion
            #region 두번째 퀘스트
            else
            {
                switch (questData04.questState_02)
                {
                    case QuestState_02.QuestHave:
                        break;
                    case QuestState_02.QuestTake:
                        break;
                    case QuestState_02.QuestClear:
                        QuestManager.questInst.CompleteQuest(questData04.Idx_02, ref questData04.questState_02);
                        ItemManager.itemInst.GetCloth02();
                        ItemManager.itemInst.GetPants02();
                        QuestManager.questInst.AddQuest_03(QuestState_03.QuestClear);
                        break;
                    case QuestState_02.None:
                        break;
                }
            }
            #endregion
        }
        #region 세번째 퀘스트
        else
        {
            switch (questData04.questState_03)
            {
                case QuestState_03.QuestHave:
                    break;
                case QuestState_03.QuestTake:
                    break;
                case QuestState_03.QuestClear:
                    QuestManager.questInst.CompleteQuest(questData04.Idx_03, ref questData04.questState_03);
                    ItemManager.itemInst.GetCloth03();
                    ItemManager.itemInst.GetPants03();
                    break;
                case QuestState_03.None:
                    break;
            }
        }
        #endregion
    }
}
