using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MutantTrigger : MonoBehaviour
{
    [SerializeField] List<MutantAI> mutantAI = new List<MutantAI>();
    [SerializeField] GameObject bossImage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < mutantAI.Count; i++)
            {
                mutantAI[i].MutantBehavior();
            }
            if(bossImage != null)
            {
                bossImage.SetActive(true);
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
            if (bossImage != null)
            {
                bossImage.SetActive(true);
            }
        }
    }
}
