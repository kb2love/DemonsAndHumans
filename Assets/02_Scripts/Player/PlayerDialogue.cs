using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    bool isNPC = false;
    bool isDialogue = false;
    GameObject npc;
    public IEnumerator NPCSearch()
    {
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
            yield return null;
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (npc != null && npc.tag == "Leader") npc.transform.GetComponentInParent<NPCLeader>().TextBox();
                else if (npc != null && npc.tag == "Maria") npc.transform.GetComponentInParent<NPCMaria>().TextBox();
                else if (npc != null && npc.tag == "Paladin") npc.transform.GetComponentInParent<NPCPaladinDialouge>().TextBox();
                else if (npc != null && npc.tag == "Store") { npc.transform.GetComponentInParent<NPCSotre>().TextBox(); }
                else if (npc != null && npc.tag == "Aki") { npc.transform.GetComponentInParent<NPCMutantKillerLeader>().TextBox();  }
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
