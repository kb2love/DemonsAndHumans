using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using static ItemData;
using static UnityEditor.Progress;
public class ItemManager : MonoBehaviour
{
    public static ItemManager itemInst;
    // 아이템들에게 정보를 넣어줄 스크립터블 데이터들
    [Header("아이템 데이터")]
    [SerializeField] ItemData itemData;
    [SerializeField] SwordData swordData;
    [SerializeField] ShieldData shieldData;
    [SerializeField] HatData hatData;
    [SerializeField] ClothData clothData;
    [SerializeField] PantsData pantsData;
    [SerializeField] ShoesData shoesData;
    [SerializeField] KloakData kloakData;
    [SerializeField] NeckData neckData;
    [SerializeField] RIngData ringData;
    [SerializeField] PotionData potionData;
    [SerializeField] MaterialData materialData;
    [SerializeField] Transform inventory;
    [SerializeField] PlayerData playerData;
    // 플레이어가 소지중인 골드릁 인벤토리상에 나타낼 텍스트
    [Header("골드 텍스트")]
    [SerializeField] Text goldText;
    // 포션 마셧을때의 이펙트
    [Header("포션 이펙트")]
    [SerializeField] GameObject hpHealingEff;
    [SerializeField] GameObject mpHealingEff;
    [Header("HP,MP 이미지")]
    [SerializeField] Image hpImage;
    [SerializeField] Image mpImage;
    // 검과 방패를 온오프 할때 필요한 변수
    PlayerAttack playerAttack;
    // 퀵슬롯 아이템에 이벤트를 등록할 플레이어 변수
    PlayerUIController getItem;
    // 아이템들의 부모
    Transform itemGroup;
    // 아이템들의 리스트
    List<RectTransform> itemList = new List<RectTransform>();
    // 아이템들이 들어갈 리스트
    List<RectTransform> windowList = new List<RectTransform>();
    // 아이템 퀵슬롯 이미지리스트
    List<Transform> quickSlot = new List<Transform>();
    // 아이템들을 얻을때 확률을 조정할때필요한 변수
    private Dictionary<ItemData.ItemType, List<ItemDrop>> itemDropTable;
    bool isSelect;
    private void Awake()
    {
        itemInst = this;
    }
    private void Start()
    {
        Transform window = inventory.GetChild(0).transform;
        for (int i = 0; i < window.childCount; i++)
        {
            windowList.Add(window.GetChild(i).GetComponent<RectTransform>());
        }
        itemGroup = inventory.GetChild(1).transform;
        for (int i = 0; i < itemGroup.childCount; i++)
        {
            itemList.Add(itemGroup.GetChild(i).GetComponent<RectTransform>());
        }
        Transform quick = GameObject.Find("Panel_UseItem").transform;
        for (int i = 0; i < quick.childCount; i++)
        {
            quickSlot.Add(quick.GetChild(i).GetComponent<Transform>());
        }
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        getItem = playerAttack.GetComponent<PlayerUIController>();

    }
    public void Initialize()
    {
        InitializeItemDropTable();
        AllItemCheck();
    }
    #region 아이템 얻기
    //************** 검얻기****************//
    public void GetSword01() { CreateWeaponItem(ItemType.Sword, swordData.sword01, ref swordData.sword01Count, ref swordData.sword01Idx, new float[] { swordData.sword01Damage }, swordData.sword01Name, swordData.sword01Explain); }
    public void GetSword02() { CreateWeaponItem(ItemType.Sword, swordData.sword02, ref swordData.sword01Count, ref swordData.sword01Idx, new float[] { swordData.sword02Damage }, swordData.sword02Name, swordData.sword02Explain); }
    public void GetSword03() { CreateWeaponItem(ItemType.Sword, swordData.sword03, ref swordData.sword01Count, ref swordData.sword01Idx, new float[] { swordData.sword03Damage }, swordData.sword03Name, swordData.sword03Explain); }
    // 방패 얻기
    public void GetShield01() { CreateWeaponItem(ItemType.Shield, shieldData.shield01, ref shieldData.shield01Count, ref shieldData.shield01Idx, new float[] { shieldData.shield01Value }, shieldData.shield01Name, shieldData.shield01Explain); }
    public void GetShield02() { CreateWeaponItem(ItemType.Shield, shieldData.shield02, ref shieldData.shield02Count, ref shieldData.shield02Idx, new float[] { shieldData.shield02Value }, shieldData.shield02Name, shieldData.shield02Explain); }
    public void GetShield03() { CreateWeaponItem(ItemType.Shield, shieldData.shield03, ref shieldData.shield03Count, ref shieldData.shield03Idx, new float[] { shieldData.shield03Value }, shieldData.shield03Name, shieldData.shield03Explain); }
    public void GetShield04() { CreateWeaponItem(ItemType.Shield, shieldData.shield04, ref shieldData.shield04Count, ref shieldData.shield04Idx, new float[] { shieldData.shield04Value }, shieldData.shield04Name, shieldData.shield04Explain); }
    // 헬멧 얻기
    public void GetHat01() { CreateWeaponItem(ItemType.Hat, hatData.hat01, ref hatData.Hat01Count, ref hatData.hat01Idx, new float[] { hatData.hat01HP, hatData.hat01Defence }, hatData.hat01Name, hatData.hat01Explain); }
    public void GetHat02() { CreateWeaponItem(ItemType.Hat, hatData.hat02, ref hatData.Hat02Count, ref hatData.hat02Idx, new float[] { hatData.hat02HP, hatData.hat02Defence }, hatData.hat02Name, hatData.hat02Explain); }
    public void GetHat03() { CreateWeaponItem(ItemType.Hat, hatData.hat03, ref hatData.Hat03Count, ref hatData.hat03Idx, new float[] { hatData.hat03HP, hatData.hat03Defence }, hatData.hat03Name, hatData.hat03Explain); }
    public void GetHat04() { CreateWeaponItem(ItemType.Hat, hatData.hat04, ref hatData.Hat04Count, ref hatData.hat04Idx, new float[] { hatData.hat04HP, hatData.hat04Defence }, hatData.hat04Name, hatData.hat04Explain); }
    // 옷 얻기
    public void GetCloth01() { CreateWeaponItem(ItemType.Cloth, clothData.cloth01, ref clothData.cloth01Count, ref clothData.cloth01Idx, new float[] { clothData.cloth01HP, clothData.cloth01Defence }, clothData.cloth01Name, clothData.cloth01Explain); }
    public void GetCloth02() { CreateWeaponItem(ItemType.Cloth, clothData.cloth02, ref clothData.cloth02Count, ref clothData.cloth02Idx, new float[] { clothData.cloth02HP, clothData.cloth02Defence }, clothData.cloth02Name, clothData.cloth02Explain); }
    public void GetCloth03() { CreateWeaponItem(ItemType.Cloth, clothData.cloth03, ref clothData.cloth03Count, ref clothData.cloth03Idx, new float[] { clothData.cloth03HP, clothData.cloth03Defence }, clothData.cloth03Name, clothData.cloth03Explain); }
    // 바지 얻기
    public void GetPants01() { CreateWeaponItem(ItemType.Pants, pantsData.pants01, ref pantsData.pants01Count, ref pantsData.pants01Idx, new float[] { pantsData.pants01HP, pantsData.pants01Defence }, pantsData.pants01Name, pantsData.pants01Explain); }
    public void GetPants02() { CreateWeaponItem(ItemType.Pants, pantsData.pants02, ref pantsData.pants02Count, ref pantsData.pants02Idx, new float[] { pantsData.pants02HP, pantsData.pants02Defence }, pantsData.pants02Name, pantsData.pants02Explain); }
    public void GetPants03() { CreateWeaponItem(ItemType.Pants, pantsData.pants03, ref pantsData.pants03Count, ref pantsData.pants03Idx, new float[] { pantsData.pants03HP, pantsData.pants03Defence }, pantsData.pants03Name, pantsData.pants03Explain); }
    // 신발 얻기
    public void GetShoes01() { CreateWeaponItem(ItemType.Shoes, shoesData.shoes01, ref shoesData.shoes01Count, ref shoesData.shoes01Idx, new float[] { shoesData.shoes01HP, shoesData.shoes01Defence }, shoesData.shoes01Name, shoesData.shoes01Explain); }
    public void GetShoes02() { CreateWeaponItem(ItemType.Shoes, shoesData.shoes02, ref shoesData.shoes02Count, ref shoesData.shoes02Idx, new float[] { shoesData.shoes02HP, shoesData.shoes02Defence }, shoesData.shoes02Name, shoesData.shoes02Explain); }
    public void GetShoes03() { CreateWeaponItem(ItemType.Shoes, shoesData.shoes03, ref shoesData.shoes03Count, ref shoesData.shoes03Idx, new float[] { shoesData.shoes03HP, shoesData.shoes03Defence }, shoesData.shoes03Name, shoesData.shoes03Explain); }
    // 망토 얻기
    public void GetKloak01() { CreateWeaponItem(ItemType.Kloak, kloakData.kloak01, ref kloakData.kloak01Count, ref kloakData.kloak01Idx, new float[] { kloakData.kloak01HP, kloakData.kloak01MP, kloakData.kloak01Defence }, kloakData.kloak01Name, kloakData.kloak01Explain); }
    public void GetKloak02() { CreateWeaponItem(ItemType.Kloak, kloakData.kloak02, ref kloakData.kloak02Count, ref kloakData.kloak02Idx, new float[] { kloakData.kloak02HP, kloakData.kloak02MP, kloakData.kloak02Defence }, kloakData.kloak02Name, kloakData.kloak02Explain); }
    public void GetKloak03() { CreateWeaponItem(ItemType.Kloak, kloakData.kloak03, ref kloakData.kloak03Count, ref kloakData.kloak03Idx, new float[] { kloakData.kloak03HP, kloakData.kloak03MP, kloakData.kloak03Defence }, kloakData.kloak03Name, kloakData.kloak03Explain); }
    // 목걸이 얻기
    public void GetNeck01() { CreateWeaponItem(ItemType.Neck, neckData.neck01, ref neckData.neck01Count, ref neckData.neck01Idx, new float[] { neckData.neck01MP, neckData.neck01Damage, neckData.neck01MagicDamage }, neckData.neck01Name, neckData.neck01Explain); }
    public void GetNeck02() { CreateWeaponItem(ItemType.Neck, neckData.neck02, ref neckData.neck02Count, ref neckData.neck02Idx, new float[] { neckData.neck02MP, neckData.neck02Damage, neckData.neck02MagicDamage }, neckData.neck02Name, neckData.neck02Explain); }
    public void GetNeck03() { CreateWeaponItem(ItemType.Neck, neckData.neck03, ref neckData.neck03Count, ref neckData.neck03Idx, new float[] { neckData.neck03MP, neckData.neck03Damage, neckData.neck03MagicDamage }, neckData.neck03Name, neckData.neck03Explain); }
    public void GetNeck04() { CreateWeaponItem(ItemType.Neck, neckData.neck04, ref neckData.neck04Count, ref neckData.neck04Idx, new float[] { neckData.neck04MP, neckData.neck04Damage, neckData.neck04MagicDamage }, neckData.neck04Name, neckData.neck04Explain); }
    // 반지 얻기
    public void GetRing01() { CreateWeaponItem(ItemType.Ring, ringData.ring01, ref ringData.ring01Count, ref ringData.ring01Idx, new float[] { ringData.ring01HP, ringData.ring01MP, ringData.ring01Damage, ringData.ring01Defence, ringData.ring01MagicDamage, ringData.ring01FatalProbability, ringData.ring01FatalValue }, ringData.ring01Name, ringData.ring01Explain); }
    public void GetRing02() { CreateWeaponItem(ItemType.Ring, ringData.ring02, ref ringData.ring02Count, ref ringData.ring02Idx, new float[] { ringData.ring02HP, ringData.ring02MP, ringData.ring02Damage, ringData.ring02Defence, ringData.ring02MagicDamage, ringData.ring02FatalProbability, ringData.ring02FatalValue }, ringData.ring02Name, ringData.ring02Explain); }


