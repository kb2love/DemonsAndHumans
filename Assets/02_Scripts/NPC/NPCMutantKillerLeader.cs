using UnityEngine;

public class NPCMutantKillerLeader : NPCDialogue
{
    // 퀘스트 데이터 변수들
    [SerializeField] private DialougeQuest maraiQuest_02;
    [SerializeField] private MutantKillQuest mutantKillerQuest_01;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_02;
    [SerializeField] private MutantKillerQuest mutantKillerQuest_03;

    // NPC 초기화 메소드
    public override void Initialize()
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


        base.Initialize();
    }

    // 대화 시작 메소드
    protected override void StartDialogue()
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
        if (mutantKillerQuest_03.questState == QuestState.QuestHave)
        {
            #region 첫번째 퀘스트
            if (mutantKillerQuest_02.questState == QuestState.QuestHave)
            {
                switch (mutantKillerQuest_01.questState)
                {
                    case QuestState.QuestHave:
                        maraiQuest_02.questState = QuestState.None;
                        QuestManager.questInst.CompleteQuest(maraiQuest_02.Idx, ref maraiQuest_02.questState);
                        QuestManager.questInst.AddQuest_02(ref mutantKillerQuest_01.Idx, mutantKillerQuest_01.Image, mutantKillerQuest_01.Name, mutantKillerQuest_01.Content, mutantKillerQuest_01.Gold.ToString() + " Gold, " +
                        mutantKillerQuest_01.Exp.ToString() + " Exp", mutantKillerQuest_01.MutantName,
                        mutantKillerQuest_01.KillCount, mutantKillerQuest_01.ClearCount, ref mutantKillerQuest_01.questState, QuestState.QuestTake);
                        QuestEnd();
                        break;
                    case QuestState.QuestTake:
                        break;
                    case QuestState.QuestClear:
                        QuestManager.questInst.CompleteQuest(mutantKillerQuest_01.Idx, ref mutantKillerQuest_01.questState);
                        ItemManager.itemInst.GetCloth01();
                        ItemManager.itemInst.GetPants01();
                        QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_02.Idx, mutantKillerQuest_02.Image, mutantKillerQuest_02.Name, mutantKillerQuest_02.Content, mutantKillerQuest_02.Gold.ToString() +
                        " Gold, " + mutantKillerQuest_02.Exp.ToString() + " Exp", mutantKillerQuest_02.RewardImage_01, mutantKillerQuest_02.RewardImage_02, mutantKillerQuest_02.MutantName,
                        mutantKillerQuest_02.BossName, mutantKillerQuest_02.KillCount, mutantKillerQuest_02.ClearCount, mutantKillerQuest_02.BossKillCount, mutantKillerQuest_02.BossClearCount,
                        ref mutantKillerQuest_02.questState, QuestState.QuestTake);
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
                        QuestManager.questInst.CompleteQuest(mutantKillerQuest_02.Idx, ref mutantKillerQuest_02.questState);
                        ItemManager.itemInst.GetCloth02();
                        ItemManager.itemInst.GetPants02();
                        QuestManager.questInst.AddQuest_03(ref mutantKillerQuest_03.Idx, mutantKillerQuest_03.Image, mutantKillerQuest_03.Name, mutantKillerQuest_03.Content, mutantKillerQuest_03.Gold.ToString() +
                            " Gold, " + mutantKillerQuest_03.Exp.ToString() + " Exp", mutantKillerQuest_03.RewardImage_01, mutantKillerQuest_03.RewardImage_02, mutantKillerQuest_03.MutantName,
                            mutantKillerQuest_03.BossName, mutantKillerQuest_03.KillCount, mutantKillerQuest_03.ClearCount, mutantKillerQuest_03.BossKillCount, mutantKillerQuest_03.BossClearCount,
                            ref mutantKillerQuest_03.questState, QuestState.QuestClear);
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
                    QuestManager.questInst.CompleteQuest(mutantKillerQuest_03.Idx, ref mutantKillerQuest_01.questState);
                    ItemManager.itemInst.GetCloth03();
                    ItemManager.itemInst.GetPants03();
                    break;
                case QuestState.None:
                    break;
            }
        }
        #endregion
    }
}
