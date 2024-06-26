using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutantData", menuName = "ScriptableObjects/MutantData", order = 1)]
public class MutantData : ScriptableObject
{
    [Header("Skeleton")]
    public float skMaxHp;
    public float skDamage;
    public GameObject gold;
    public GameObject normalItem;
    public GameObject equipmentItem;
}
