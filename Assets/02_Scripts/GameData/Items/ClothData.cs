using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothData", menuName = "ScriptableObjects/Items/ClothData", order = 5)]
public class ClothData : ScriptableObject
{
    [Header("¸ðÇè°¡ÀÇ ¿Ê")]
    public Sprite cloth01;
    public float cloth01HP;
    public float cloth01Defence;
    public int cloth01Idx;
    public int cloth01Count;
    public string cloth01Name;
    public string cloth01Explain;
    [Header("¾Ï»ìÀÚÀÇ ¿Ê")]
    public Sprite cloth02;
    public float cloth02HP;
    public float cloth02Defence;
    public int cloth02Idx;
    public int cloth02Count;
    public string cloth02Name;
    public string cloth02Explain;
    [Header("¿µ¿õÀÇ °©¿Ê")]
    public Sprite cloth03;
    public float cloth03HP;
    public float cloth03Defence;
    public int cloth03Idx;
    public int cloth03Count;
    public string cloth03Name;
    public string cloth03Explain;
}
