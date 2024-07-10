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
    [SerializeField] Transform questImage;
    public List<Transform> questList = new List<Transform>();
    private void Awake()
    {
        if (questInst == null)
        {
            questInst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (questInst != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        for(int i = 1; i < questImage.childCount; i++)
        {
            questList.Add(questImage.GetChild(i).transform);
        }
    }
    public void QustPlus(ref int questIdx, Sprite questSprite,string questName, string questContent, string questRewards)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (!questList[i].gameObject.activeSelf)
            {
                questIdx = i;
                break;
            }
        }
        questList[questIdx].GetChild(1).GetComponent<Text>().text = questName;
        questList[questIdx].GetChild(0).GetChild(0).GetComponent<Text>().text = questContent;
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = questRewards;
        questList[questIdx].GetChild(2).GetComponent<Image>().sprite = questSprite;
        questList[questIdx].GetChild(0).GetChild(2).gameObject.SetActive(false);
        questList[questIdx].gameObject.SetActive(true);
    }
    public void QustPlus(ref int questIdx, Sprite questSprite ,string questName, string questContent, string questRewards, int questGoalCount, int questClearCount)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (!questList[i].gameObject.activeSelf)
            {
                questIdx = i;
                break;
            }
        }
        questList[questIdx].GetChild(2).GetComponent<Image>().sprite = questSprite;
        questList[questIdx].GetChild(1).GetComponent<Text>().text = questName;
        questList[questIdx].GetChild(0).GetChild(0).GetComponent<Text>().text = questContent;
        questList[questIdx].GetChild(0).GetChild(1).GetComponent<Text>().text = questRewards;
        questList[questIdx].GetChild(0).GetChild(2).gameObject.SetActive(true);
        questList[questIdx].GetChild(0).GetChild(2).GetComponent<Text>().text = questGoalCount.ToString() + " / " + questClearCount.ToString();
        questList[questIdx].gameObject.SetActive(true);
    }
    public void DoingQuest(int questIdx, int questGoalCount, int questClearCount)
    {
        questList[questIdx].GetChild(0).GetChild(2).GetComponent<Text>().text = questGoalCount.ToString() + " / " + questClearCount.ToString();
    }
    public void QuestClear(int questIdx)
    {
        questList[questIdx].gameObject.SetActive(false);
    }    
}
