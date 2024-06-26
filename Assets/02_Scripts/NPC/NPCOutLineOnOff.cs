using cakeslice;
using System.Collections.Generic;
using UnityEngine;

public class NPCOutLineOnOff : MonoBehaviour
{
    [SerializeField] int childCount = 0;
    [SerializeField] List<Outline> npcOutLine = new List<Outline>();
    void Start()
    {
        for(int i = 0; i < childCount; i++)
        {
            npcOutLine.Add(transform.GetChild(i).GetComponent<Outline>());
        }
    }
    public void OutLineOn()
    {
        for(int i = 0;i < npcOutLine.Count;i++)
        {
            npcOutLine[i].eraseRenderer = false;
        }
    }
    public void OutLineOff()
    {
        for (int i = 0; i < npcOutLine.Count; i++)
        {
            npcOutLine[i].eraseRenderer = true;
        }
    }
}
