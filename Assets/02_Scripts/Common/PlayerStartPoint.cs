using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    [SerializeField] List<Transform> startPoint = new List<Transform>();
    [SerializeField] PlayerData playerData;
    Transform playerTr;
    private void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        playerTr.position = startPoint[playerData.playerSceneIdx].position;
        Camera.main.transform.parent.position = playerTr.transform.position;
    }
}
