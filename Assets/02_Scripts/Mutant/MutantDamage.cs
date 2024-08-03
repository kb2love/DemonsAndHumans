using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutantDamage : MonoBehaviour
{
    [SerializeField] Image hpImage;
    [Header("체력")]
    [SerializeField] float maxHp;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip itemDropClip;
    AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        isDie = false;
        hp = maxHp;
        hpImage.fillAmount = hp / maxHp;
        GetComponent<Collider>().enabled = true;
        if (hpText != null)
        {
            hpText.text = maxHp.ToString() + " : " + hp.ToString();
        }
    }
    public void MutantHit(float damage, SkillState skillState)
    {
        if (!isDie && !mutantShield)
        {
            hp -= damage;
            switch (skillState)
            {
                case SkillState.None: hitEff = ObjectPoolingManager.objInst.GetHitEff(); break;
                case SkillState.Ice: hitEff = ObjectPoolingManager.objInst.GetIceHitEff(); break;
                case SkillState.Fire: hitEff = ObjectPoolingManager.objInst.GetFireHitEff(); break;
                case SkillState.Electro: hitEff = ObjectPoolingManager.objInst.GetElectroHitEff(); break;
            }
            SoundManager.soundInst.EffectSoundPlay(audioSource, hitClip);
            hpImage.fillAmount = hp / maxHp;
            if (hpText != null)
                hpText.text = maxHp.ToString() + " : " + hp.ToString();
            hitEff.transform.position = transform.position + (Vector3.up * 0.8f);
            hitEff.SetActive(true);
            Debug.Log(hp);
            if (hp <= 0)
                MutantDie();
        }
        else if (mutantShield)
        {
            Debug.Log("막았죠?");
        }
    }
    void MutantDie()
    {
        mutantAI.IsDie(true);
        GetComponent<Collider>().enabled = false;
        GameManager.GM.ExpUp(exp);
        QuestManager.questInst.UpdateKillCount(questDataIdx, boss);
        if (!audioSource.isPlaying)
            SoundManager.soundInst.EffectSoundPlay(audioSource, itemDropClip);
        GenerateLoot();
        isDie = true;
    }
    void GenerateLoot()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 90f)
        {
            // 90% 확률로 Gold 생성
            ItemDrop(ObjectPoolingManager.objInst.GetGold(), ItemType.Gold);
        }
        else if (randomValue < 98f)
        {
            // 8% 확률로 normalItem 생성
            ItemType type = GetNormalItemType();
            ItemDrop(ObjectPoolingManager.objInst.GetNormalItem(), type);
        }
        else if (randomValue < 100f)
        {
            // 2% 확률로 equipmentItem 생성
            ItemType type = GetEquipmentItemType();
            ItemDrop(ObjectPoolingManager.objInst.GetEquipmentItem(), type);
        }
    }

    ItemType GetNormalItemType()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 33.33f)
        {
            return ItemType.HPPotion;
        }
        else if (randomValue < 66.66f)
        {
            return ItemType.MPPotion;
        }
        else
        {
            return ItemType.Material;
        }
    }

    ItemType GetEquipmentItemType()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 11.5f)
        {
            return ItemType.Hat;
        }
        else if (randomValue < 24f)
        {
            return ItemType.Kloak;
        }
        else if (randomValue < 36.5f)
        {
            return ItemType.Sword;
        }
        else if (randomValue < 49f)
        {
            return ItemType.Shield;
        }
        else if (randomValue < 61.5f)
        {
            return ItemType.Cloth;
        }
        else if (randomValue < 74f)
        {
            return ItemType.Shoes;
        }
        else if (randomValue < 86.5f)
        {
            return ItemType.Pants;
        }
        else if (randomValue < 99f)
        {
            return ItemType.Neck;
        }
        else
        {
            return ItemType.Ring;
        }
    }
    private void ItemDrop(GameObject obj, ItemType itemType)
    {
        GameObject item = obj;
        item.transform.position = transform.position;
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<ItemInfo>().type = itemType;
        item.SetActive(true);
    }
    public void IsMutantShield(bool isShield) { mutantShield = isShield; }
}
