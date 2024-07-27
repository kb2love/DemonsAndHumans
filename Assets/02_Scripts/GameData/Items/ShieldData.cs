using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData", menuName = "ScriptableObjects/Items/ShieldData", order = 3)]
public class ShieldData : ScriptableObject
{
    [Header("Base Shield")]
    public Sprite Image_01;
    public float Defence_01;
    public int Idx_01;
    public int Count_01;
    public string Name_01;
    [Header("그리핀의 방패")]
    public Sprite Image_02;
    public float Defence_02;
    public int Idx_02;
    public int Count_02;
    public string Name_02;
    [Header("마족의 방패")]
    public Sprite Image_03;
    public float Defence_03;
    public int Idx_03;
    public int Count_03;
    public string Name_03;
    [Header("영웅의 방패")]
    public Sprite Image_04;
    public float Defence_04;
    public int Idx_04;
    public int Count_04;
    public string Name_04;
    public int GetIdx(string itemName)
    {
        if (itemName == Name_01) return Idx_01;
        if (itemName == Name_02) return Idx_02;
        if (itemName == Name_03) return Idx_03;
        if (itemName == Name_04) return Idx_04;
        return -1; // Not found
    }

    public int GetCount(string itemName)
    {
        if (itemName == Name_01) return Count_01;
        if (itemName == Name_02) return Count_02;
        if (itemName == Name_03) return Count_03;
        if (itemName == Name_04) return Count_04;
        return 0; // Not found
    }
}
