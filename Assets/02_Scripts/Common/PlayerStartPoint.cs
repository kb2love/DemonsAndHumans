using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    [SerializeField] List<Transform> startPoint = new List<Transform>();
    [SerializeField] PlayerData playerData;
    Transform playerTr;
    public void Initialize()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        playerTr.position = startPoint[playerData.playerSceneIdx].position;
        playerTr.rotation = startPoint[playerData.playerSceneIdx].rotation;
        Camera.main.transform.parent.position = playerTr.transform.position;
    }
}
