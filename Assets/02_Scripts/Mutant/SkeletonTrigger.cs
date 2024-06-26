using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonTrigger : MonoBehaviour
{
    [SerializeField] List<SkeletonAI> skeletonAIs = new List<SkeletonAI>();
    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < skeletonAIs.Count; i++)
            {
                StartCoroutine(skeletonAIs[i].SkeletonMove());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            for (int i = 0; i < skeletonAIs.Count; i++)
            {
                skeletonAIs[i].Isout(true);
            }
        }
    }
}
