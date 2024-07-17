using UnityEngine;

[CreateAssetMenu(fileName = "QuestData01", menuName = "ScriptableObjects/Quest/QuestData01", order = 0)]
public class QuestData01 : ScriptableObject
{
    [Header("퀘스트 이미지")]
    public Sprite Image;
    [Header("퀘스트 이름")]
    public string Name;
    [Header("퀘스트 내용")]
    public string content;
    [Header("퀘스트 보상")]
    public int exp;
    [Header("퀘스트 위치")]
    public int Idx;
    [Header("퀘스트 성공여부")]
    public bool Result;
    [Header("퀘스트 수주여부")]
    public bool Take;
}

