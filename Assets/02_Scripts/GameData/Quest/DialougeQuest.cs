using UnityEngine;

[CreateAssetMenu(fileName = "DialougeQuest", menuName = "ScriptableObjects/Quest/DialougeQuest", order = 0)]
public class DialougeQuest : ScriptableObject
{
    [Header("����Ʈ �̹���")]
    public Sprite Image;
    [Header("����Ʈ �̸�")]
    public string Name;
    [Header("����Ʈ ����")]
    public string Content;
    [Header("����Ʈ ����")]
    public int Exp;
}

