using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialougeBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerDialogue>().IsNPC(true);
            StartCoroutine(other.gameObject.GetComponent<PlayerDialogue>().NPCSearch());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerDialogue>().IsNPC(false);
        }
    }
}
