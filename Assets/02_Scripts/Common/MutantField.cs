using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantField : MonoBehaviour
{
    Transform playerTr;
    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        playerTr.position = this.transform.position;
        Camera.main.transform.parent.position = playerTr.transform.position;
    }
}
