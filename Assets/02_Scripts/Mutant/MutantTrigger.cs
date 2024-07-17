using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantTrigger : MonoBehaviour
{
    [SerializeField] List<MutantAI> mutantAI = new List<MutantAI>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < mutantAI.Count; i++)
            {
                StartCoroutine(mutantAI[i].MutantMove());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < mutantAI.Count; i++)
            {
                mutantAI[i].Isout(true);
            }
        }
    }
}
