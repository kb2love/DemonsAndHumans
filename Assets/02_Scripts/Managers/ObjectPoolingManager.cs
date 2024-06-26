using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager objInst;
    [SerializeField] PlayerData playerData;
    [SerializeField] MutantData mutantData;
    [SerializeField] ItemData itemData;
    [SerializeField] Transform items;
    List<GameObject> hitEffList = new List<GameObject>();
    List<GameObject> goldList = new List<GameObject>();
    List<GameObject> normalItemList = new List<GameObject>();
    List<GameObject> equipmentItemList = new List<GameObject>();
    private void Awake()
    {
        objInst = this;
    }
    private void Start()
    {
        CreateObejct("HitEffGroup", playerData.hitEff, 10, "HitEff", hitEffList);
        CreateObejct("GoldGroup", itemData.gold, 10, "Gold", goldList);
        CreateObejct("NormalItemGroup", itemData.normalItem, 10, "NormalItem", normalItemList);
        CreateObejct("EquipmentItemGroup", itemData.equipmentItem, 10, "EquipmentItem", equipmentItemList);
    }
    private void CreateObejct(string parentName, GameObject creatObj, int itemCount, string objName, List<GameObject> addList)
    {
        GameObject objParentGroup = new GameObject(parentName);
        objParentGroup.transform.SetParent(items.transform);
        for (int i = 0; i < itemCount; i++)
        {
            GameObject _obj = Instantiate(creatObj, objParentGroup.transform);
            _obj.name = objName + i.ToString() + "°³";
            _obj.SetActive(false);
            addList.Add(_obj);
        }
    }

    public GameObject GetHitEff()
    {
        foreach (GameObject hitEff in hitEffList)
        {
            if (!hitEff.activeSelf)
            {
                return hitEff;
            }
        }
        return null;
    }
    public GameObject GetGold()
    {
        foreach (GameObject gold in goldList)
        {
            if (!gold.activeSelf)
            {
                return gold;
            }
        }
        return null;
    }
    public GameObject GetNormalItem()
    {
        foreach (GameObject normalItem in normalItemList)
        {
            if (!normalItem.activeSelf)
            {
                return normalItem;
            }
        }
        return null;
    }
    public GameObject GetEquipmentItem()
    {
        foreach (GameObject equipmentItem in equipmentItemList)
        {
            if (!equipmentItem.activeSelf)
            {
                return equipmentItem;
            }
        }
        return null;
    }
}
