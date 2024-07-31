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
        if(other.gameObject.tag == "Player" && playerData.Level >= playerLevel)
        {
            ItemManager.itemInst.AllItemSave();
            DataManager.dataInst.DataSave();
            playerData.playerSceneIdx = startPointIdx;
            SceneMove.SceneInst.PotalMove(sceneIdx);
        }
    }
}
