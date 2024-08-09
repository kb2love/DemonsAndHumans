using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialData", menuName = "ScriptableObjects/Items/MaterialData", order = 11)]
public class MaterialData : ScriptableObject
{
    [Header("������ �Ӹ�")]
    public Sprite material01;
    public int material01Price;
    public string material01Name; 
    [Header("������ ����")]
    public Sprite material02;
    public int material02Price;
    public string material02Name; 
    [Header("�������载")]
    public Sprite material03;
    public int material03Price;
    public string material03Name; 
    [Header("��ġ�� ����")]
    public Sprite material04;
    public int material04Price;
    public string material04Name; 
    [Header("������ ����")]
    public Sprite material05;
    public int material05Price;
    public string material05Name; 
    [Header("��ġ�� ��")]
    public Sprite material06;
    public int material06Price;
    public string material06Name; 
    [Header("��ȹ�� ����")]
    public Sprite material07;
    public int material07Price;
    public string material07Name; 
    [Header("������ ����")]
    public Sprite material08;
    public int material08Price;
    public string material08Name; 
}
