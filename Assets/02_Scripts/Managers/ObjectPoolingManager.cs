using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager objInst;
    [SerializeField] PlayerData playerData;
    [SerializeField] MutantData mutantData;
    List<GameObject> hitEffList = new List<GameObject>();
    private void Awake()
    {
        objInst = this;
    }
    private void Start()
    {
        CreateHitEffect();
    }

    private void CreateHitEffect()
    {
        GameObject hitEffGroup = new GameObject("HitEffGroup");
        for (int i = 0; i < 10; i++)
        {
            GameObject _hitEff = Instantiate(playerData.hitEff, hitEffGroup.transform);
            _hitEff.name = "HitEffect" + i.ToString() + "°³";
            _hitEff.SetActive(false);
            hitEffList.Add(_hitEff);
        }
    }
    private void CreateGoldt()
    {
        GameObject hitEffGroup = new GameObject("HitEffGroup");
        for (int i = 0; i < 10; i++)
        {
            GameObject _hitEff = Instantiate(playerData.hitEff, hitEffGroup.transform);
            _hitEff.name = "HitEffect" + i.ToString() + "°³";
            _hitEff.SetActive(false);
            hitEffList.Add(_hitEff);
        }
    }


    public GameObject GetHitEff()
    {
        foreach(GameObject hitEff in hitEffList)
        {
            if(!hitEff.activeSelf)
            {
                return hitEff;
            }
        }
        return null;
    }
}
