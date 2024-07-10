using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonDamage : MonoBehaviour
{
    [SerializeField] MutantData mutantData;
    [SerializeField] Image hpImage;
    SkeletonAI skeletonAI;
    Animator animator;
    GameObject hitEff;
    float hp;
    private void Start()
    {
        hp = mutantData.skMaxHp;
        skeletonAI = GetComponent<SkeletonAI>();
        animator = GetComponent<Animator>();
    }
    public void SkeletonHit(float damage)
    {
        hp -= damage;
        hpImage.fillAmount = hp / mutantData.skMaxHp;
        hitEff = ObjectPoolingManager.objInst.GetHitEff();
        hitEff.transform.position = transform.position + (Vector3.up * 0.8f);
        hitEff.SetActive(true);
        skeletonAI.IsHit(true);
        animator.SetTrigger("HitTrigger");
        Debug.Log(hp);
        if (hp <= 0)
            SkeletonDie();
    }
    void SkeletonDie()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        skeletonAI.IsDie(true);
        GameManager.GM.ExpUp(50);
        GenerateLoot();
    }
    void GenerateLoot()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 90f)
        {
            // 90% 犬伏肺 gold 积己
            ItemDrop(ObjectPoolingManager.objInst.GetGold(), ItemData.ItemType.Gold);
        }
        else if (randomValue < 98f)
        {
            // 8% 犬伏肺 normalItem 积己
            ItemData.ItemType type = GetNormalItemType();
            ItemDrop(ObjectPoolingManager.objInst.GetNormalItem(), type);
        }
        else if (randomValue < 100f)
        {
            // 2% 犬伏肺 equipmentItem 积己
            ItemData.ItemType type = GetEquipmentItemType();
            ItemDrop(ObjectPoolingManager.objInst.GetEquipmentItem(), type);
        }
    }

    ItemData.ItemType GetNormalItemType()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 33.33f)
        {
            return ItemData.ItemType.HPPotion;
        }
        else if (randomValue < 66.66f)
        {
            return ItemData.ItemType.MPPotion;
        }
        else
        {
            return ItemData.ItemType.Material;
        }
    }

    ItemData.ItemType GetEquipmentItemType()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 11.5f)
        {
            return ItemData.ItemType.Hat;
        }
        else if (randomValue < 24f)
        {
            return ItemData.ItemType.Kloak;
        }
        else if (randomValue < 36.5f)
        {
            return ItemData.ItemType.Sword;
        }
        else if (randomValue < 49f)
        {
            return ItemData.ItemType.Shield;
        }
        else if (randomValue < 61.5f)
        {
            return ItemData.ItemType.Cloth;
        }
        else if (randomValue < 74f)
        {
            return ItemData.ItemType.Shoes;
        }
        else if (randomValue < 86.5f)
        {
            return ItemData.ItemType.Pants;
        }
        else if (randomValue < 99f)
        {
            return ItemData.ItemType.Neck;
        }
        else
        {
            return ItemData.ItemType.Ring;
        }
    }
    private void ItemDrop(GameObject obj, ItemData.ItemType itemType)
    {
        GameObject item = obj;
        item.transform.position = transform.position;
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<ItemInfo>().type = itemType;
        item.SetActive(true);
    }
}
