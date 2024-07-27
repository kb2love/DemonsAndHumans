using UnityEngine;

[CreateAssetMenu(fileName = "NeckData", menuName = "ScriptableObjects/Items/NeckData", order = 9)]
public class NeckData : ScriptableObject
{
    [Header("�������� �����")]
    public Sprite Image_01;
    public float MP_01;
    public float Damage_01;
    public float Magicdamage_01;
    public int Idx_01;
    public int Count_01;
    public string Name_01; 
    [Header("�ϻ����� �����")]
    public Sprite Image_02;
    public float MP_02;
    public float Damage_02;
    public float Magicdamage_02;
    public int Idx_02;
    public int Count_02;
    public string Name_02; 
    [Header("������ �����")]
    public Sprite Image_03;
    public float MP_03;
    public float Damage_03;
    public float Magicdamage_03;
    public int Idx_03;
    public int Count_03;
    public string Name_03; 
    [Header("������ �����")]
    public Sprite Image_04;
    public float MP_04;
    public float Damage_04;
    public float Magicdamage_04;
    public int Idx_04;
    public int Count_04;
    public string Name_04; 
}
