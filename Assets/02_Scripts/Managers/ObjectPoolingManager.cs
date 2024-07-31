using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager objInst;
    [SerializeField] PlayerData playerData;
    [SerializeField] SunDryItemData itemData;
    [SerializeField] Transform objects;
    Transform items;
    Transform hits;
    List<GameObject> stoneHitEffList = new List<GameObject>();
    List<GameObject> iceHitEffList = new List<GameObject>();
    List<GameObject> fireHitEffList = new List<GameObject>();
    List<GameObject> electroHitEffList = new List<GameObject>();
    List<GameObject> goldList = new List<GameObject>();
    List<GameObject> normalItemList = new List<GameObject>();
    List<GameObject> equipmentItemList = new List<GameObject>();
    private void Awake()
    {
        objInst = this;
    }
    public void Initialize()
    {
        items = objects.GetChild(0);
        hits = objects.GetChild(1);
        CreateObejct("HitEffGroup", playerData.hitEff, 10, "HitEff", stoneHitEffList, hits);
        CreateObejct("IceEffGroup", playerData.iceHitEff, 10, "IceHitEff", iceHitEffList, hits);
        CreateObejct("FireEffGroup", playerData.fireHitEff, 10, "FireHitEff", fireHitEffList, hits);
        CreateObejct("ElectroEffGroup", playerData.electroHitEff, 10, "ElectroHitEff", electroHitEffList, hits);
        CreateObejct("GoldGroup", itemData.gold, 10, "Gold", goldList, items);
        CreateObejct("NormalItemGroup", itemData.normalItem, 10, "NormalItem", normalItemList, items);
        CreateObejct("EquipmentItemGroup", itemData.equipmentItem, 10, "EquipmentItem", equipmentItemList, items);
    }
    private void CreateObejct(string parentName, GameObject creatObj, int itemCount, string objName, List<GameObject> addList, Transform tr)
    {
        GameObject objParentGroup = new GameObject(parentName);
        objParentGroup.transform.SetParent(tr.transform);
        for (int i = 0; i < itemCount; i++)
        {
            GameObject _obj = Instantiate(creatObj, objParentGroup.transform);
            _obj.name = objName + i.ToString() + "°³";
            _obj.SetActive(false);
            addList.Add(_obj);
        }
    }
    public GameObject GetHitEff() {  return RetrunGameObject(stoneHitEffList); }
    public GameObject GetIceHitEff() { return RetrunGameObject(iceHitEffList); }
    public GameObject GetFireHitEff() { return RetrunGameObject(fireHitEffList); }
    public GameObject GetElectroHitEff() { return RetrunGameObject(electroHitEffList); }
    public GameObject GetGold() { return RetrunGameObject(goldList); }
    public GameObject GetNormalItem() { return RetrunGameObject(normalItemList); }
    public GameObject GetEquipmentItem() { return RetrunGameObject(equipmentItemList); }
    private GameObject RetrunGameObject(List<GameObject> objects)
    {
        foreach (GameObject hitEff in objects)
        {
            if (!hitEff.activeSelf)
            {
                return hitEff;
            }
        }
        return null;
    }
}