    //************** HP 포션 얻기 ****************//
    public void GetHPpotion01()
    {
        potionData.HPpotion01Count++;
        if (potionData.HPpotion01Count > 1)
        {
            itemList[potionData.HPpotion01Idx].GetComponentInChildren<Text>().text = potionData.HPpotion01Count.ToString();
        }
        else
        {
            potionData.HPpotion01Idx = itemList.Count;
            CreatItemPotionMatItem(ItemType.HPPotion, potionData.HPpotion01, potionData.HPpotion01Count, ref potionData.HPpotion01Idx, potionData.HPpotion01Name, potionData.HPpotion01Explain, potionData.HPpotion01quick, HPHeal01);
        }
    }

    public void GetHPpotion02()
    {
        potionData.HPpotion02Count++;
        if (potionData.HPpotion02Count > 1)
        {
            itemList[potionData.HPpotion02Idx].GetComponentInChildren<Text>().text = potionData.HPpotion02Count.ToString();
        }
        else
        {
            potionData.HPpotion02Idx = itemList.Count;
            CreatItemPotionMatItem(ItemType.HPPotion, potionData.HPpotion02, potionData.HPpotion02Count, ref potionData.HPpotion02Idx, potionData.HPpotion02Name, potionData.HPpotion02Explain, potionData.HPpotion02quick, HPHeal02);
        }
    }

