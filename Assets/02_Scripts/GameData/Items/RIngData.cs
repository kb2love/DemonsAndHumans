using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RingData", menuName = "ScriptableObjects/Items/RIngData", order = 9)]
public class RIngData : ScriptableObject
{
    [Header("마왕의 반지")]
    public Sprite Image_01;
    public float HP_01;
    public float MP_01;
    public float Defence_01;
    public float FatalValue_01;
    public float FatalProbability_01;
    public float MagicDamage_01;
    public float Damage_01;
    public int Idx_01;
    public int Count_01;
    public string Name_01;
    [Header("용사의 반지")]
    public Sprite Image_02;
    public float HP_02;
    public float MP_02;
    public float Defence_02;
    public float FatalValue_02;
    public float FatalProbability_02;
    public float MagicDamage_02;
    public float Damage_02;
    public int Idx_02;
    public int Count_02;
    public string Name_02;
}
