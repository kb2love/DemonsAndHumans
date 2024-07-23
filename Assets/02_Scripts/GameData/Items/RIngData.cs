using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RingData", menuName = "ScriptableObjects/Items/RIngData", order = 9)]
public class RIngData : ScriptableObject
{
    [Header("마왕의 반지")]
    public Sprite ring01;
    public float ring01HP;
    public float ring01MP;
    public float ring01Defence;
    public float ring01FatalValue;
    public float ring01FatalProbability;
    public float ring01MagicDamage;
    public float ring01Damage;
    public int ring01Idx;
    public int ring01Count;
    public string ring01Name;
    public string ring01Explain;
    public string parent01Path;
    [Header("용사의 반지")]
    public Sprite ring02;
    public float ring02HP;
    public float ring02MP;
    public float ring02Defence;
    public float ring02FatalValue;
    public float ring02FatalProbability;
    public float ring02MagicDamage;
    public float ring02Damage;
    public int ring02Idx;
    public int ring02Count;
    public string ring02Name;
    public string ring02Explain;
    public string parent02Path; 
}