    public void GetHPpotion03()
    {
        potionData.HPpotion03Count++;
        if (potionData.HPpotion03Count > 1)
        {
            itemList[potionData.HPpotion03Idx].GetComponentInChildren<Text>().text = potionData.HPpotion03Count.ToString();
        }
        else
        {
            potionData.HPpotion03Idx = itemList.Count;
            CreatItemPotionMatItem(ItemType.HPPotion, potionData.HPpotion03, potionData.HPpotion03Count, ref potionData.HPpotion03Idx, potionData.HPpotion03Name, potionData.HPpotion03Explain, potionData.HPpotion03quick, HPHeal03);
        }
    }

    //************** MP 포션 얻기 ****************//
    public void GetMPpotion01()
    {
        potionData.MPpotion01Count++;
        if (potionData.MPpotion01Count > 1)
        {
            itemList[potionData.MPpotion01Idx].GetComponentInChildren<Text>().text = potionData.MPpotion01Count.ToString();
        }
        else
        {
            potionData.MPpotion01Idx = itemList.Count;
            CreatItemPotionMatItem(ItemType.MPPotion, potionData.MPpotion01, potionData.MPpotion01Count, ref potionData.MPpotion01Idx, potionData.MPpotion01Name, potionData.MPpotion01Explain, potionData.MPpotion01quick, MPHeal01);
        }
    }

    public void GetMPpotion02()
    {
        potionData.MPpotion02Count++;
        if (potionData.MPpotion02Count > 1)
        {
            itemList[potionData.MPpotion02Idx].GetComponentInChildren<Text>().text = potionData.MPpotion02Count.ToString();
        }
        else
        {
            potionData.MPpotion02Idx = itemList.Count;
            CreatItemPotionMatItem(ItemType.MPPotion, potionData.MPpotion02, potionData.MPpotion02Count, ref potionData.MPpotion02Idx, potionData.MPpotion02Name, potionData.MPpotion02Explain, potionData.MPpotion02quick, MPHeal02);
        }
    }

    public void GetMPpotion03()
    {
        potionData.MPpotion03Count++;
        if (potionData.MPpotion03Count > 1)
        {
            itemList[potionData.MPpotion03Idx].GetComponentInChildren<Text>().text = potionData.MPpotion03Count.ToString();
        }
        else
        {
            potionData.MPpotion03Idx = itemList.Count;
            CreatItemPotionMatItem(ItemType.MPPotion, potionData.MPpotion03, potionData.MPpotion03Count, ref potionData.MPpotion03Idx, potionData.MPpotion03Name, potionData.MPpotion03Explain, potionData.MPpotion03quick, MPHeal03);
        }
    }

