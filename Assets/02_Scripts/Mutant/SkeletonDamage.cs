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

        if (randomValue < 11.11f)
        {
            return ItemData.ItemType.Hat;
        }
        else if (randomValue < 22.22f)
        {
            return ItemData.ItemType.Cloak;
        }
        else if (randomValue < 33.33f)
        {
            return ItemData.ItemType.Sword;
        }
        else if (randomValue < 44.44f)
        {
            return ItemData.ItemType.Shield;
        }
        else if (randomValue < 55.55f)
        {
            return ItemData.ItemType.Cloth;
        }
        else if (randomValue < 66.66f)
        {
            return ItemData.ItemType.Ring;
        }
        else if (randomValue < 77.77f)
        {
            return ItemData.ItemType.Pants;
        }
        else if (randomValue < 88.88f)
        {
            return ItemData.ItemType.Necklace;
        }
        else
        {
            return ItemData.ItemType.Shoes;
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
