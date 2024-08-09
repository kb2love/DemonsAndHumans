using UnityEngine;

public class NPCMutantKillerLeader : NPCDialogue
{
    // 퀘스트 데이터 변수들
    [SerializeField] private DialougeQuest mariaQuest_02;
    [SerializeField] private MutantKillQuest mutantKillerQuest_01;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_02;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_03;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_04;
    DataManager dataManager;
    // NPC 초기화 메소드
    public override void Initialize()
    {
        dataManager = DataManager.dataInst;
        if (dataManager.mutantKillerQuest_04DataJson.questState == QuestState.QuestHave)
        {
            if (dataManager.mutantKillerQuest_03DataJson.questState == QuestState.QuestHave)
            {
                if (dataManager.mutantKillerQuest_02DataJson.questState == QuestState.QuestHave)
                {
                    if (dataManager.mariaQuest_02DataJson.questState == QuestState.QuestTake)
                    {
                        QuestClear();
                    }
                    else
                    {
                        switch (dataManager.mutantKillerQuest_01DataJson.questState)
                        {
                            case QuestState.QuestTake:
                                QuestEnd();
                                break;
                            case QuestState.QuestClear:
                                QuestClear();
                                break;
                            case QuestState.None:
                                QuestEnd();
                                break;
                        }
                    }
                }
                else
                {
                    switch (dataManager.mutantKillerQuest_02DataJson.questState)
                    {
                        case QuestState.QuestTake:
                            QuestEnd();
                            break;
                        case QuestState.QuestClear:
                            QuestClear();
                            break;
                        case QuestState.None:
                            QuestEnd();
                            break;
                    }
                }
            }
            else
            {

                switch (dataManager.mutantKillerQuest_03DataJson.questState)
                {
                    case QuestState.QuestTake:
                        QuestEnd();
                        break;
                    case QuestState.QuestClear:
                        QuestClear();
                        break;
                    case QuestState.None:
                        QuestEnd();
                        break;
                }
            }
        }
        else
        {
            switch (dataManager.mutantKillerQuest_04DataJson.questState)
            {
                case QuestState.QuestTake:
                    QuestEnd();
                    break;
                case QuestState.QuestClear:
                    QuestClear();
                    break;
                case QuestState.None:
                    QuestEnd();
                    break;
                case QuestState.QuestNormal:
                    QuestAdd();
                    break;
            }
        }


        base.Initialize();
    }

    // 대화 시작 메소드
    protected override void StartDialogue()
    {
        if (dataManager.mutantKillerQuest_04DataJson.questState == QuestState.QuestHave)
        {
            if (dataManager.mutantKillerQuest_03DataJson.questState == QuestState.QuestHave)
            {
                if (dataManager.mutantKillerQuest_02DataJson.questState == QuestState.QuestHave)
                {
                    if (dataManager.mariaQuest_02DataJson.questState == QuestState.QuestTake)
                    {
                        dialogueIdx = 0;
                    }
                    else
                    {
                        switch (dataManager.mutantKillerQuest_01DataJson.questState)
                        {
                            case QuestState.QuestHave:
                                dialogueIdx = 0;
                                break;
                            case QuestState.QuestTake:
                                dialogueIdx = 1;
                                break;
                            case QuestState.QuestClear:
                                dialogueIdx = 2;
                                break;
                            case QuestState.None:
                                break;
                        }
                    }
                }
                #region 두번째 퀘스트
                else
                {
                    switch (dataManager.mutantKillerQuest_02DataJson.questState)
                    {
                        case QuestState.QuestHave:
                            break;
                        case QuestState.QuestTake:
                            dialogueIdx = 3;
                            break;
                        case QuestState.QuestClear:
                            dialogueIdx = 4;
                            break;
                        case QuestState.None:
                            break;
                    }
                }
                #endregion
            }
            #region 세번째 퀘스트
            else
            {
                switch (dataManager.mutantKillerQuest_03DataJson.questState)
                {
                    case QuestState.QuestHave:
                        break;
                    case QuestState.QuestTake:
                        dialogueIdx = 5;
                        break;
                    case QuestState.QuestClear:
                        dialogueIdx = 6;
                        break;
                    case QuestState.None:
                        dialogueIdx = 7;
                        break;
                }
            }
            #endregion
        }
        else
        {
            switch (dataManager.mutantKillerQuest_04DataJson.questState)
            {
                case QuestState.QuestNormal:
                    dialogueIdx = 8;
                    break;
                case QuestState.QuestTake:
                    dialogueIdx = 9;
                    break;
                case QuestState.QuestClear:
                    dialogueIdx = 10;
                    break;
                case QuestState.None:
                    dialogueIdx = 11;
                    break;

            }
        }

        base.StartDialogue();
    }

