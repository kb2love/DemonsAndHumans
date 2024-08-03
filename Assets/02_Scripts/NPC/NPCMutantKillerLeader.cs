using UnityEngine;

public class NPCMutantKillerLeader : NPCDialogue
{
    // 퀘스트 데이터 변수들
    [SerializeField] private DialougeQuest maraiQuest_02;
    [SerializeField] private MutantKillQuest mutantKillerQuest_01;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_02;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_03;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_04;    
    // NPC 초기화 메소드
    public override void Initialize()
    {
        if(mutantKillerQuest_04.questState == QuestState.QuestNormal)
        {
            if (mutantKillerQuest_03.questState == QuestState.QuestHave)
            {
                if (mutantKillerQuest_02.questState == QuestState.QuestHave)
                {
                    if (maraiQuest_02.questState == QuestState.QuestTake)
                    {
                        QuestClear();
                    }
                    else
                    {
                        switch (mutantKillerQuest_01.questState)
                        {
                            case QuestState.QuestHave:
                                break;
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
                    switch (mutantKillerQuest_02.questState)
                    {
                        case QuestState.QuestHave:
                            break;
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

                switch (mutantKillerQuest_03.questState)
                {
                    case QuestState.QuestHave:
                        break;
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
            switch (mutantKillerQuest_04.questState)
            {
                case QuestState.QuestHave:
                    QuestAdd();
                    break;
                case QuestState.QuestTake:
                    QuestEnd();
                    break;
                case QuestState.QuestClear:
                    QuestClear();
                    break;
                case QuestState.None:
                    break;
            }
        }


        base.Initialize();
    }

    // 대화 시작 메소드
    protected override void StartDialogue()
    {
        Debug.Log(dialogueIdx);
        if(mutantKillerQuest_04.questState == QuestState.QuestNormal)
        {
            if (mutantKillerQuest_03.questState == QuestState.QuestHave)
            {
                if (mutantKillerQuest_02.questState == QuestState.QuestHave)
                {
                    if (maraiQuest_02.questState == QuestState.QuestTake)
                    {
                        dialogueIdx = 0;
                    }
                    else
                    {
                        switch (mutantKillerQuest_01.questState)
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
                    switch (mutantKillerQuest_02.questState)
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
                switch (mutantKillerQuest_03.questState)
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
            switch (mutantKillerQuest_04.questState)
            {
                case QuestState.QuestHave:
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

        Debug.Log(dialogueIdx);
        base.StartDialogue();
    }

    // 대화 종료 메소드
    protected override void EndDialogue()
    {
        base.EndDialogue();

        // 특정 퀘스트 상태에 따라 퀘스트 완료 및 새로운 퀘스트 추가
        if (mutantKillerQuest_04.questState == QuestState.QuestNormal)
        {
            if (mutantKillerQuest_03.questState == QuestState.QuestHave)
            {
                #region 첫번째 퀘스트
                if (mutantKillerQuest_02.questState == QuestState.QuestHave)
                {
                    switch (mutantKillerQuest_01.questState)
                    {
                        case QuestState.QuestHave:
                            maraiQuest_02.questState = QuestState.None;
                            QuestManager.questInst.CompleteQuest(maraiQuest_02.Idx, ref maraiQuest_02.questState, maraiQuest_02.Exp, maraiQuest_02.Name);
                            QuestManager.questInst.AddQuest_02(ref mutantKillerQuest_01.Idx, mutantKillerQuest_01.Image, mutantKillerQuest_01.Name, mutantKillerQuest_01.Content, mutantKillerQuest_01.Gold.ToString() + " Gold, " +
                            mutantKillerQuest_01.Exp.ToString() + " Exp", mutantKillerQuest_01.MutantName,
                            mutantKillerQuest_01.KillCount, mutantKillerQuest_01.ClearCount, ref mutantKillerQuest_01.questState, QuestState.QuestTake);
                            QuestEnd();
                            break;
                        case QuestState.QuestTake:
                            break;
                        case QuestState.QuestClear:
                            QuestManager.questInst.CompleteQuest(mutantKillerQuest_01.Idx, ref mutantKillerQuest_01.questState, mutantKillerQuest_01.Exp, mutantKillerQuest_01.Gold, mutantKillerQuest_01.Name);
                            QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_02.Idx, mutantKillerQuest_02.Image, mutantKillerQuest_02.Name, mutantKillerQuest_02.Content, mutantKillerQuest_02.Gold.ToString() +
                            " Gold, " + mutantKillerQuest_02.Exp.ToString() + " Exp", mutantKillerQuest_02.RewardImage_01, mutantKillerQuest_02.RewardImage_02, mutantKillerQuest_02.MutantName,
                            mutantKillerQuest_02.BossName, mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, mutantKillerQuest_02.BossKillCount, mutantKillerQuest_02.BossClearCount,
                            ref mutantKillerQuest_02.questState, QuestState.QuestTake);
                            QuestEnd();
                            break;
                        case QuestState.None:
                            break;
                    }
                }
                #endregion
                #region 두번째 퀘스트
                else
                {
                    switch (mutantKillerQuest_02.questState)
                    {
                        case QuestState.QuestHave:
                            break;
                        case QuestState.QuestTake:
                            break;
                        case QuestState.QuestClear:
                            QuestManager.questInst.CompleteQuest(mutantKillerQuest_02.Idx, ref mutantKillerQuest_02.questState, mutantKillerQuest_02.Exp, mutantKillerQuest_02.Gold, mutantKillerQuest_02.Name);
                            ItemManager.itemInst.GetNeck01();
                            ItemManager.itemInst.GetKloak01();
                            QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_03.Idx, mutantKillerQuest_03.Image, mutantKillerQuest_03.Name, mutantKillerQuest_03.Content, mutantKillerQuest_03.Gold.ToString() +
                                " Gold, " + mutantKillerQuest_03.Exp.ToString() + " Exp", mutantKillerQuest_03.RewardImage_01, mutantKillerQuest_03.RewardImage_02, mutantKillerQuest_03.MutantName,
                                mutantKillerQuest_03.BossName, mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, mutantKillerQuest_03.BossKillCount, mutantKillerQuest_03.BossClearCount,
                                ref mutantKillerQuest_03.questState, QuestState.QuestClear);
                            QuestEnd();
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
                switch (mutantKillerQuest_03.questState)
                {
                    case QuestState.QuestHave:
                        break;
                    case QuestState.QuestTake:
                        break;
                    case QuestState.QuestClear:
                        QuestManager.questInst.CompleteQuest(mutantKillerQuest_03.Idx, ref mutantKillerQuest_03.questState, mutantKillerQuest_03.Exp, mutantKillerQuest_03.Gold, mutantKillerQuest_03.Name);
                        ItemManager.itemInst.GetNeck02();
                        ItemManager.itemInst.GetKloak02();
                        QuestEnd();
                        break;
                    case QuestState.None:
                        break;
                }
            }
            #endregion
        }
        #region 네번째 퀘스트
        else
        {
            switch (mutantKillerQuest_04.questState)
            {
                case QuestState.QuestHave:
                    QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_04.Idx, mutantKillerQuest_04.Image, mutantKillerQuest_04.Name, mutantKillerQuest_04.Content, mutantKillerQuest_04.Gold.ToString() +
                        " Gold, " + mutantKillerQuest_04.Exp.ToString() + " Exp", mutantKillerQuest_04.RewardImage_01, mutantKillerQuest_04.RewardImage_02, mutantKillerQuest_04.MutantName,
                        mutantKillerQuest_04.BossName, mutantKillerQuest_04.KillCount, mutantKillerQuest_04.ClearCount, mutantKillerQuest_04.BossKillCount, mutantKillerQuest_04.BossClearCount,
                        ref mutantKillerQuest_03.questState, QuestState.QuestTake);
                    break;
                case QuestState.QuestTake:
                    break;
                case QuestState.QuestClear:
                    QuestManager.questInst.CompleteQuest(mutantKillerQuest_04.Idx, ref mutantKillerQuest_04.questState, mutantKillerQuest_04.Exp, mutantKillerQuest_04.Gold, mutantKillerQuest_04.Name);
                    ItemManager.itemInst.GetRing01();
                    ItemManager.itemInst.GetKloak03();
                    QuestEnd();
                    break;
                case QuestState.None:
                    break;
            }
        }
        #endregion
    }
}
