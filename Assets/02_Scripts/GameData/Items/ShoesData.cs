using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShoesData", menuName = "ScriptableObjects/Items/ShoesData", order = 7)]
public class ShoesData : ScriptableObject
{
    [Header("���谡�� �Ź�")]
    public Sprite shoes01;
    public float shoes01HP;
    public float shoes01Defence;
    public int shoes01Count;
    public int shoes01Idx;
    public string shoes01Name;
    public string shoes01Explain;
    [Header("�ϻ����� �Ź�")]
    public Sprite shoes02;
    public float shoes02HP;
    public float shoes02Defence;
    public int shoes02Idx;
    public int shoes02Count;
    public string shoes02Name;
    public string shoes02Explain;
    [Header("������ �Ź�")]
    public Sprite shoes03;
    public float shoes03HP;
    public float shoes03Defence;
    public int shoes03Idx;
    public int shoes03Count;
    public string shoes03Name;
    public string shoes03Explain;
}
