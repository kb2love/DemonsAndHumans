using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HatData", menuName = "ScriptableObjects/Items/HatData", order = 4)]
public class HatData : ScriptableObject
{
    [Header("암살자의 투구")]
    public Sprite hat01;
    public float hat01HP;
    public float hat01Defence;
    public int hat01Idx;
    public int Hat01Count;
    public string hat01Name;
    public string hat01Explain;
    [Header("전사의 투구")]
    public Sprite hat02;
    public float hat02HP;
    public float hat02Defence;
    public int hat02Idx;
    public int Hat02Count;
    public string hat02Name;
    public string hat02Explain;
    [Header("마족의 투구")]
    public Sprite hat03;
    public float hat03HP;
    public float hat03Defence;
    public int hat03Idx;
    public int Hat03Count;
    public string hat03Name;
    public string hat03Explain;
    [Header("영웅의 투구")]
    public Sprite hat04;
    public float hat04HP;
    public float hat04Defence;
    public int hat04Idx;
    public int Hat04Count;
    public string hat04Name;
    public string hat04Explain;
}
