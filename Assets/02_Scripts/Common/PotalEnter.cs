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
        if(other.gameObject.tag == "Player" && GameManager.GM.playerDataJson.Level >= playerLevel)
        {
            DataManager.dataInst.DataSave();
            GameManager.GM.playerDataJson.currentSceneIdx = startPointIdx;
            DataManager.dataInst.PlayerDataSave(GameManager.GM.playerDataJson);
            SceneMove.SceneInst.PotalMove(sceneIdx);
        }
    }
}
