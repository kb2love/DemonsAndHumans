using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KloakData", menuName = "ScriptableObjects/Items/KloakData", order = 8)]
public class KloakData : ScriptableObject
{
    [Header("�ϻ����� ����")]
    public Sprite kloak01;
    public float kloak01HP;
    public float kloak01MP;
    public float kloak01Defence;
    public int kloak01Idx;
    public int kloak01Count;
    public string kloak01Name;
    public string kloak01Explain;
    public string parent01Path;
    [Header("������ ����")]
    public Sprite kloak02;
    public float kloak02HP;
    public float kloak02MP;
    public float kloak02Defence;
    public int kloak02Idx;
    public int kloak02Count;
    public string kloak02Name;
    public string kloak02Explain;
    public string parent02Path;
    [Header("������ ����")]
    public Sprite kloak03;
    public float kloak03HP;
    public float kloak03MP;
    public float kloak03Defence;
    public int kloak03Idx;
    public int kloak03Count;
    public string kloak03Name;
    public string kloak03Explain;
    public string parent03Path; 
}
