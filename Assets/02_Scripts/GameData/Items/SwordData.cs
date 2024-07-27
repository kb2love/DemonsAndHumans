using UnityEngine;

[CreateAssetMenu(fileName = "SwordData", menuName = "ScriptableObjects/Items/SwordData", order = 2)]
public class SwordData : ScriptableObject
{
    [Header("·Õ¼Òµå")]
    public Sprite Image_01;
    public float Damage_01;
    public int Idx_01;
    public int Count_01;
    public string Name_01;

    [Header("¸¶°Ë")]
    public Sprite Image_02;
    public float Damage_02;
    public int Idx_02;
    public int Count_02;
    public string Name_02;

    [Header("¿µ¿õÀÇ °Ë")]
    public Sprite Image_03;
    public float Damage_03;
    public int Idx_03;
    public int Count_03;
    public string Name_03; 
}
