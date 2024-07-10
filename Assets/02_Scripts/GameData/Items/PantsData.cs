using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantsData", menuName = "ScriptableObjects/Items/PantsData", order = 6)]
public class PantsData : ScriptableObject
{
    [Header("모험가의 바지")]
    public Sprite pants01;
    public float pants01HP;
    public float pants01Defence;
    public int pants01Idx;
    public int pants01Count;
    public string pants01Name;
    public string pants01Explain;
    [Header("암살자의 바지")]
    public Sprite pants02;
    public float pants02HP;
    public float pants02Defence;
    public int pants02Idx;
    public int pants02Count;
    public string pants02Name;
    public string pants02Explain;
    [Header("영웅의 바지")]
    public Sprite pants03;
    public float pants03HP;
    public float pants03Defence;
    public int pants03Idx;
    public int pants03Count;
    public string pants03Name;
    public string pants03Explain;
}
