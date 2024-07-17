using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class QuestManager : MonoBehaviour
{
    public static QuestManager questInst;
    [SerializeField] QuestData01 questData01;
    [SerializeField] QuestData02 questData02;
    [SerializeField] QuestData03 questData03;
    [SerializeField] RectTransform content;
    Vector2 plusContent;
    public List<Transform> questList = new List<Transform>();
    private void Awake()
    {
        questInst = this;
    }
    private void Start()
    {
        plusContent = content.sizeDelta;    
        for (int i = 1; i < content.childCount; i++)
        {
            questList.Add(content.GetChild(i).transform);
        }
        if (questData01.Take)
        {
            QuestPlus(ref questData01.Idx, questData01.Image, questData01.Name, questData01.content, questData01.exp.ToString() + " exp", ref questData01.Take);
        }
        else if (questData02.Take)
        {
            QuestPlus(ref questData02.Idx, questData02.Image, questData02.Name, questData02.Content, questData02.gold.ToString() + " gold, " + questData02.exp.ToString() + " exp", questData02.killCount, questData02.clearCount, ref questData02.Take);
        }
        else if (questData03.Take)
        {
            QuestPlus(ref questData03.Idx, questData03.Image, questData03.Name, questData03.Content, questData03.exp.ToString() + " exp", ref questData03.Take);
        }
    }
    public void QuestPlus(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, ref bool take)
    {
        questIdx = questListIdx(questIdx);
        questList[questIdx].GetChild(1).GetComponent<Text>().text = questName;
        questList[questIdx].GetChild(0).GetChild(0).GetComponent<Text>().text = questContent;
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = questRewards;
        questList[questIdx].GetChild(2).GetComponent<Image>().sprite = questSprite;
        questList[questIdx].GetChild(0).GetChild(2).gameObject.SetActive(false);
        questList[questIdx].gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        take = true;
    }
    public void QuestPlus(ref int questIdx, Sprite questSprite, string questName, string questContent, string questRewards, int questkillCount, int questClearCount, ref bool take)
    {// 보상이 있는 퀘스트
        questIdx = questListIdx(questIdx);
        questList[questIdx].GetChild(2).GetComponent<Image>().sprite = questSprite;
        questList[questIdx].GetChild(1).GetComponent<Text>().text = questName;
        questList[questIdx].GetChild(0).GetChild(0).GetComponent<Text>().text = questContent;
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = questRewards;
        questList[questIdx].GetChild(0).GetChild(2).gameObject.SetActive(true);
        questList[questIdx].GetChild(0).GetChild(2).GetComponent<Text>().text = questkillCount.ToString() + " / " + questClearCount.ToString();
        questList[questIdx].gameObject.SetActive(true);
        plusContent.y += 305.0f;
        content.sizeDelta = plusContent;
        take = true;
    }

    private int questListIdx(int questIdx)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (!questList[i].gameObject.activeSelf)
            {
                questIdx = i;
                break;
            }
        }

        return questIdx;
    }

    public void DoingQuest(int questIdx, int questGoalCount, int questClearCount)
    {
        questList[questIdx].GetChild(0).GetChild(2).GetComponent<Text>().text = questGoalCount.ToString() + " / " + questClearCount.ToString();
    }
    public void QuestClear(int questIdx,  ref bool result,ref bool take)
    {
        questList[questIdx].gameObject.SetActive(false);
        result = true;
        take = false;
    }
    public void QuestKillCount(int questIdx)
    {
        if(questIdx == 0 && questData02.Take)
        {
            questData02.killCount++;
            questList[questData02.Idx].GetChild(0).GetChild(2).GetComponent<Text>().text = questData02.killCount.ToString() + " / " + questData02.clearCount.ToString();
        }
    }
}
