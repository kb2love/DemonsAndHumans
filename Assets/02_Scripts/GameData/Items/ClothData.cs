using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothData", menuName = "ScriptableObjects/Items/ClothData", order = 5)]
public class ClothData : ScriptableObject
{
    [Header("¸ðÇè°¡ÀÇ ¿Ê")]
    public Sprite Image_01;
    public float HP_01;
    public float Defence_01;
    public int Idx_01;
    public int Count_01;
    public string Name_01; 
    [Header("¾Ï»ìÀÚÀÇ ¿Ê")]
    public Sprite Image_02;
    public float HP_02;
    public float Defence_02;
    public int Idx_02;
    public int Count_02;
    public string Name_02; 
    [Header("¿µ¿õÀÇ °©¿Ê")]
    public Sprite Image_03;
    public float HP_03;
    public float Defence_03;
    public int Idx_03;
    public int Count_03;
    public string Name_03; 
}
