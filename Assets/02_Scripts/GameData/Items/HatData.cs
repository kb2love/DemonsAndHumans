using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatData", menuName = "ScriptableObjects/Items/HatData", order = 4)]
public class HatData : ScriptableObject
{
    [Header("�ϻ����� ����")]
    public Sprite Image_01;
    public float HP_01;
    public float Defence_01;
    public int Idx_01;
    public int Count_01;
    public string Name_01; 
    [Header("������ ����")]
    public Sprite Image_02;
    public float HP_02;
    public float Defence_02;
    public int Idx_02;
    public int Count_02;
    public string Name_02; 
    [Header("������ ����")]
    public Sprite Image_03;
    public float HP_03;
    public float Defence_03;
    public int Idx_03;
    public int Count_03;
    public string Name_03; 
    [Header("������ ����")]
    public Sprite Image_04;
    public float HP_04;
    public float Defence_04;
    public int Idx_04;
    public int Count_04;
    public string Name_04; 
}