    // 대화 종료 메소드
    protected override void EndDialogue()
    {
        base.EndDialogue();

        // 특정 퀘스트 상태에 따라 퀘스트 완료 및 새로운 퀘스트 추가
        if (dataManager.mutantKillerQuest_04DataJson.questState == QuestState.QuestHave)
        {
            if (dataManager.mutantKillerQuest_03DataJson.questState == QuestState.QuestHave)
            {
                #region 첫번째 퀘스트
                if (dataManager.mutantKillerQuest_02DataJson.questState == QuestState.QuestHave)
                {
                    switch (dataManager.mutantKillerQuest_01DataJson.questState)
                    {
                        case QuestState.QuestHave:
                            dataManager.mariaQuest_02DataJson.questState = QuestState.None;

                            QuestManager.questInst.CompleteQuest(mariaQuest_02.Exp, mariaQuest_02.Exp, mariaQuest_02.Name, ref dataManager.mariaQuest_02DataJson);
                            QuestManager.questInst.AddQuest_02(ref mutantKillerQuest_01, ref dataManager.mutantKillerQuest_01DataJson);
                            QuestEnd();
                            break;
                        case QuestState.QuestClear:
                            QuestManager.questInst.CompleteQuest(mutantKillerQuest_01.Exp, mutantKillerQuest_01.Gold, mutantKillerQuest_01.Name, ref dataManager.mutantKillerQuest_01DataJson);
                            QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_02, ref dataManager.mutantKillerQuest_02DataJson);
                            QuestEnd();
                            break;
                    }
                }
                #endregion
                #region 두번째 퀘스트
                else
                {
                    switch (dataManager.mutantKillerQuest_02DataJson.questState)
                    {
                        case QuestState.QuestClear:
                            QuestManager.questInst.CompleteQuest(mutantKillerQuest_02.Exp, mutantKillerQuest_02.Gold, mutantKillerQuest_02.Name, ref dataManager.mariaQuest_02DataJson);
                            ItemManager.itemInst.GetNeck01();
                            ItemManager.itemInst.GetKloak01();
                            QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_03, ref dataManager.mutantKillerQuest_03DataJson);
                            QuestEnd();
                            break;
                    }
                }
                #endregion
            }
            #region 세번째 퀘스트
            else
            {
                switch (dataManager.mutantKillerQuest_03DataJson.questState)
                {
                    case QuestState.QuestClear:
                        QuestManager.questInst.CompleteQuest(mutantKillerQuest_03.Exp, mutantKillerQuest_03.Gold, mutantKillerQuest_03.Name, ref dataManager.mutantKillerQuest_03DataJson);
                        ItemManager.itemInst.GetNeck02();
                        ItemManager.itemInst.GetKloak02();
                        QuestEnd();
                        break;
                }
            }
            #endregion
        }
        #region 네번째 퀘스트
        else
        {
            switch (dataManager.mutantKillerQuest_04DataJson.questState)
            {
                case QuestState.QuestNormal:
                    QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_04, ref dataManager.mutantKillerQuest_04DataJson);
                    break;
                case QuestState.QuestClear:
                    QuestManager.questInst.CompleteQuest(mutantKillerQuest_04.Exp, mutantKillerQuest_04.Gold, mutantKillerQuest_04.Name, ref dataManager.mutantKillerQuest_04DataJson);
                    ItemManager.itemInst.GetRing01();
                    ItemManager.itemInst.GetKloak03();
                    QuestEnd();
                    break;
            }
        }
        #endregion
    }
}
