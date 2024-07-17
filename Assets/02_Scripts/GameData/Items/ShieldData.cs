using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData", menuName = "ScriptableObjects/Items/ShieldData", order = 3)]
public class ShieldData : ScriptableObject
{
    [Header("Base Shield")]
    public Sprite shield01;
    public float shield01Value;
    public int shield01Idx;
    public int shield01Count;
    public string shield01Name;
    public string shield01Explain;
    public string parent01Path;
    [Header("그리핀의 방패")]
    public Sprite shield02;
    public float shield02Value;
    public int shield02Idx;
    public int shield02Count;
    public string shield02Name;
    public string shield02Explain;
    public string parent02Path;
    [Header("마족의 방패")]
    public Sprite shield03;
    public float shield03Value;
    public int shield03Idx;
    public int shield03Count;
    public string shield03Name;
    public string shield03Explain;
    public string parent03Path;
    [Header("영웅의 방패")]
    public Sprite shield04;
    public float shield04Value;
    public int shield04Idx;
    public int shield04Count;
    public string shield04Name;
    public string shield04Explain;
    public string parent04Path;
}
