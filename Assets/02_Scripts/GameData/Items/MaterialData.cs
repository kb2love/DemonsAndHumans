using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "ScriptableObjects/Items/MaterialData", order = 11)]
public class MaterialData : ScriptableObject
{
    [Header("마족의 머리")]
    public Sprite material01;
    public int material01Price;
    public string material01Name; 
    [Header("마족의 정수")]
    public Sprite material02;
    public int material02Price;
    public string material02Name; 
    [Header("라이프배슬")]
    public Sprite material03;
    public int material03Price;
    public string material03Name; 
    [Header("리치의 심장")]
    public Sprite material04;
    public int material04Price;
    public string material04Name; 
    [Header("마족의 심장")]
    public Sprite material05;
    public int material05Price;
    public string material05Name; 
    [Header("리치의 핵")]
    public Sprite material06;
    public int material06Price;
    public string material06Name; 
    [Header("포획한 마족")]
    public Sprite material07;
    public int material07Price;
    public string material07Name; 
    [Header("마족의 상자")]
    public Sprite material08;
    public int material08Price;
    public string material08Name; 
}
