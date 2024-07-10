using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordData", menuName = "ScriptableObjects/Items/SwordData", order = 2)]
public class SwordData : ScriptableObject
{
    [Header("·Õ¼Òµå")]
    public Sprite sword01;
    public float sword01Damage;
    public int sword01Idx;
    public int sword01Count;
    public string sword01Name;
    public string sword01Explain;
    [Header("¸¶°Ë")]
    public Sprite sword02;
    public float sword02Damage;
    public int sword02Idx;
    public int sword02Count;
    public string sword02Name;
    public string sword02Explain;
    [Header("¿µ¿õÀÇ °Ë")]
    public Sprite sword03;
    public float sword03Damage;
    public int sword03Idx;
    public int sword03Count;
    public string sword03Name;
    public string sword03Explain;
}
