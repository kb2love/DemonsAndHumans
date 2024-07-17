using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NeckData", menuName = "ScriptableObjects/Items/NeckData", order = 9)]
public class NeckData : ScriptableObject
{
    [Header("마법사의 목걸이")]
    public Sprite neck01;
    public float neck01MP;
    public float neck01Damage;
    public float neck01MagicDamage;
    public int neck01Idx;
    public int neck01Count;
    public string neck01Name;
    public string neck01Explain;
    public string parent01Path;
    [Header("암살자의 목걸이")]
    public Sprite neck02;
    public float neck02MP;
    public float neck02Damage;
    public float neck02MagicDamage;
    public int neck02Idx;
    public int neck02Count;
    public string neck02Name;
    public string neck02Explain;
    public string parent02Path;
    [Header("마족의 목걸이")]
    public Sprite neck03;
    public float neck03MP;
    public float neck03Damage;
    public float neck03MagicDamage;
    public int neck03Idx;
    public int neck03Count;
    public string neck03Name;
    public string neck03Explain;
    public string parent03Path;
    [Header("영웅의 목걸이")]
    public Sprite neck04;
    public float neck04MP;
    public float neck04Damage;
    public float neck04MagicDamage;
    public int neck04Idx;
    public int neck04Count;
    public string neck04Name;
    public string neck04Explain;
    public string parent04Path;
}
