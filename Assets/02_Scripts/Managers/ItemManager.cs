using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ItemData;
public class ItemManager : MonoBehaviour
{
    public static ItemManager itemInst;
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
    [Header("골드 텍스트")]
    [SerializeField] Text goldText;
    [Header("포션 이펙트")]
    [SerializeField] GameObject hpHealingEff;
    [SerializeField] GameObject mpHealingEff;
    [Header("HP,MP 이미지")]
    [SerializeField] Image hpImage;
    [SerializeField] Image mpImage;
    PlayerAttack playerAttack;  // 검과 방패를 온오프 할때 필요한 변수
    PlayerUIController getItem; // 퀵슬롯 아이템에 이벤트를 등록할 플레이어 변수
    Transform itemGroup;        // 아이템들의 부모
    private List<string> paths = new List<string>();
    List<RectTransform> itemList = new List<RectTransform>();   // 아이템들의 리스트
    List<RectTransform> windowList = new List<RectTransform>(); // 아이템들이 들어갈 리스트
    List<Transform> quickSlot = new List<Transform>();          // 아이템 퀵슬롯 이미지리스트
    private Dictionary<ItemType, List<ItemDrop>> itemDropTable; // 아이템들을 얻을때 확률을 조정할때필요한 변수
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
        DataManager.dataInst.uiElements = itemList;
        InitializeItemDropTable();
        AllItemCheck();
    }
    #region 아이템 얻기
    //************** 검얻기****************//
    public void GetSword01() { CreateWeaponItem(ItemType.Sword, swordData.Image_01, ref swordData.Count_01, ref swordData.Idx_01, new float[] { swordData.Damage_01 }, swordData.Name_01); }
    public void GetSword02() { CreateWeaponItem(ItemType.Sword, swordData.Image_02, ref swordData.Count_02, ref swordData.Idx_01, new float[] { swordData.Damage_02 }, swordData.Name_02); }
    public void GetSword03() { CreateWeaponItem(ItemType.Sword, swordData.Image_03, ref swordData.Count_03, ref swordData.Idx_01, new float[] { swordData.Damage_03 }, swordData.Name_03); }
    // 방패 얻기
    public void GetShield01() { CreateWeaponItem(ItemType.Shield, shieldData.Image_01, ref shieldData.Count_01, ref shieldData.Idx_01, new float[] { shieldData.Defence_01 }, shieldData.Name_01); }
    public void GetShield02() { CreateWeaponItem(ItemType.Shield, shieldData.Image_02, ref shieldData.Count_02, ref shieldData.Idx_02, new float[] { shieldData.Defence_02 }, shieldData.Name_02); }
    public void GetShield03() { CreateWeaponItem(ItemType.Shield, shieldData.Image_03, ref shieldData.Count_03, ref shieldData.Idx_03, new float[] { shieldData.Defence_03 }, shieldData.Name_03); }
    public void GetShield04() { CreateWeaponItem(ItemType.Shield, shieldData.Image_04, ref shieldData.Count_04, ref shieldData.Idx_04, new float[] { shieldData.Defence_04 }, shieldData.Name_04); }
    // 헬멧 얻기
    public void GetHat01() { CreateWeaponItem(ItemType.Hat, hatData.Image_01, ref hatData.Count_01, ref hatData.Idx_01, new float[] { hatData.HP_01, hatData.Defence_01 }, hatData.Name_01); }
    public void GetHat02() { CreateWeaponItem(ItemType.Hat, hatData.Image_02, ref hatData.Count_02, ref hatData.Idx_02, new float[] { hatData.HP_02, hatData.Defence_02 }, hatData.Name_02); }
    public void GetHat03() { CreateWeaponItem(ItemType.Hat, hatData.Image_03, ref hatData.Count_03, ref hatData.Idx_03, new float[] { hatData.HP_03, hatData.Defence_03 }, hatData.Name_03); }
    public void GetHat04() { CreateWeaponItem(ItemType.Hat, hatData.Image_04, ref hatData.Count_04, ref hatData.Idx_04, new float[] { hatData.HP_04, hatData.Defence_04 }, hatData.Name_04); }
    // 옷 얻기
    public void GetCloth01() { CreateWeaponItem(ItemType.Cloth, clothData.Image_01, ref clothData.Count_01, ref clothData.Idx_01, new float[] { clothData.HP_01, clothData.Defence_01 }, clothData.Name_01); }
    public void GetCloth02() { CreateWeaponItem(ItemType.Cloth, clothData.Image_02, ref clothData.Count_02, ref clothData.Idx_02, new float[] { clothData.HP_02, clothData.Defence_02 }, clothData.Name_02); }
    public void GetCloth03() { CreateWeaponItem(ItemType.Cloth, clothData.Image_03, ref clothData.Count_03, ref clothData.Idx_03, new float[] { clothData.HP_03, clothData.Defence_03 }, clothData.Name_03); }
    // 바지 얻기
    public void GetPants01() { CreateWeaponItem(ItemType.Pants, pantsData.Image_01, ref pantsData.Count_01, ref pantsData.Idx_01, new float[] { pantsData.HP_01, pantsData.Defence_01 }, pantsData.Name_01); }
    public void GetPants02() { CreateWeaponItem(ItemType.Pants, pantsData.Image_02, ref pantsData.Count_02, ref pantsData.Idx_02, new float[] { pantsData.HP_02, pantsData.Defence_02 }, pantsData.Name_02); }
    public void GetPants03() { CreateWeaponItem(ItemType.Pants, pantsData.Image_03, ref pantsData.Count_03, ref pantsData.Idx_03, new float[] { pantsData.HP_03, pantsData.Defence_03 }, pantsData.Name_03); }
    // 신발 얻기
    public void GetShoes01() { CreateWeaponItem(ItemType.Shoes, shoesData.Image_01, ref shoesData.Count_01, ref shoesData.Idx_01, new float[] { shoesData.HP_01, shoesData.Defence_01 }, shoesData.Name_01); }
    public void GetShoes02() { CreateWeaponItem(ItemType.Shoes, shoesData.Image_02, ref shoesData.Count_02, ref shoesData.Idx_02, new float[] { shoesData.HP_02, shoesData.Defence_02 }, shoesData.Name_02); }
    public void GetShoes03() { CreateWeaponItem(ItemType.Shoes, shoesData.Image_03, ref shoesData.Count_03, ref shoesData.Idx_03, new float[] { shoesData.HP_03, shoesData.Defence_03 }, shoesData.Name_03); }
    // 망토 얻기
    public void GetKloak01() { CreateWeaponItem(ItemType.Kloak, kloakData.Image_01, ref kloakData.Count_01, ref kloakData.Idx_01, new float[] { kloakData.HP_01, kloakData.MP_01, kloakData.Defence_01 }, kloakData.Name_01); }
    public void GetKloak02() { CreateWeaponItem(ItemType.Kloak, kloakData.Image_02, ref kloakData.Count_02, ref kloakData.Idx_02, new float[] { kloakData.HP_02, kloakData.MP_02, kloakData.Defence_02 }, kloakData.Name_02); }
    public void GetKloak03() { CreateWeaponItem(ItemType.Kloak, kloakData.Image_03, ref kloakData.Count_03, ref kloakData.Idx_03, new float[] { kloakData.HP_03, kloakData.MP_03, kloakData.Defence_03 }, kloakData.Name_03); }
    // 목걸이 얻기
    public void GetNeck01() { CreateWeaponItem(ItemType.Neck, neckData.Image_01, ref neckData.Count_01, ref neckData.Idx_01, new float[] { neckData.MP_01, neckData.Damage_01, neckData.Magicdamage_01 }, neckData.Name_01); }
    public void GetNeck02() { CreateWeaponItem(ItemType.Neck, neckData.Image_02, ref neckData.Count_02, ref neckData.Idx_02, new float[] { neckData.MP_02, neckData.Damage_02, neckData.Magicdamage_02 }, neckData.Name_02); }
    public void GetNeck03() { CreateWeaponItem(ItemType.Neck, neckData.Image_03, ref neckData.Count_03, ref neckData.Idx_03, new float[] { neckData.MP_03, neckData.Damage_03, neckData.Magicdamage_03 }, neckData.Name_03); }
    public void GetNeck04() { CreateWeaponItem(ItemType.Neck, neckData.Image_04, ref neckData.Count_04, ref neckData.Idx_04, new float[] { neckData.MP_04, neckData.Damage_04, neckData.Magicdamage_04 }, neckData.Name_04); }
    // 반지 얻기
    public void GetRing01() { CreateWeaponItem(ItemType.Ring, ringData.Image_01, ref ringData.Count_01, ref ringData.Idx_01, new float[] { ringData.HP_01, ringData.MP_01, ringData.Damage_01, ringData.Defence_01, ringData.MagicDamage_01, ringData.FatalProbability_01, ringData.FatalValue_01 }, ringData.Name_01); }
    public void GetRing02() { CreateWeaponItem(ItemType.Ring, ringData.Image_02, ref ringData.Count_02, ref ringData.Idx_02, new float[] { ringData.HP_02, ringData.MP_02, ringData.Damage_02, ringData.Defence_02, ringData.MagicDamage_02, ringData.FatalProbability_02, ringData.FatalValue_02 }, ringData.Name_02); }


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
            CreatItemPotionMatItem(ItemType.HPPotion, potionData.HPpotion01, potionData.HPpotion01Count, ref potionData.HPpotion01Idx, potionData.HPpotion01Name, potionData.HPpotion01quick, HPHeal01);
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
            CreatItemPotionMatItem(ItemType.HPPotion, potionData.HPpotion02, potionData.HPpotion02Count, ref potionData.HPpotion02Idx, potionData.HPpotion02Name, potionData.HPpotion02quick, HPHeal02);
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
            CreatItemPotionMatItem(ItemType.HPPotion, potionData.HPpotion03, potionData.HPpotion03Count, ref potionData.HPpotion03Idx, potionData.HPpotion03Name, potionData.HPpotion03quick, HPHeal03);
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
            CreatItemPotionMatItem(ItemType.MPPotion, potionData.MPpotion01, potionData.MPpotion01Count, ref potionData.MPpotion01Idx, potionData.MPpotion01Name, potionData.MPpotion01quick, MPHeal01);
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
            CreatItemPotionMatItem(ItemType.MPPotion, potionData.MPpotion02, potionData.MPpotion02Count, ref potionData.MPpotion02Idx, potionData.MPpotion02Name, potionData.MPpotion02quick, MPHeal02);
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
            CreatItemPotionMatItem(ItemType.MPPotion, potionData.MPpotion03, potionData.MPpotion03Count, ref potionData.MPpotion03Idx, potionData.MPpotion03Name, potionData.MPpotion03quick, MPHeal03);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material01, materialData.material01Count, ref materialData.material01Idx, materialData.material01Name, materialData.material01Price);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material02, materialData.material02Count, ref materialData.material02Idx, materialData.material02Name, materialData.material02Price);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material03, materialData.material03Count, ref materialData.material03Idx, materialData.material03Name, materialData.material03Price);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material04, materialData.material04Count, ref materialData.material04Idx, materialData.material04Name, materialData.material04Price);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material05, materialData.material05Count, ref materialData.material05Idx, materialData.material05Name, materialData.material05Price);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material06, materialData.material06Count, ref materialData.material06Idx, materialData.material06Name, materialData.material06Price);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material07, materialData.material07Count, ref materialData.material07Idx, materialData.material07Name, materialData.material07Price);
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
            CreatItemPotionMatItem(ItemType.Material, materialData.material08, materialData.material08Count, ref materialData.material08Idx, materialData.material08Name, materialData.material08Price);
        }
    }
    #endregion
    #region 아이템 얻는 메소드
    public void GetItem(ItemType itemType)
    {
        if (itemType == ItemType.Gold)
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
            AllItemSave();
        }
    }
    private void InitializeItemDropTable()
    {
        itemDropTable = new Dictionary<ItemType, List<ItemDrop>>
        {
            {
                ItemType.Sword, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 80.0f, getItemAction = GetSword01 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetSword02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetSword03 }
                }
            },
            {
                ItemType.Shield, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 60.0f, getItemAction = GetShield01 },
                    new ItemDrop { dropRate = 85.0f, getItemAction = GetShield02 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetShield03 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetShield04 }
                }
            },
            {
                ItemType.Hat, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 60.0f, getItemAction = GetHat01 },
                    new ItemDrop { dropRate = 85.0f, getItemAction = GetHat02 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetHat03 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetHat04 }
                }
            },
            {
                ItemType.Cloth, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetCloth01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetCloth02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetCloth03 }
                }
            },
            {
                ItemType.Pants, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetPants01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetPants02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetPants03 }
                }
            },
            {
                ItemType.Shoes, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetShoes01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetShoes02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetShoes03 }
                }
            },
            {
                ItemType.Kloak, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetKloak01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetKloak02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetKloak03 }
                }
            },
            {
                ItemType.Neck, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 60.0f, getItemAction = GetNeck01 },
                    new ItemDrop { dropRate = 85.0f, getItemAction = GetNeck02 },
                    new ItemDrop { dropRate = 98.0f, getItemAction = GetNeck03 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetNeck04 }
                }
            },
            {
                ItemType.Ring, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 80.0f, getItemAction = GetRing01 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetRing01 }
                }
            },
            {
                ItemType.HPPotion, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetHPpotion01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetHPpotion02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetHPpotion03 }
                }
            },
            {
                ItemType.MPPotion, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 70.0f, getItemAction = GetMPpotion01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetMPpotion02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetMPpotion03 }
                }
            },
            {
                ItemType.Material, new List<ItemDrop>
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
    float[] values, string itemName)
    {
        itemCount++;
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
        string explain = "";

        switch (itemType)
        {
            case ItemType.Sword:
                item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                explain = "공격력 + " + values[0].ToString();
                break;
            case ItemType.Shield:
                item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                explain = "방어력 + " + values[0].ToString();
                break;
            case ItemType.Hat:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                break;
            case ItemType.Cloth:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                break;
            case ItemType.Pants:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                break;
            case ItemType.Shoes:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                break;
            case ItemType.Kloak:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                explain = "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "공격력 + " + values[2].ToString();
                break;
            case ItemType.Neck:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                explain = "MP + " + values[0].ToString() + "공격력 + " + values[1].ToString() + "MagicDamage + " + values[2].ToString();
                break;
            case ItemType.Ring:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                explain = "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "방어력 + " + values[2].ToString() + "치명타공격력 + " +
                    values[3].ToString() + "치명타확률 + " + values[4].ToString() + "마법데미지" + values[5].ToString() + "공격력" + values[5].ToString();
                break;
        }

        item.GetComponent<ItemToolTip>().itemName = itemName;
        item.GetComponent<ItemToolTip>().itemExplain = explain;
        WindowSetParents(item);
        item.gameObject.SetActive(true);
    }
    void CreatItemPotionMatItem(ItemType itemType, Sprite itemSprite, int itemCount, ref int itemIdx, string itemName, int priceAndQuickIdx, UnityAction onClickAction = null, float potionValue = 0)
    {
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
        string explain = "";
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
                explain = "HP + " + potionValue.ToString();
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
                explain = "MP + " + potionValue.ToString();
                break;
            case ItemType.Material:
                var matText = Instantiate(itemData.itemCountText, item);
                matText.GetComponent<Text>().text = itemCount.ToString();
                item.GetComponent<Drag>().price = priceAndQuickIdx;
                explain = priceAndQuickIdx.ToString() + " 원";
                break;
        }
        item.GetComponent<ItemToolTip>().itemName = itemName;
        item.GetComponent<ItemToolTip>().itemExplain = explain;
        ItemSave(itemType,itemIdx, itemName, itemCount);
        WindowSetParents(item);
        item.gameObject.SetActive(true);
    }
    #endregion
    #region 아이템 체크 및 위치 저장
    public void AllItemCheck()
    {
        DataManager.dataInst.LoadItemData();
        // 아이템 체크
        ItemCheck(ItemType.Sword, swordData.Image_01, ref swordData.Idx_01, new float[] { swordData.Damage_01 }, swordData.Name_01);
        ItemCheck(ItemType.Sword, swordData.Image_02, ref swordData.Idx_02, new float[] { swordData.Damage_02 }, swordData.Name_02);
        ItemCheck(ItemType.Sword, swordData.Image_03, ref swordData.Idx_03, new float[] { swordData.Damage_03 }, swordData.Name_03);
        ItemCheck(ItemType.Shield, shieldData.Image_01, ref shieldData.Idx_01, new float[] { shieldData.Defence_01 }, shieldData.Name_01);
        ItemCheck(ItemType.Shield, shieldData.Image_02, ref shieldData.Idx_02, new float[] { shieldData.Defence_02 }, shieldData.Name_02);
        ItemCheck(ItemType.Shield, shieldData.Image_03, ref shieldData.Idx_03, new float[] { shieldData.Defence_03 }, shieldData.Name_03);
        ItemCheck(ItemType.Shield, shieldData.Image_04, ref shieldData.Idx_04, new float[] { shieldData.Defence_04 }, shieldData.Name_04);
        ItemCheck(ItemType.Hat, hatData.Image_01, ref hatData.Idx_01, new float[] { hatData.HP_01, hatData.Defence_01 }, hatData.Name_01);
        ItemCheck(ItemType.Hat, hatData.Image_02, ref hatData.Idx_02, new float[] { hatData.HP_02, hatData.Defence_02 }, hatData.Name_02);
        ItemCheck(ItemType.Hat, hatData.Image_03, ref hatData.Idx_03, new float[] { hatData.HP_03, hatData.Defence_03 }, hatData.Name_03);
        ItemCheck(ItemType.Hat, hatData.Image_04, ref hatData.Idx_04, new float[] { hatData.HP_04, hatData.Defence_04 }, hatData.Name_04);
        ItemCheck(ItemType.Cloth, clothData.Image_01, ref clothData.Idx_01, new float[] { clothData.HP_01, clothData.Defence_01 }, clothData.Name_01);
        ItemCheck(ItemType.Cloth, clothData.Image_02, ref clothData.Idx_02, new float[] { clothData.HP_01, clothData.Defence_02 }, clothData.Name_02);
        ItemCheck(ItemType.Cloth, clothData.Image_03, ref clothData.Idx_03, new float[] { clothData.HP_01, clothData.Defence_03 }, clothData.Name_03);
        ItemCheck(ItemType.Pants, pantsData.Image_01, ref pantsData.Idx_01, new float[] { pantsData.HP_01, pantsData.Defence_01 }, pantsData.Name_01);
        ItemCheck(ItemType.Pants, pantsData.Image_02, ref pantsData.Idx_02, new float[] { pantsData.HP_01, pantsData.Defence_02 }, pantsData.Name_02);
        ItemCheck(ItemType.Pants, pantsData.Image_03, ref pantsData.Idx_03, new float[] { pantsData.HP_01, pantsData.Defence_03 }, pantsData.Name_03);
        ItemCheck(ItemType.Shoes, shoesData.Image_01, ref shoesData.Idx_01, new float[] { shoesData.HP_01, shoesData.Defence_01 }, shoesData.Name_01);
        ItemCheck(ItemType.Shoes, shoesData.Image_02, ref shoesData.Idx_02, new float[] { shoesData.HP_01, shoesData.Defence_02 }, shoesData.Name_02);
        ItemCheck(ItemType.Shoes, shoesData.Image_03, ref shoesData.Idx_03, new float[] { shoesData.HP_01, shoesData.Defence_03 }, shoesData.Name_03);
        ItemCheck(ItemType.Kloak, kloakData.Image_01, ref kloakData.Idx_01, new float[] { kloakData.HP_01, kloakData.MP_01, kloakData.Defence_01 }, kloakData.Name_01);
        ItemCheck(ItemType.Kloak, kloakData.Image_02, ref kloakData.Idx_02, new float[] { kloakData.HP_02, kloakData.MP_02, kloakData.Defence_02 }, kloakData.Name_02);
        ItemCheck(ItemType.Kloak, kloakData.Image_03, ref kloakData.Idx_03, new float[] { kloakData.HP_03, kloakData.MP_03, kloakData.Defence_03 }, kloakData.Name_03);
        ItemCheck(ItemType.Neck, neckData.Image_01, ref neckData.Idx_01, new float[] { neckData.MP_01, neckData.Damage_01, neckData.Magicdamage_01 }, neckData.Name_01);
        ItemCheck(ItemType.Neck, neckData.Image_02, ref neckData.Idx_02, new float[] { neckData.MP_02, neckData.Damage_02, neckData.Magicdamage_02 }, neckData.Name_02);
        ItemCheck(ItemType.Neck, neckData.Image_03, ref neckData.Idx_03, new float[] { neckData.MP_03, neckData.Damage_03, neckData.Magicdamage_03 }, neckData.Name_03);
        ItemCheck(ItemType.Neck, neckData.Image_04, ref neckData.Idx_04, new float[] { neckData.MP_04, neckData.Damage_04, neckData.Magicdamage_04 }, neckData.Name_04);
        ItemCheck(ItemType.Ring, ringData.Image_01, ref ringData.Idx_01,
            new float[] { ringData.HP_01, ringData.MP_01, ringData.Defence_01, ringData.FatalValue_01, ringData.FatalProbability_01, ringData.MagicDamage_01, ringData.Damage_01 }, ringData.Name_01);
        ItemCheck(ItemType.Ring, ringData.Image_02, ref ringData.Idx_02,
            new float[] { ringData.HP_02, ringData.MP_02, ringData.Defence_02, ringData.FatalValue_02, ringData.FatalProbability_02, ringData.MagicDamage_02, ringData.Damage_02 }, ringData.Name_02);
        CheckPotionMat(ItemType.HPPotion, potionData.HPpotion01, ref potionData.HPpotion01Idx, potionData.HPpotion01quick,
             potionData.HPpotion01Name, HPHeal01, potionData.HPpotion01Value);
        CheckPotionMat(ItemType.HPPotion, potionData.HPpotion02, ref potionData.HPpotion02Idx, potionData.HPpotion02quick,
             potionData.HPpotion02Name, HPHeal02, potionData.HPpotion02Value);
        CheckPotionMat(ItemType.HPPotion, potionData.HPpotion03, ref potionData.HPpotion03Idx, potionData.HPpotion03quick,
            potionData.HPpotion03Name, HPHeal03, potionData.HPpotion03Value);
        CheckPotionMat(ItemType.HPPotion, potionData.MPpotion01, ref potionData.MPpotion01Idx, potionData.MPpotion01quick,
             potionData.MPpotion01Name, MPHeal01, potionData.MPpotion01Value);
        CheckPotionMat(ItemType.HPPotion, potionData.MPpotion02, ref potionData.MPpotion02Idx, potionData.MPpotion02quick,
             potionData.MPpotion02Name, MPHeal02, potionData.MPpotion02Value);
        CheckPotionMat(ItemType.HPPotion, potionData.MPpotion03, ref potionData.MPpotion03Idx, potionData.MPpotion03quick,
             potionData.MPpotion03Name, MPHeal03, potionData.MPpotion03Value);

        CheckPotionMat(ItemType.Material, materialData.material01, ref materialData.material01Idx, materialData.material01Price, materialData.material01Name);
        CheckPotionMat(ItemType.Material, materialData.material02, ref materialData.material02Idx, materialData.material02Price, materialData.material02Name);
        CheckPotionMat(ItemType.Material, materialData.material03, ref materialData.material03Idx, materialData.material03Price, materialData.material03Name);
        CheckPotionMat(ItemType.Material, materialData.material04, ref materialData.material04Idx, materialData.material04Price, materialData.material04Name);
        CheckPotionMat(ItemType.Material, materialData.material05, ref materialData.material05Idx, materialData.material05Price, materialData.material05Name);
        CheckPotionMat(ItemType.Material, materialData.material06, ref materialData.material06Idx, materialData.material06Price, materialData.material06Name);
        CheckPotionMat(ItemType.Material, materialData.material07, ref materialData.material07Idx, materialData.material07Price, materialData.material07Name);
        CheckPotionMat(ItemType.Material, materialData.material08, ref materialData.material08Idx, materialData.material08Price, materialData.material08Name);
        AllItemSave();
    }
    public void AllItemSave()
    {
        ItemSave(ItemType.Sword, swordData.Idx_01, swordData.Name_01,  swordData.Count_01);
        ItemSave(ItemType.Sword, swordData.Idx_02, swordData.Name_02, swordData.Count_02);
        ItemSave(ItemType.Sword, swordData.Idx_03, swordData.Name_03, swordData.Count_03);

        ItemSave(ItemType.Shield, shieldData.Idx_01, shieldData.Name_01, shieldData.Count_01);
        ItemSave(ItemType.Shield, shieldData.Idx_02, shieldData.Name_02, shieldData.Count_02);
        ItemSave(ItemType.Shield, shieldData.Idx_03, shieldData.Name_03, shieldData.Count_03);
        ItemSave(ItemType.Shield, shieldData.Idx_04, shieldData.Name_04, shieldData.Count_04);

        ItemSave(ItemType.Hat, hatData.Idx_01, hatData.Name_01, hatData.Count_01);
        ItemSave(ItemType.Hat, hatData.Idx_02, hatData.Name_02, hatData.Count_02);
        ItemSave(ItemType.Hat, hatData.Idx_03, hatData.Name_03, hatData.Count_03);
        ItemSave(ItemType.Hat, hatData.Idx_04, hatData.Name_04, hatData.Count_04);

        ItemSave(ItemType.Cloth, clothData.Idx_01, clothData.Name_01, clothData.Count_01);
        ItemSave(ItemType.Cloth, clothData.Idx_02, clothData.Name_02, clothData.Count_02);
        ItemSave(ItemType.Cloth, clothData.Idx_03, clothData.Name_03, clothData.Count_03);

        ItemSave(ItemType.Pants, pantsData.Idx_01, pantsData.Name_01, pantsData.Count_01);
        ItemSave(ItemType.Pants, pantsData.Idx_02, pantsData.Name_02, pantsData.Count_02);
        ItemSave(ItemType.Pants, pantsData.Idx_03, pantsData.Name_03, pantsData.Count_03);

        ItemSave(ItemType.Shoes, shoesData.Idx_01, shoesData.Name_01, shoesData.Count_01);
        ItemSave(ItemType.Shoes, shoesData.Idx_02, shoesData.Name_02, shoesData.Count_02);
        ItemSave(ItemType.Shoes, shoesData.Idx_03, shoesData.Name_03, shoesData.Count_03);

        ItemSave(ItemType.Kloak, kloakData.Idx_01, kloakData.Name_01, kloakData.Count_01);
        ItemSave(ItemType.Kloak, kloakData.Idx_02, kloakData.Name_02, kloakData.Count_02);
        ItemSave(ItemType.Kloak, kloakData.Idx_03, kloakData.Name_03, kloakData.Count_03);

        ItemSave(ItemType.Neck, neckData.Idx_01, neckData.Name_01, neckData.Count_01);
        ItemSave(ItemType.Neck, neckData.Idx_02, neckData.Name_02, neckData.Count_02);
        ItemSave(ItemType.Neck, neckData.Idx_03, neckData.Name_03, neckData.Count_03);
        ItemSave(ItemType.Neck, neckData.Idx_04, neckData.Name_04, neckData.Count_04);

        ItemSave(ItemType.Ring, ringData.Idx_01, ringData.Name_01, ringData.Count_01);
        ItemSave(ItemType.Ring, ringData.Idx_02, ringData.Name_02, ringData.Count_02);

        ItemSave(ItemType.HPPotion, potionData.HPpotion01Idx, potionData.HPpotion01Name,  potionData.HPpotion01Count);
        ItemSave(ItemType.HPPotion, potionData.HPpotion02Idx, potionData.HPpotion02Name, potionData.HPpotion02Count);
        ItemSave(ItemType.HPPotion, potionData.HPpotion03Idx, potionData.HPpotion03Name, potionData.HPpotion03Count);

        ItemSave(ItemType.MPPotion, potionData.MPpotion01Idx, potionData.MPpotion01Name, potionData.MPpotion01Count);
        ItemSave(ItemType.MPPotion, potionData.MPpotion02Idx, potionData.MPpotion02Name, potionData.MPpotion02Count);
        ItemSave(ItemType.MPPotion, potionData.MPpotion03Idx, potionData.MPpotion03Name, potionData.MPpotion03Count);

        ItemSave(ItemType.Material, materialData.material01Idx, materialData.material01Name, materialData.material01Count);
        ItemSave(ItemType.Material, materialData.material02Idx, materialData.material02Name,  materialData.material02Count);
        ItemSave(ItemType.Material, materialData.material03Idx, materialData.material03Name, materialData.material03Count);
        ItemSave(ItemType.Material, materialData.material04Idx, materialData.material04Name, materialData.material04Count);
        ItemSave(ItemType.Material, materialData.material05Idx, materialData.material05Name, materialData.material05Count);
        ItemSave(ItemType.Material, materialData.material06Idx, materialData.material06Name, materialData.material06Count);
        ItemSave(ItemType.Material, materialData.material07Idx, materialData.material07Name, materialData.material07Count);
        ItemSave(ItemType.Material, materialData.material08Idx, materialData.material08Name, materialData.material08Count);
    }
    //아이템이 있는지 없는지 확인할 매서드
    private void ItemCheck(ItemType itemType, Sprite itemSprite, ref int itemIdx, float[] values, string itemName)
    {
        ItemDataJson itemDataJson = DataManager.dataInst.FindItem(itemType, itemName);
        if (itemDataJson != null )
        {
            if (itemDataJson.Count > 0)
            {
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
                item.GetComponent<ItemToolTip>().itemName = itemName;
                Transform trparent = GetParentTransformByPath(itemDataJson.Path);
                string explain = "";
                if (trparent != null)
                {
                    ItemInfo itemInfo = item.GetComponent<ItemInfo>();
                    if (trparent.GetComponent<ItemInfo>() != null && itemInfo.type == trparent.GetComponent<ItemInfo>().type)
                    {
                        Drag itemDrag = item.GetComponent<Drag>();
                        itemDrag.IsEuqip(true);
                        if (itemInfo.type == ItemType.Sword || itemInfo.type == ItemType.Shield)
                        {
                            itemDrag.ItemTypeSelect(itemInfo.type, true);
                        }
                        switch (itemInfo.type)
                        {
                            case ItemType.Sword:
                                itemDrag.ItemTypeSelect(itemInfo.type, true);
                                itemDrag.ItemChange(itemType, values[0]);
                                explain = "공격력 + " + values[0].ToString();
                                break;
                            case ItemType.Shield:
                                itemDrag.ItemTypeSelect(itemInfo.type, true);
                                itemDrag.ItemChange(itemType, values[0]);
                                explain = "방어력 + " + values[0].ToString();
                                break;
                            case ItemType.Hat:
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                                break;
                            case ItemType.Cloth:
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                                break;
                            case ItemType.Pants:
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                                break;
                            case ItemType.Shoes:
                                itemDrag.ItemChange(itemType, values[0], values[1]);
                                explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                                break;
                            case ItemType.Kloak:
                                explain = "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "공격력 + " + values[2].ToString();
                                itemDrag.ItemChange(itemType, values[0], values[1], values[2]);
                                break;
                            case ItemType.Neck:
                                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                                explain = "MP + " + values[0].ToString() + "공격력 + " + values[1].ToString() + "마법데미지 + " + values[2].ToString();
                                break;
                            case ItemType.Ring:
                                itemDrag.ItemChange(itemType, values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                                explain = "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "방어력 + " + values[2].ToString() + "치명타공격력 + " +
                                    values[3].ToString() + "치명타확률 + " + values[4].ToString() + "마법데미지" + values[5].ToString() + "공격력" + values[5].ToString();
                                break;
                        }
                    }
                    item.SetParent(trparent);
                }
                else
                {
                    switch (itemType)
                    {
                        case ItemType.Sword:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                            explain = "공격력 + " + values[0].ToString();
                            break;
                        case ItemType.Shield:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                            explain = "방어력 + " + values[0].ToString();
                            break;
                        case ItemType.Hat:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                            explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                            break;
                        case ItemType.Cloth:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                            explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                            break;
                        case ItemType.Pants:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                            explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                            break;
                        case ItemType.Shoes:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                            explain = "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
                            break;
                        case ItemType.Kloak:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                            explain = "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "공격력 + " + values[2].ToString();
                            break;
                        case ItemType.Neck:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                            explain = "MP + " + values[0].ToString() + "공격력 + " + values[1].ToString() + "마법데미지 + " + values[2].ToString();
                            break;
                        case ItemType.Ring:
                            item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                            explain = "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "방어력 + " + values[2].ToString() + "치명타공격력 + " +
                                values[3].ToString() + "치명타확률 + " + values[4].ToString() + "마법데미지" + values[5].ToString() + "공격력" + values[5].ToString();
                            break;
                    }
                    WindowSetParents(item);
                }
                item.GetComponent<ItemToolTip>().itemExplain = explain;
                item.gameObject.SetActive(true);
            }
        }
    }
    private void CheckPotionMat(ItemType itemType, Sprite itemSprite, ref int itemIdx, int priceAndQuickIdx, string itemName, UnityAction onClickAction = null, float potionValue = 0)
    {
        ItemDataJson itemDataJson = DataManager.dataInst.FindItem(itemType, itemName);
        if(itemDataJson != null)
        {

            if (itemDataJson.Count > 0)
            {
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
                 Transform tr = GetParentTransformByPath(itemDataJson.Path);
                string explain = "";
                if (tr != null)
                {
                    item.SetParent(tr);
                    switch (itemType)
                    {
                        case ItemType.HPPotion:
                            var text = Instantiate(itemData.itemCountText, item);
                            text.GetComponent<Text>().text = itemDataJson.Count.ToString();
                            item.AddComponent<Button>();
                            item.GetComponent<Button>().onClick.AddListener(onClickAction);
                            EventTrigger eventTrigger = item.AddComponent<EventTrigger>();
                            EventTrigger.Entry entry = new EventTrigger.Entry();
                            entry.eventID = EventTriggerType.PointerDown;
                            entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data, onClickAction, j); });
                            eventTrigger.triggers.Add(entry);
                            explain = "HP + " + potionValue.ToString();
                            break;
                        case ItemType.MPPotion:
                            var text02 = Instantiate(itemData.itemCountText, item);
                            text02.GetComponent<Text>().text = itemDataJson.Count.ToString();
                            item.AddComponent<Button>();
                            item.GetComponent<Button>().onClick.AddListener(onClickAction);
                            EventTrigger MPeventTrigger = item.AddComponent<EventTrigger>();
                            EventTrigger.Entry MPentry = new EventTrigger.Entry();
                            MPentry.eventID = EventTriggerType.PointerDown;
                            MPentry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data, onClickAction, j); });
                            MPeventTrigger.triggers.Add(MPentry);
                            explain = "MP + " + potionValue.ToString();
                            break;
                        case ItemType.Material:
                            var matText = Instantiate(itemData.itemCountText, item);
                            matText.GetComponent<Text>().text = itemDataJson.Count.ToString();
                            item.GetComponent<Drag>().price = priceAndQuickIdx;
                            explain = priceAndQuickIdx.ToString() + " 원";
                            break;
                    }
                }

                item.GetComponent<ItemToolTip>().itemName = itemName;
                item.GetComponent<ItemToolTip>().itemExplain = explain;
                item.gameObject.SetActive(true);
            }
        }
    }
    // 경로에 따라 Transform을 반환하는 메서드
    private Transform GetTransformByPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        string[] pathElements = path.Split('/');
        Transform currentTransform = null;

        foreach (string element in pathElements)
        {
            if (currentTransform == null)
            {
                // 루트 Transform을 찾기
                GameObject obj = GameObject.Find(element);
                if (obj != null)
                {
                    currentTransform = obj.transform;
                }
                else
                {
                    Debug.LogError($"Root GameObject '{element}' not found.");
                    return null;
                }
            }
            else
            {
                // 현재 Transform의 자식 Transform을 찾기
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
    // 경로에 따라 Transform을 반환하는 메서드
    private Transform GetParentTransformByPath(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;

        string[] pathElements = path.Split('/');
        if (pathElements.Length <= 1)
        {
            Debug.LogError("Path does not contain enough elements to determine a parent.");
            return null;
        }

        // 부모 경로 분리
        string parentPath = string.Join("/", pathElements, 0, pathElements.Length - 1);

        // 부모 Transform 찾기
        Transform parentTransform = GetTransformByPath(parentPath);
        if (parentTransform == null)
        {
            Debug.LogError($"Parent Transform not found for path: '{parentPath}'.");
            return null;
        }

        return parentTransform;
    }


    // 아이템의 위치를 경로로 저장할 메서드
    private void ItemSave(ItemType itemType, int idx, string itemName, int itemCount)
    {
        string path = GetTransformPath(itemList[idx]);
        DataManager.dataInst.SaveItemData(itemType, itemName, path, itemCount);
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
    void WindowSetParents(Transform tr)
    {
        for (int i = 0; i < windowList.Count; i++)
        {
            if (windowList[i].childCount == 0)
            {
                tr.SetParent(windowList[i]);
                break;
            }
        }
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
        playerData.playerSceneIdx = 0;
        swordData.Count_01 = 0;
        swordData.Count_02 = 0;
        swordData.Count_03 = 0;
        shieldData.Count_01 = 0;
        shieldData.Count_02 = 0;
        shieldData.Count_03 = 0;
        shieldData.Count_04 = 0;
        hatData.Count_01 = 0;
        hatData.Count_02 = 0;
        hatData.Count_03 = 0;
        hatData.Count_04 = 0;
        clothData.Count_01 = 0;
        clothData.Count_02 = 0;
        clothData.Count_03 = 0;
        pantsData.Count_01 = 0;
        pantsData.Count_02 = 0;
        pantsData.Count_03 = 0;
        shoesData.Count_01 = 0;
        shoesData.Count_02 = 0;
        shoesData.Count_03 = 0;
        kloakData.Count_01 = 0;
        kloakData.Count_02 = 0;
        kloakData.Count_03 = 0;
        neckData.Count_01 = 0;
        neckData.Count_02 = 0;
        neckData.Count_03 = 0;
        neckData.Count_04 = 0;
        ringData.Count_01 = 0;
        ringData.Count_01 = 0;
        kloakData.Count_02 = 0;
        potionData.HPpotion01Count = 0;
        potionData.HPpotion02Count = 0;
        potionData.HPpotion03Count = 0;
        potionData.MPpotion01Count = 0;
        potionData.MPpotion02Count = 0;
        potionData.MPpotion03Count = 0;
        materialData.material01Count = 0;
        materialData.material02Count = 0;
        materialData.material03Count = 0;
        materialData.material04Count = 0;
        materialData.material05Count = 0;
        materialData.material06Count = 0;
        materialData.material07Count = 0;
        materialData.material08Count = 0;
    }
}
[System.Serializable]
public class ItemDrop
{
    public float dropRate;
    public System.Action getItemAction;
}
