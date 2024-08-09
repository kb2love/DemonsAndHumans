using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "ScriptableObjects/Items/PotionData", order = 10)]
public class PotionData : ScriptableObject
{
    [Header("����HP����")]
    public Sprite HPpotion01;
    public float HPpotion01Value;
    public int HPpotion01quick;
    public string HPpotion01Name; 
    [Header("����HP����")]
    public Sprite HPpotion02;
    public float HPpotion02Value;
    public int HPpotion02quick;
    public string HPpotion02Name; 
    [Header("����HP����")]
    public Sprite HPpotion03;
    public float HPpotion03Value;
    public int HPpotion03quick;
    public string HPpotion03Name; 
    [Header("����MP����")]
    public Sprite MPpotion01;
    public float MPpotion01Value;
    public int MPpotion01quick;
    public string MPpotion01Name; 
    [Header("����MP����")]
    public Sprite MPpotion02;
    public float MPpotion02Value;
    public int MPpotion02quick;
    public string MPpotion02Name; 
    [Header("����MP����")]
    public Sprite MPpotion03;
    public float MPpotion03Value;
    public int MPpotion03quick;
    public string MPpotion03Name; 

}
