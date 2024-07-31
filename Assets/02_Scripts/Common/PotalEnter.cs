using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalEnter : MonoBehaviour
{
    [Header("포탈 입장할 씬의 Idx")]
    [SerializeField] int sceneIdx;
    [Header("포탈 입장 할 스타트 포인트의 Idx")]
    [SerializeField] int startPointIdx;
    [Header("플레이어 레벨")]
    [SerializeField] int playerLevel;
    [SerializeField] PlayerData playerData;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && playerData.Level >= playerLevel)
        {
            ItemManager.itemInst.AllItemSave();
            DataManager.dataInst.DataSave();
            playerData.playerSceneIdx = startPointIdx;
            SceneMove.SceneInst.PotalMove(sceneIdx);
        }
    }
}
