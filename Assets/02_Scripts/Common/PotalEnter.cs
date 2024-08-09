using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalEnter : MonoBehaviour
{
    [Header("��Ż ������ ���� Idx")]
    [SerializeField] int sceneIdx;
    [Header("��Ż ���� �� ��ŸƮ ����Ʈ�� Idx")]
    [SerializeField] int startPointIdx;
    [Header("�÷��̾� ����")]
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
