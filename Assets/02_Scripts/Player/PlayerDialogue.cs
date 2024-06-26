using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    bool isNPC = false;
    bool isDialogue = false;
    GameObject npc;
    string night = "Night";
    string paladin = "Paladin";
    public IEnumerator NPCSearch()
    {
        Debug.Log("qwe");
        while (isNPC)
        {
            yield return new WaitForSeconds(0.05f);
            RaycastHit hit;
            if (Physics.Raycast(transform.position + (Vector3.up * 0.85f), transform.forward, out hit, 5f, 1 << 6))
            {
                if (npc == null)
                {
                    hit.transform.GetComponentInParent<NPCOutLineOnOff>().OutLineOn();
                    isDialogue = true;
                    StartCoroutine(DialogueOn());
                    npc = hit.transform.GetComponentInParent<NPCOutLineOnOff>().gameObject;
                    Debug.Log(npc.name);
                }
            }
            else
            {
                if (npc != null)
                {
                    npc.transform.GetComponentInParent<NPCOutLineOnOff>().OutLineOff();
                    isDialogue = false;
                }
                npc = null;
            }
        }
    }

    IEnumerator DialogueOn()
    {
        while (isDialogue)
        {
            yield return new WaitForSeconds(0.005f);
            if (Input.GetKeyDown(KeyCode.G))
            {
                 if(npc != null && npc.tag == night)
                    npc.transform.GetComponentInParent<NPCDialogue>().TextBox();
                 else if(npc != null && npc.tag == paladin)
                    npc.transform.GetComponentInParent<NPCPaladinDialouge>().TextBox();
            }
        }
    }

    public void IsNPC(bool _isNPC)
    {
        isNPC = _isNPC;
    }

    public void IsDialogue(bool _isDialogue)
    {
        isDialogue = _isDialogue;
    }
}
