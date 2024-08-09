using UnityEngine;

[CreateAssetMenu(fileName = "DialougeQuest", menuName = "ScriptableObjects/Quest/DialougeQuest", order = 0)]
public class DialougeQuest : ScriptableObject
{
    [Header("퀘스트 이미지")]
    public Sprite Image;
    [Header("퀘스트 이름")]
    public string Name;
    [Header("퀘스트 내용")]
    public string Content;
    [Header("퀘스트 보상")]
    public int Exp;
}

