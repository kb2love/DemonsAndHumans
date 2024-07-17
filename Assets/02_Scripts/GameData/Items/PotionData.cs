using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "ScriptableObjects/Items/PotionData", order = 10)]
public class PotionData : ScriptableObject
{
    [Header("소형HP물약")]
    public Sprite HPpotion01;
    public float HPpotion01Value;
    public int HPpotion01Count;
    public int HPpotion01Idx;
    public int HPpotion01quick;
    public string HPpotion01Name;
    public string HPpotion01Explain;
    public string parent01Path;
    [Header("중형HP물약")]
    public Sprite HPpotion02;
    public float HPpotion02Value;
    public int HPpotion02Count;
    public int HPpotion02Idx;
    public int HPpotion02quick;
    public string HPpotion02Name;
    public string HPpotion02Explain;
    public string parent02Path;
    [Header("대형HP물약")]
    public Sprite HPpotion03;
    public float HPpotion03Value;
    public int HPpotion03Count;
    public int HPpotion03Idx;
    public int HPpotion03quick;
    public string HPpotion03Name;
    public string HPpotion03Explain;
    public string parent03Path;
    [Header("소형MP물약")]
    public Sprite MPpotion01;
    public float MPpotion01Value;
    public int MPpotion01Count;
    public int MPpotion01Idx;
    public int MPpotion01quick;
    public string MPpotion01Name;
    public string MPpotion01Explain;
    public string parentMP01Path;
    [Header("중형MP물약")]
    public Sprite MPpotion02;
    public float MPpotion02Value;
    public int MPpotion02Count;
    public int MPpotion02Idx;
    public int MPpotion02quick;
    public string MPpotion02Name;
    public string MPpotion02Explain;
    public string parentMP02Path;
    [Header("대형MP물약")]
    public Sprite MPpotion03;
    public float MPpotion03Value;
    public int MPpotion03Count;
    public int MPpotion03Idx;
    public int MPpotion03quick;
    public string MPpotion03Name;
    public string MPpotion03Explain;
    public string parentMP03Path;

}
