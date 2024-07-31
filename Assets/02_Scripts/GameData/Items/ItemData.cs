using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Items/ItemData", order = 8)]
public class ItemData : ScriptableObject
{
    [Header("ù��° ������")]
    public string Name_01;
    public Sprite Image_01;
    public float[] Value_01;
    public int Idx_01;
    public int Count_01;
    [Header("�ι�° ������")]
    public string Name_02;
    public Sprite Image_02;
    public float[] Value_02;
    public int Idx_02;
    public int Count_02;
    [Header("����° ������")]
    public string Name_03;
    public Sprite Image_03;
    public float[] Value_03;
    public int Idx_03;
    public int Count_03;
    [Header("�׹�° ������")]
    public string Name_04;
    public Sprite Image_04;
    public float[] Value_04;
    public int Idx_04;
    public int Count_04;
}
