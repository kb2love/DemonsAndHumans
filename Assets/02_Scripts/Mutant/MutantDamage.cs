using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantDamage : MonoBehaviour
{
    [SerializeField] Image hpImage;
    [Header("체력")]
    [SerializeField] float maxHp;
    [SerializeField] Text hpText;
    [Header("경험치")]
    [SerializeField] int exp;
    [Header("퀘스트 클리어 횟수에 포함될 숫자")]
    [SerializeField] int questDataIdx;
    [Header("보스인지 아닌지")]
    [SerializeField] bool boss = false;
    float hp;
    MutantAI mutantAI;
    GameObject hitEff;
    bool isDie;
    bool mutantShield;
    private void Start()
    {
        mutantAI = GetComponent<MutantAI>();
    }
    private void OnEnable()
    {
        isDie = false;
        hp = maxHp;
        hpImage.fillAmount = hp / maxHp;
        if (hpText != null)
        {
            hpText.text = maxHp.ToString() + " : " + hp.ToString();
        }
    }
    public void MutantHit(float damage)
    {
        if (!isDie && !mutantShield)
        {
            hp -= damage;
            hpImage.fillAmount = hp / maxHp;
            if (hpText != null)
                hpText.text = maxHp.ToString() + " : " + hp.ToString();
            hitEff = ObjectPoolingManager.objInst.GetHitEff();
            hitEff.transform.position = transform.position + (Vector3.up * 0.8f);
            hitEff.SetActive(true);
            Debug.Log(hp);
            if (hp <= 0)
                MutantDie();
        }
        else if(mutantShield)
        {
            Debug.Log("막았죠?");
        }
    }
    void MutantDie()
    {
        mutantAI.IsDie(true);
        GameManager.GM.ExpUp(exp);
        QuestManager.questInst.UpdateKillCount(questDataIdx, boss);
        GenerateLoot();
        isDie = true;
    }
    void GenerateLoot()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 90f)
        {
            // 90% 확률로 gold_01 생성
            ItemDrop(ObjectPoolingManager.objInst.GetGold(), ItemData.ItemType.Gold);
        }
        else if (randomValue < 98f)
        {
            // 8% 확률로 normalItem 생성
            ItemData.ItemType type = GetNormalItemType();
            ItemDrop(ObjectPoolingManager.objInst.GetNormalItem(), type);
        }
        else if (randomValue < 100f)
        {
            // 2% 확률로 equipmentItem 생성
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
    public void IsMutantShield(bool isShield) { mutantShield = isShield; }
}
