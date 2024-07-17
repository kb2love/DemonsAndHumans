using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "ScriptableObjects/Items/MaterialData", order = 11)]
public class MaterialData : ScriptableObject
{
    [Header("마족의 머리")]
    public Sprite material01;
    public int material01Price;
    public int material01Count;
    public int material01Idx;
    public string material01Name;
    public string material01Explain;
    public string parent01Path;
    [Header("마족의 정수")]
    public Sprite material02;
    public int material02Price;
    public int material02Count;
    public int material02Idx;
    public string material02Name;
    public string material02Explain;
    public string parent02Path;
    [Header("라이프배슬")]
    public Sprite material03;
    public int material03Price;
    public int material03Count;
    public int material03Idx;
    public string material03Name;
    public string material03Explain;
    public string parent03Path;
    [Header("리치의 심장")]
    public Sprite material04;
    public int material04Price;
    public int material04Count;
    public int material04Idx;
    public string material04Name;
    public string material04Explain;
    public string parent04Path;
    [Header("마족의 심장")]
    public Sprite material05;
    public int material05Price;
    public int material05Count;
    public int material05Idx;
    public string material05Name;
    public string material05Explain;
    public string parent05Path;
    [Header("리치의 핵")]
    public Sprite material06;
    public int material06Price;
    public int material06Count;
    public int material06Idx;
    public string material06Name;
    public string material06Explain;
    public string parent06Path;
    [Header("포획한 마족")]
    public Sprite material07;
    public int material07Price;
    public int material07Count;
    public int material07Idx;
    public string material07Name;
    public string material07Explain;
    public string parent07Path;
    [Header("마족의 상자")]
    public Sprite material08;
    public int material08Price;
    public int material08Count;
    public int material08Idx;
    public string material08Name;
    public string material08Explain;
    public string parent08Path;
}