    //************** 재료 얻기 ****************//
    public void GetMaterial01()
    {
        materialData.material01Count++;
        if (materialData.material01Count > 1)
        {
            itemList[materialData.material01Idx].GetComponentInChildren<Text>().text = materialData.material01Count.ToString();
        }
        else
        {
            materialData.material01Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material01, ref materialData.material01Count, ref materialData.material01Idx, null, materialData.material01Name, materialData.material01Explain);
        }
    }

    public void GetMaterial02()
    {
        materialData.material02Count++;
        if (materialData.material02Count > 1)
        {
            itemList[materialData.material02Idx].GetComponentInChildren<Text>().text = materialData.material02Count.ToString();
        }
        else
        {
            materialData.material02Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material02, ref materialData.material02Count, ref materialData.material02Idx, null, materialData.material02Name, materialData.material02Explain);
        }
    }

    public void GetMaterial03()
    {
        materialData.material03Count++;
        if (materialData.material03Count > 1)
        {
            itemList[materialData.material03Idx].GetComponentInChildren<Text>().text = materialData.material03Count.ToString();
        }
        else
        {
            materialData.material03Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material03, ref materialData.material03Count, ref materialData.material03Idx, null, materialData.material03Name, materialData.material03Explain);
        }
    }
    public void GetMaterial04()
    {
        materialData.material04Count++;
        if (materialData.material04Count > 1)
        {
            itemList[materialData.material04Idx].GetComponentInChildren<Text>().text = materialData.material04Count.ToString();
        }
        else
        {
            materialData.material04Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material04, ref materialData.material04Count, ref materialData.material04Idx, null, materialData.material04Name, materialData.material04Explain);
        }
    }

    public void GetMaterial05()
    {
        materialData.material05Count++;
        if (materialData.material05Count > 1)
        {
            itemList[materialData.material05Idx].GetComponentInChildren<Text>().text = materialData.material05Count.ToString();
        }
        else
        {
            materialData.material05Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material05, ref materialData.material05Count, ref materialData.material05Idx, null, materialData.material05Name, materialData.material05Explain);
        }
    }

    public void GetMaterial06()
    {
        materialData.material06Count++;
        if (materialData.material06Count > 1)
        {
            itemList[materialData.material06Idx].GetComponentInChildren<Text>().text = materialData.material06Count.ToString();
        }
        else
        {
            materialData.material06Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material06, ref materialData.material06Count, ref materialData.material06Idx, null, materialData.material06Name, materialData.material06Explain);
        }
    }

    public void GetMaterial07()
    {
        materialData.material07Count++;
        if (materialData.material07Count > 1)
        {
            itemList[materialData.material07Idx].GetComponentInChildren<Text>().text = materialData.material07Count.ToString();
        }
        else
        {
            materialData.material07Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material07, ref materialData.material07Count, ref materialData.material07Idx, null, materialData.material07Name, materialData.material07Explain);
        }
    }

    public void GetMaterial08()
    {
        materialData.material08Count++;
        if (materialData.material08Count > 1)
        {
            itemList[materialData.material08Idx].GetComponentInChildren<Text>().text = materialData.material08Count.ToString();
        }
        else
        {
            materialData.material08Idx = itemList.Count;
            CreateWeaponItem(ItemType.Material, materialData.material08, ref materialData.material08Count, ref materialData.material08Idx, null, materialData.material08Name, materialData.material08Explain);
        }
    }
    #endregion
    #region 아이템 얻는 메소드
    public void GetItem(ItemData.ItemType itemType)
    {
        if (itemType == ItemData.ItemType.Gold)
        {
            GoldPlus(Random.Range(1, 10));
            return;
        }

        if (itemDropTable.TryGetValue(itemType, out var itemDrops))
        {
            float randomValue = Random.Range(0f, 100f);
            foreach (var itemDrop in itemDrops)
            {
                if (randomValue <= itemDrop.dropRate)
                {
                    itemDrop.getItemAction.Invoke();
                    break;
                }
            }
            AllItemTrSave();
        }
    }
    private void InitializeItemDropTable()
    {
        itemDropTable = new Dictionary<ItemData.ItemType, List<ItemDrop>>
        {
            {
                ItemData.ItemType.Sword, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 80.0f, getItemAction = GetSword01 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetSword02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetSword03 }
                }
            },
            {
                ItemData.ItemType.Shield, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 60.0f, getItemAction = GetShield01 },
                    new ItemDrop { dropRate = 85.0f, getItemAction = GetShield02 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetShield03 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetShield04 }
                }
            },
            {
                ItemData.ItemType.Hat, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 60.0f, getItemAction = GetHat01 },
                    new ItemDrop { dropRate = 85.0f, getItemAction = GetHat02 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetHat03 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetHat04 }
                }
            },
            {
                ItemData.ItemType.Cloth, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetCloth01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetCloth02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetCloth03 }
                }
            },
            {
                ItemData.ItemType.Pants, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetPants01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetPants02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetPants03 }
                }
            },
            {
                ItemData.ItemType.Shoes, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetShoes01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetShoes02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetShoes03 }
                }
            },
            {
                ItemData.ItemType.Kloak, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetKloak01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetKloak02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetKloak03 }
                }
            },
            {
                ItemData.ItemType.Neck, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 60.0f, getItemAction = GetNeck01 },
                    new ItemDrop { dropRate = 85.0f, getItemAction = GetNeck02 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetNeck03 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetNeck04 }
                }
            },
            {
                ItemData.ItemType.Ring, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 80.0f, getItemAction = GetRing01 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetRing01 }
                }
            },
            {
                ItemData.ItemType.HPPotion, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetHPpotion01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetHPpotion02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetHPpotion03 }
                }
            },
            {
                ItemData.ItemType.MPPotion, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetMPpotion01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetMPpotion02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetMPpotion03 }
                }
            },
            {
                ItemData.ItemType.Material, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 12.5f, getItemAction = GetMaterial01 },
                    new ItemDrop { dropRate = 12.5f, getItemAction = GetMaterial01 },
                    new ItemDrop { dropRate = 25f, getItemAction = GetMaterial02 },
                    new ItemDrop { dropRate = 37.5f, getItemAction = GetMaterial03 },
                    new ItemDrop { dropRate = 50f, getItemAction = GetMaterial04 },
                    new ItemDrop { dropRate = 62.5f, getItemAction = GetMaterial05 },
                    new ItemDrop { dropRate = 75f, getItemAction = GetMaterial06 },
                    new ItemDrop { dropRate = 87.5f, getItemAction = GetMaterial07 },
                    new ItemDrop { dropRate = 100f, getItemAction = GetMaterial08 }
                }
            }
        };
    }
    public void CreateWeaponItem(ItemType itemType, Sprite itemSprite, ref int itemCount, ref int itemIdx,
    float[] values, string itemName, string itemExplain)
    {
        itemCount++;
        RectTransform window = GetWindowList();
        for (int i = 0; i < itemList.Count; i++)
        {
            if (!itemList[i].gameObject.activeSelf)
            {
                itemIdx = i;
                break;
            }
        }

        var item = itemList[itemIdx];
        item.GetComponent<Image>().sprite = itemSprite;
        item.GetComponent<ItemInfo>().type = itemType;

        switch (itemType)
        {
            case ItemType.Sword:
                item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                break;
            case ItemType.Shield:
                item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                break;
            case ItemType.Hat:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                break;
            case ItemType.Cloth:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                break;
            case ItemType.Pants:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                break;
            case ItemType.Shoes:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                break;
            case ItemType.Kloak:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                break;
            case ItemType.Neck:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                break;
            case ItemType.Ring:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                break;
        }

        item.GetComponent<ItemToolTip>().itemName = itemName;
        item.GetComponent<ItemToolTip>().itemExplain = itemExplain;
        item.SetParent(window);
        item.gameObject.SetActive(true);
    }
    void CreatItemPotionMatItem(ItemType itemType, Sprite itemSprite, int itemCount, ref int itemIdx, string itemName, string itemExplain, int priceAndQuickIdx, UnityAction onClickAction = null)
    {
        RectTransform window = GetWindowList();
        for (int i = 0; i < itemList.Count; i++)
        {
            if (!itemList[i].gameObject.activeSelf)
            {
                itemIdx = i;
                break;
            }
        }
        var item = itemList[itemIdx];
        item.GetComponent<Image>().sprite = itemSprite;
        item.GetComponent<ItemInfo>().type = itemType;
        int j = itemIdx;
        switch (itemType)
        {
            case ItemType.HPPotion:
                var text = Instantiate(itemData.itemCountText, item);
                text.GetComponent<Text>().text = itemCount.ToString();
                item.AddComponent<Button>();
                item.GetComponent<Button>().onClick.AddListener(onClickAction);
                EventTrigger eventTrigger = item.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data, onClickAction, j); });
                eventTrigger.triggers.Add(entry);
                break;
            case ItemType.MPPotion:
                var text02 = Instantiate(itemData.itemCountText, item);
                text02.GetComponent<Text>().text = itemCount.ToString();
                item.AddComponent<Button>();
                item.GetComponent<Button>().onClick.AddListener(onClickAction);
                EventTrigger MPeventTrigger = item.AddComponent<EventTrigger>();
                EventTrigger.Entry MPentry = new EventTrigger.Entry();
                MPentry.eventID = EventTriggerType.PointerDown;
                MPentry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data, onClickAction, j); });
                MPeventTrigger.triggers.Add(MPentry);
                break;
            case ItemType.Material:
                var matText = Instantiate(itemData.itemCountText, item);
                matText.GetComponent<Text>().text = itemCount.ToString();
                item.GetComponent<Drag>().price = priceAndQuickIdx;
                break;
        }
        item.GetComponent<ItemToolTip>().itemName = itemName;
        item.GetComponent<ItemToolTip>().itemExplain = itemExplain;
        item.SetParent(window);
        item.gameObject.SetActive(true);
    }
    #endregion
    #region 아이템 체크 및 위치 저장
    public void AllItemCheck()
    {
        // 아이템 체크
        ItemCheck(ItemType.Sword, ref swordData.sword01Count, swordData.sword01Idx, GetSword01, swordData.parent01Path, new float[] { swordData.sword01Damage }, ref itemData.isSword);
        ItemCheck(ItemType.Sword, ref swordData.sword02Count, swordData.sword02Idx, GetSword02, swordData.parent02Path, new float[] { swordData.sword02Damage }, ref itemData.isSword);
        ItemCheck(ItemType.Sword, ref swordData.sword03Count, swordData.sword03Idx, GetSword03, swordData.parent03Path, new float[] { swordData.sword03Damage }, ref itemData.isSword);
        ItemCheck(ItemType.Shield, ref shieldData.shield01Count, shieldData.shield01Idx, GetShield01, shieldData.parent01Path, new float[] { shieldData.shield01Value }, ref itemData.isShield);
        ItemCheck(ItemType.Shield, ref shieldData.shield02Count, shieldData.shield02Idx, GetShield02, shieldData.parent02Path, new float[] { shieldData.shield02Value }, ref itemData.isShield);
        ItemCheck(ItemType.Shield, ref shieldData.shield03Count, shieldData.shield03Idx, GetShield03, shieldData.parent03Path, new float[] { shieldData.shield03Value }, ref itemData.isShield);
        ItemCheck(ItemType.Shield, ref shieldData.shield04Count, shieldData.shield04Idx, GetShield04, shieldData.parent04Path, new float[] { shieldData.shield04Value }, ref itemData.isShield);
        ItemCheck(ItemType.Hat, ref hatData.Hat01Count, hatData.hat01Idx, GetHat01, hatData.parent01Path, new float[] { hatData.hat01HP, hatData.hat01Defence }, ref itemData.isHat);
        ItemCheck(ItemType.Hat, ref hatData.Hat02Count, hatData.hat02Idx, GetHat02, hatData.parent02Path, new float[] { hatData.hat02HP, hatData.hat02Defence }, ref itemData.isHat);
        ItemCheck(ItemType.Hat, ref hatData.Hat03Count, hatData.hat03Idx, GetHat03, hatData.parent03Path, new float[] { hatData.hat03HP, hatData.hat03Defence }, ref itemData.isHat);
        ItemCheck(ItemType.Hat, ref hatData.Hat04Count, hatData.hat04Idx, GetHat04, hatData.parent04Path, new float[] { hatData.hat04HP, hatData.hat04Defence }, ref itemData.isHat);
        ItemCheck(ItemType.Cloth, ref clothData.cloth01Count, clothData.cloth01Idx, GetCloth01, clothData.parent01Path, new float[] { clothData.cloth01HP, clothData.cloth01Defence }, ref itemData.isCloth);
        ItemCheck(ItemType.Cloth, ref clothData.cloth02Count, clothData.cloth02Idx, GetCloth02, clothData.parent02Path, new float[] { clothData.cloth02HP, clothData.cloth02Defence }, ref itemData.isCloth);
        ItemCheck(ItemType.Cloth, ref clothData.cloth03Count, clothData.cloth03Idx, GetCloth03, clothData.parent03Path, new float[] { clothData.cloth03HP, clothData.cloth03Defence }, ref itemData.isCloth);
        ItemCheck(ItemType.Pants, ref pantsData.pants01Count, pantsData.pants01Idx, GetPants01, pantsData.parent01Path, new float[] { pantsData.pants01HP, pantsData.pants01Defence }, ref itemData.isPants);
        ItemCheck(ItemType.Pants, ref pantsData.pants02Count, pantsData.pants02Idx, GetPants02, pantsData.parent02Path, new float[] { pantsData.pants02HP, pantsData.pants02Defence }, ref itemData.isPants);
        ItemCheck(ItemType.Pants, ref pantsData.pants03Count, pantsData.pants03Idx, GetPants03, pantsData.parent03Path, new float[] { pantsData.pants03HP, pantsData.pants03Defence }, ref itemData.isPants);
        ItemCheck(ItemType.Shoes, ref shoesData.shoes01Count, shoesData.shoes01Idx, GetShoes01, shoesData.parent01Path, new float[] { shoesData.shoes01HP, shoesData.shoes01Defence }, ref itemData.isShoes);
        ItemCheck(ItemType.Shoes, ref shoesData.shoes02Count, shoesData.shoes02Idx, GetShoes02, shoesData.parent02Path, new float[] { shoesData.shoes02HP, shoesData.shoes02Defence }, ref itemData.isShoes);
        ItemCheck(ItemType.Shoes, ref shoesData.shoes03Count, shoesData.shoes03Idx, GetShoes03, shoesData.parent03Path, new float[] { shoesData.shoes03HP, shoesData.shoes03Defence }, ref itemData.isShoes);
        ItemCheck(ItemType.Kloak, ref kloakData.kloak01Count, kloakData.kloak01Idx, GetKloak01, kloakData.parent01Path, new float[] { kloakData.kloak01HP, kloakData.kloak01MP, kloakData.kloak01Defence}, ref itemData.isKloak);
        ItemCheck(ItemType.Kloak, ref kloakData.kloak02Count, kloakData.kloak02Idx, GetKloak02, kloakData.parent02Path, new float[] { kloakData.kloak02HP, kloakData.kloak02MP, kloakData.kloak02Defence }, ref itemData.isKloak);
        ItemCheck(ItemType.Kloak, ref kloakData.kloak03Count, kloakData.kloak03Idx, GetKloak03, kloakData.parent03Path, new float[] { kloakData.kloak03HP, kloakData.kloak03MP, kloakData.kloak03Defence }, ref itemData.isKloak);
        ItemCheck(ItemType.Neck, ref neckData.neck01Count, neckData.neck01Idx, GetNeck01, neckData.parent01Path, new float[] { neckData.neck01MP, neckData.neck01Damage, neckData.neck01MagicDamage }, ref itemData.isNeck);
        ItemCheck(ItemType.Neck, ref neckData.neck02Count, neckData.neck02Idx, GetNeck02, neckData.parent02Path, new float[] { neckData.neck02MP, neckData.neck02Damage, neckData.neck02MagicDamage }, ref itemData.isNeck);
        ItemCheck(ItemType.Neck, ref neckData.neck03Count, neckData.neck03Idx, GetNeck03, neckData.parent03Path, new float[] { neckData.neck03MP, neckData.neck03Damage, neckData.neck03MagicDamage }, ref itemData.isNeck);
        ItemCheck(ItemType.Neck, ref neckData.neck04Count, neckData.neck04Idx, GetNeck04, neckData.parent04Path, new float[] { neckData.neck04MP, neckData.neck04Damage, neckData.neck04MagicDamage }, ref itemData.isNeck);
        ItemCheck(ItemType.Ring,ref ringData.ring01Count, ringData.ring01Idx, GetRing01, ringData.parent01Path, 
            new float[] { ringData.ring01HP, ringData.ring01MP, ringData.ring01Defence, ringData.ring01FatalValue, ringData.ring01FatalProbability, ringData.ring01MagicDamage, ringData.ring01Damage }, ref itemData.isRing);
        ItemCheck(ItemType.Ring, ref ringData.ring02Count, ringData.ring02Idx, GetRing02, ringData.parent02Path,
            new float[] { ringData.ring02HP, ringData.ring02MP, ringData.ring02Defence, ringData.ring02FatalValue, ringData.ring02FatalProbability, ringData.ring02MagicDamage, ringData.ring02Damage }, ref itemData.isRing);

        CheckPotion(ItemType.HPPotion, potionData.HPpotion01, ref potionData.HPpotion01Count, ref potionData.HPpotion01Idx, potionData.HPpotion01quick,
             potionData.HPpotion01Name, potionData.HPpotion01Explain, potionData.parent01Path, HPHeal01);
        CheckPotion(ItemType.HPPotion, potionData.HPpotion02, ref potionData.HPpotion02Count, ref potionData.HPpotion02Idx, potionData.HPpotion02quick,
             potionData.HPpotion02Name, potionData.HPpotion02Explain, potionData.parent02Path, HPHeal02);
        CheckPotion(ItemType.HPPotion, potionData.HPpotion03, ref potionData.HPpotion03Count, ref potionData.HPpotion03Idx, potionData.HPpotion03quick,
            potionData.HPpotion03Name, potionData.HPpotion03Explain, potionData.parent03Path, HPHeal03);
        CheckPotion(ItemType.HPPotion, potionData.MPpotion01, ref potionData.MPpotion01Count, ref potionData.MPpotion01Idx, potionData.MPpotion01quick,
             potionData.MPpotion01Name, potionData.MPpotion01Explain, potionData.parentMP01Path, MPHeal01);
        CheckPotion(ItemType.HPPotion, potionData.MPpotion02, ref potionData.MPpotion02Count, ref potionData.MPpotion02Idx, potionData.MPpotion02quick,
             potionData.MPpotion02Name, potionData.MPpotion02Explain, potionData.parentMP02Path, MPHeal02);
        CheckPotion(ItemType.HPPotion, potionData.MPpotion03, ref potionData.MPpotion03Count, ref potionData.MPpotion03Idx, potionData.MPpotion03quick,
             potionData.MPpotion03Name, potionData.MPpotion03Explain, potionData.parentMP03Path, MPHeal03);

        CheckMaterial(ItemType.Material, materialData.material01, materialData.material01Count, ref materialData.material01Idx, materialData.material01Name, materialData.material01Explain, materialData.parent01Path, materialData.material01Price);
        CheckMaterial(ItemType.Material, materialData.material02, materialData.material02Count, ref materialData.material02Idx, materialData.material02Name, materialData.material02Explain, materialData.parent02Path, materialData.material02Price);
        CheckMaterial(ItemType.Material, materialData.material03, materialData.material03Count, ref materialData.material03Idx, materialData.material03Name, materialData.material03Explain, materialData.parent03Path, materialData.material03Price);
        CheckMaterial(ItemType.Material, materialData.material04, materialData.material04Count, ref materialData.material04Idx, materialData.material04Name, materialData.material04Explain, materialData.parent04Path, materialData.material04Price);
        CheckMaterial(ItemType.Material, materialData.material05, materialData.material05Count, ref materialData.material05Idx, materialData.material05Name, materialData.material05Explain, materialData.parent05Path, materialData.material05Price);
        CheckMaterial(ItemType.Material, materialData.material06, materialData.material06Count, ref materialData.material06Idx, materialData.material06Name, materialData.material06Explain, materialData.parent06Path, materialData.material06Price);
        CheckMaterial(ItemType.Material, materialData.material07, materialData.material07Count, ref materialData.material07Idx, materialData.material07Name, materialData.material07Explain, materialData.parent07Path, materialData.material07Price);
        CheckMaterial(ItemType.Material, materialData.material08, materialData.material08Count, ref materialData.material08Idx, materialData.material08Name, materialData.material08Explain, materialData.parent08Path, materialData.material08Price);
        AllItemTrSave();
    }
    public void AllItemTrSave()
    {
        ItemTrSave(swordData.sword01Count, swordData.sword01Idx, ref swordData.parent01Path);
        ItemTrSave(swordData.sword02Count, swordData.sword02Idx, ref swordData.parent02Path);
        ItemTrSave(swordData.sword03Count, swordData.sword03Idx, ref swordData.parent03Path);

        ItemTrSave(shieldData.shield01Count, shieldData.shield01Idx, ref shieldData.parent01Path);
        ItemTrSave(shieldData.shield02Count, shieldData.shield02Idx, ref shieldData.parent02Path);
        ItemTrSave(shieldData.shield03Count, shieldData.shield03Idx, ref shieldData.parent03Path);
        ItemTrSave(shieldData.shield04Count, shieldData.shield04Idx, ref shieldData.parent04Path);

        ItemTrSave(hatData.Hat01Count, hatData.hat01Idx, ref hatData.parent01Path);
        ItemTrSave(hatData.Hat02Count, hatData.hat02Idx, ref hatData.parent02Path);
        ItemTrSave(hatData.Hat03Count, hatData.hat03Idx, ref hatData.parent03Path);
        ItemTrSave(hatData.Hat04Count, hatData.hat04Idx, ref hatData.parent04Path);

        ItemTrSave(clothData.cloth01Count, clothData.cloth01Idx, ref clothData.parent01Path);
        ItemTrSave(clothData.cloth02Count, clothData.cloth02Idx, ref clothData.parent02Path);
        ItemTrSave(clothData.cloth03Count, clothData.cloth03Idx, ref clothData.parent03Path);

        ItemTrSave(pantsData.pants01Count, pantsData.pants01Idx, ref pantsData.parent01Path);
        ItemTrSave(pantsData.pants02Count, pantsData.pants02Idx, ref pantsData.parent02Path);
        ItemTrSave(pantsData.pants03Count, pantsData.pants03Idx, ref pantsData.parent03Path);

        ItemTrSave(shoesData.shoes01Count, shoesData.shoes01Idx, ref shoesData.parent01Path);
        ItemTrSave(shoesData.shoes02Count, shoesData.shoes02Idx, ref shoesData.parent02Path);
        ItemTrSave(shoesData.shoes03Count, shoesData.shoes03Idx, ref shoesData.parent03Path);

        ItemTrSave(kloakData.kloak01Count, kloakData.kloak01Idx, ref kloakData.parent01Path);
        ItemTrSave(kloakData.kloak02Count, kloakData.kloak02Idx, ref kloakData.parent02Path);
        ItemTrSave(kloakData.kloak03Count, kloakData.kloak03Idx, ref kloakData.parent03Path);

        ItemTrSave(neckData.neck01Count, neckData.neck01Idx, ref neckData.parent01Path);
        ItemTrSave(neckData.neck02Count, neckData.neck02Idx, ref neckData.parent02Path);
        ItemTrSave(neckData.neck03Count, neckData.neck03Idx, ref neckData.parent03Path);
        ItemTrSave(neckData.neck04Count, neckData.neck04Idx, ref neckData.parent04Path);

        ItemTrSave(ringData.ring01Count, ringData.ring01Idx, ref ringData.parent01Path);
        ItemTrSave(ringData.ring02Count, ringData.ring02Idx, ref ringData.parent02Path);

        ItemTrSave(potionData.HPpotion01Count, potionData.HPpotion01Idx, ref potionData.parent01Path);
        ItemTrSave(potionData.HPpotion02Count, potionData.HPpotion02Idx, ref potionData.parent02Path);
        ItemTrSave(potionData.HPpotion03Count, potionData.HPpotion03Idx, ref potionData.parent03Path);

        ItemTrSave(potionData.MPpotion01Count, potionData.MPpotion01Idx, ref potionData.parentMP01Path);
        ItemTrSave(potionData.MPpotion02Count, potionData.MPpotion02Idx, ref potionData.parentMP02Path);
        ItemTrSave(potionData.MPpotion03Count, potionData.MPpotion03Idx, ref potionData.parentMP03Path);

        ItemTrSave(materialData.material01Count, materialData.material01Idx, ref materialData.parent01Path);
        ItemTrSave(materialData.material02Count, materialData.material02Idx, ref materialData.parent02Path);
        ItemTrSave(materialData.material03Count, materialData.material03Idx, ref materialData.parent03Path);
        ItemTrSave(materialData.material04Count, materialData.material04Idx, ref materialData.parent04Path);
        ItemTrSave(materialData.material05Count, materialData.material05Idx, ref materialData.parent05Path);
        ItemTrSave(materialData.material06Count, materialData.material06Idx, ref materialData.parent06Path);
        ItemTrSave(materialData.material07Count, materialData.material07Idx, ref materialData.parent07Path);
        ItemTrSave(materialData.material08Count, materialData.material08Idx, ref materialData.parent08Path);
    }
    //아이템이 있는지 없는지 확인할 매서드
    // 아이템이 존재하는지 확인 후, 위치를 설정하는 메서드
    private void ItemCheck(ItemType itemType, ref int itemCount, int itemIdx, UnityAction action, string parentPath, float[] values,ref bool isEquip)
    {
        if (itemCount > 0)
        {
            itemCount--;
            action();

            Transform tr = GetTransformByPath(parentPath);
            Debug.Log(tr);
            RectTransform item = itemList[itemIdx];
            if (tr != null)
            {
                item.SetParent(tr);
                ItemInfo itemInfo = item.GetComponent<ItemInfo>();
                if (tr.GetComponent<ItemInfo>() != null && itemInfo.type == tr.GetComponent<ItemInfo>().type)
                {
                    Drag itemDrag = item.GetComponent<Drag>();
                    itemDrag.IsEuqip(true);
                    if (itemInfo.type == ItemData.ItemType.Sword || itemInfo.type == ItemData.ItemType.Shield)
                    {
                        itemDrag.ItemTypeSelect(itemInfo.type, true);
                    }
                    switch (itemInfo.type)
                    {
                        case ItemData.ItemType.Sword:
                            itemDrag.ItemTypeSelect(itemInfo.type, true);
                            if(!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0]);
                                isEquip = true;
                            }
                            break;
                        case ItemData.ItemType.Shield:
                            itemDrag.ItemTypeSelect(itemInfo.type, true);
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0]);
                                isEquip= true;
                            }
                            break;
                        case ItemData.ItemType.Hat:
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                isEquip = true;
                            }
                            break;
                        case ItemData.ItemType.Cloth:
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                isEquip = true;
                            }
                            break;
                        case ItemData.ItemType.Pants:
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                isEquip = true;
                            }
                            break;
                        case ItemData.ItemType.Shoes:
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                isEquip = true;
                            }
                            break;
                        case ItemData.ItemType.Kloak:
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0], values[1], values[2]);
                                isEquip = true;
                            }
                            break;
                        case ItemData.ItemType.Neck:
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0], values[1], values[2]);
                                isEquip = true;
                            }
                            break;
                        case ItemData.ItemType.Ring:
                            if (!isEquip)
                            {
                                itemDrag.ItemChange(itemType, values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                                isEquip = true;
                            }
                            break;
                    }
                }

            }
            else
            {
                RectTransform window = GetWindowList();
                item.SetParent(window);
            }
        }
    }
    private void CheckPotion(ItemType itemType, Sprite sprite, ref int potionCount, ref int potionIdx, int quickIdx, string name, string exPlain, string parentPath, UnityAction action)
    {
        if (potionCount > 0)
        {
            CreatItemPotionMatItem(itemType, sprite, potionCount, ref potionIdx, name, exPlain, 0, action);
            Transform parentTransform = null;
            if (parentPath != null)
            {
                parentTransform = GetTransformByPath(parentPath);
            }
            if (parentTransform != null)
            {
                itemList[potionIdx].SetParent(parentTransform);
            }
            else
            {
                RectTransform window = GetWindowList();
                itemList[potionIdx].SetParent(window);
            }
        }
    }
    private void CheckMaterial(ItemType itemType, Sprite sprite, int materialCount, ref int materialIdx, string name, string exPlain, string parentPath, int price)
    {
        if (materialCount > 0)
        {
            materialCount--;
            CreatItemPotionMatItem(itemType, sprite, materialCount, ref materialIdx, name, exPlain, price);
            Transform parentTransform = null;
            if (parentPath != null)
            {
                parentTransform = GetTransformByPath(parentPath);
            }
            if (parentTransform != null)
            {
                itemList[materialIdx].SetParent(parentTransform);
            }
            else
            {
                RectTransform window = GetWindowList();
                itemList[materialIdx].SetParent(window);
            }
        }
    }
    // 경로에 따라 Transform을 반환하는 메서드
    private Transform GetTransformByPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        Debug.Log(path);
        string[] pathElements = path.Split('/');
        Transform currentTransform = null;

        foreach (string element in pathElements)
        {
            if (currentTransform == null)
            {
                GameObject obj = GameObject.Find(element);
                if (obj != null)
                {
                    currentTransform = obj.transform;
                }
                else
                {
                    Debug.LogError($"GameObject '{element}' not found in the scene.");
                    return null;
                }
            }
            else
            {
                Transform childTransform = currentTransform.Find(element);
                if (childTransform != null)
                {
                    currentTransform = childTransform;
                }
                else
                {
                    Debug.LogError($"Transform '{element}' not found under '{currentTransform.name}'.");
                    return null;
                }
            }
        }

        return currentTransform;
    }
    // 아이템의 위치를 경로로 저장할 메서드
    private void ItemTrSave(int itemCount, int itemIdx, ref string path)
    {
        if (itemCount > 0 && itemList[itemIdx] != null)
        {
            path = GetTransformPath(itemList[itemIdx].parent);
        }
    }
    #endregion
    // Transform의 경로를 얻는 메서드
    private string GetTransformPath(Transform transform)
    {
        if (transform == null)
            return null;

        if (transform.parent == null)
            return transform.name;

        return GetTransformPath(transform.parent) + "/" + transform.name;
    }



    public void GoldPlus(int gold)
    {
        playerData.GoldValue += gold;
        goldText.text = playerData.GoldValue.ToString();
    }
    public void GoldUpdate()
    { // 골드 상태 업데이트
        goldText.text = playerData.GoldValue.ToString();
    }
    RectTransform GetWindowList()
    {
        foreach (RectTransform window in windowList)
        {
            if (window.childCount == 0)
                return window;
        }
        return null;
    }
    #region 포션 메서드
    //*****************************포션 사용*********************************//


    public void HPHeal01() => Heal("HP", 50, ref potionData.HPpotion01Count, potionData.HPpotion01Idx);
    public void HPHeal02() => Heal("HP", 100, ref potionData.HPpotion02Count, potionData.HPpotion02Idx);
    public void HPHeal03() => Heal("HP", 500, ref potionData.HPpotion03Count, potionData.HPpotion03Idx);
    public void MPHeal01() => Heal("MP", 50, ref potionData.MPpotion01Count, potionData.MPpotion01Idx);
    public void MPHeal02() => Heal("MP", 100, ref potionData.MPpotion02Count, potionData.MPpotion02Idx);
    public void MPHeal03() => Heal("MP", 500, ref potionData.MPpotion03Count, potionData.MPpotion03Idx);
    public void Heal(string type, int amount, ref int potionCount, int potionIdx)
    {
        if (type == "HP")
        {
            playerData.HP += amount;
            hpHealingEff.SetActive(true);
            hpImage.fillAmount = playerData.HP / playerData.MaxHP;
            if (playerData.HP > playerData.MaxHP)
            {
                playerData.HP = playerData.MaxHP;
            }
            GameManager.GM.StatUpdate(PlayerData.PlayerStat.HP);
        }
        else if (type == "MP")
        {
            playerData.MP += amount;
            mpHealingEff.SetActive(true);
            mpImage.fillAmount = playerData.MP / playerData.MaxMP;
            if (playerData.MP > playerData.MaxMP)
            {
                playerData.MP = playerData.MaxMP;
            }
            GameManager.GM.StatUpdate(PlayerData.PlayerStat.MP);
        }

        potionCount--;
        itemList[potionIdx].GetComponentInChildren<Text>().text = potionCount.ToString();

        if (potionCount == 0)
        {
            ClearPotion(potionIdx);
            itemList[potionIdx].gameObject.SetActive(false);
        }
    }
    public void ClearPotion(int itemIdx)
    {
        itemList[itemIdx].GetComponent<Button>().onClick.RemoveAllListeners();
        for (int i = 0; i < quickSlot.Count; i++)
        {
            if (itemList[itemIdx].parent == quickSlot[i])
            {
                getItem.SetAction(i, null);
                break;
            }
        }
        itemList[itemIdx].SetParent(itemGroup);
        Destroy(itemList[itemIdx].GetComponent<EventTrigger>());
    }
    //************** 포션을 우클릭했을때 퀵슬롯창에 등록할 메서드************************//
    private void OnPointerDownDelegate(PointerEventData data, UnityAction action, int idx)
    {
        if (data.button == PointerEventData.InputButton.Right)
        {
            isSelect = false;
            StartCoroutine(PotionSelect(action, idx));
        }
    }
    IEnumerator PotionSelect(UnityAction action, int itemIdx)
    {
        while (!isSelect)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                getItem.SetAction(0, action);
                itemList[itemIdx].SetParent(quickSlot[0]);
                isSelect = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                getItem.SetAction(1, action);
                itemList[itemIdx].SetParent(quickSlot[1]);
                isSelect = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                getItem.SetAction(2, action);
                itemList[itemIdx].SetParent(quickSlot[2]);
                isSelect = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                getItem.SetAction(3, action);
                itemList[itemIdx].SetParent(quickSlot[3]);
                isSelect = true;
            }
        }
    }
    #endregion
    // 아이템을 다썻을때
    public void ClearMaterial(int itemIdx)
    {
        itemList[itemIdx].gameObject.SetActive(false);
        itemList[itemIdx].SetParent(itemGroup);
    }
    //****************************아이템 착용 확인**************************//
    public void SwordOnOff(bool _active)
    {
        playerAttack.IsSword(_active);

    }
    public void ShieldTakeOff(bool _active)
    {
        playerAttack.IsShield(_active);
    }
    // 새로 게임을 시작할때
    public void ReSetGame()
    {
        playerData.HP = 100;
        playerData.MaxHP = 100;
        playerData.MP = 100;
        playerData.MaxMP = 100;
        playerData.AttackValue = 10;
        playerData.MagicAttackValue = 0;
        playerData.expValue = 0;
        playerData.maxExpValue = 100;
        playerData.Level = 1;
        playerData.DefenceValue = 5;
        playerData.FatalProbability = 0.05f;
        playerData.FatalValue = 150f;
        playerData.level05MagicDamage = 100;
        playerData.level10MagicDamage = 200;
        playerData.level20_1MagicDamage = 300;
        playerData.level20_2MagicDamage = 300;
        playerData.level30MagicDamage = 400;
        playerData.GoldValue = 10;
        playerData.levelSkillPoint = 0;
        swordData.sword01Count = 0;
        shieldData.shield01Count = 0;
    }
}
[System.Serializable]
public class ItemDrop
{
    public float dropRate;
    public System.Action getItemAction;
}
