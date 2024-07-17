using UnityEngine;


[CreateAssetMenu(fileName = "QuestData03", menuName = "ScriptableObjects/Quest/QuestData03", order = 2)]
public class QuestData03 : ScriptableObject
{
    [Header("퀘스트 이미지")]
    public Sprite Image;
    [Header("퀘스트 이름")]
    public string Name;
    [Header("퀘스트 내용")]
    public string Content;
    [Header("퀘스트 보상")]
    public int exp;
    [Header("퀘스트 위치")]
    public int Idx;
    [Header("퀘스트 성공여부")]
    public bool Result;
    [Header("퀘스트 수주여부")]
    public bool Take;
}