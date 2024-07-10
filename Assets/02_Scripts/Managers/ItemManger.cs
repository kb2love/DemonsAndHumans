using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static ItemData;
public class ItemManger : MonoBehaviour
{
    public static ItemManger itemInst;
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
    [SerializeField] List<GameObject> items;
    [SerializeField] Text goldText;
    Transform itemGroup;
    PlayerAttack playerAttack;
    [SerializeField] List<RectTransform> itemList = new List<RectTransform>();
    [SerializeField] List<RectTransform> windowList = new List<RectTransform>();
    private Dictionary<ItemData.ItemType, List<ItemDrop>> itemDropTable;
    private void Awake()
    {
        if (itemInst == null)
        {
            itemInst = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (itemInst != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
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
        AllItemCheck();
    }
    //************** 검얻기****************//
    public void GetSword01()
    {
        CreatItem(ItemType.Sword, swordData.sword01, ref swordData.sword01Count, ref swordData.sword01Idx,
     new float[] { swordData.sword01Damage }, swordData.sword01Name, swordData.sword01Explain);

    }
    public void GetSword02()
    {
        CreatItem(ItemType.Sword, swordData.sword02, ref swordData.sword01Count, ref swordData.sword01Idx, new float[] { swordData.sword02Damage }, swordData.sword02Name, swordData.sword02Explain);
    }

    public void GetSword03()
    {
        CreatItem(ItemType.Sword, swordData.sword03, ref swordData.sword01Count, ref swordData.sword01Idx, new float[] { swordData.sword03Damage }, swordData.sword03Name, swordData.sword03Explain);
    }

    //************** 방패 얻기 ****************//
    public void GetShield01()
    {
        CreatItem(ItemType.Shield, shieldData.shield01, ref shieldData.shield01Count, ref shieldData.shield01Idx, new float[] { shieldData.shield01Value }, shieldData.shield01Name, shieldData.shield01Explain);
    }

    public void GetShield02()
    {
        CreatItem(ItemType.Shield, shieldData.shield02, ref shieldData.shield02Count, ref shieldData.shield02Idx, new float[] { shieldData.shield02Value }, shieldData.shield02Name, shieldData.shield02Explain);
    }

    public void GetShield03()
    {
        CreatItem(ItemType.Shield, shieldData.shield03, ref shieldData.shield03Count, ref shieldData.shield03Idx, new float[] { shieldData.shield03Value }, shieldData.shield03Name, shieldData.shield03Explain);
    }

    public void GetShield04()
    {
        CreatItem(ItemType.Shield, shieldData.shield04, ref shieldData.shield04Count, ref shieldData.shield04Idx, new float[] { shieldData.shield04Value }, shieldData.shield04Name, shieldData.shield04Explain);
    }
    //************** 헬멧 얻기 ****************//
    public void GetHat01()
    {
        CreatItem(ItemType.Hat, hatData.hat01, ref hatData.Hat01Count, ref hatData.hat01Idx, new float[] { hatData.hat01HP, hatData.hat01Defence }, hatData.hat01Name, hatData.hat01Explain);
    }

    public void GetHat02()
    {
        CreatItem(ItemType.Hat, hatData.hat02, ref hatData.Hat02Count, ref hatData.hat02Idx, new float[] { hatData.hat02HP, hatData.hat02Defence }, hatData.hat02Name, hatData.hat02Explain);
    }

    public void GetHat03()
    {
        CreatItem(ItemType.Hat, hatData.hat03, ref hatData.Hat03Count, ref hatData.hat03Idx, new float[] { hatData.hat03HP, hatData.hat03Defence }, hatData.hat03Name, hatData.hat03Explain);
    }

    public void GetHat04()
    {
        CreatItem(ItemType.Hat, hatData.hat04, ref hatData.Hat04Count, ref hatData.hat04Idx, new float[] { hatData.hat04HP, hatData.hat04Defence }, hatData.hat04Name, hatData.hat04Explain);
    }

    //************** 옷 얻기 ****************//
    public void GetCloth01()
    {
        CreatItem(ItemType.Cloth, clothData.cloth01, ref clothData.cloth01Count, ref clothData.cloth01Idx, new float[] { clothData.cloth01HP, clothData.cloth01Defence }, clothData.cloth01Name, clothData.cloth01Explain);
    }

    public void GetCloth02()
    {
        CreatItem(ItemType.Cloth, clothData.cloth02, ref clothData.cloth02Count, ref clothData.cloth02Idx, new float[] { clothData.cloth02HP, clothData.cloth02Defence }, clothData.cloth02Name, clothData.cloth02Explain);
    }

    public void GetCloth03()
    {
        CreatItem(ItemType.Cloth, clothData.cloth03, ref clothData.cloth03Count, ref clothData.cloth03Idx, new float[] { clothData.cloth03HP, clothData.cloth03Defence }, clothData.cloth03Name, clothData.cloth03Explain);
    }

    //************** 바지 얻기 ****************//
    public void GetPants01()
    {
        CreatItem(ItemType.Pants, pantsData.pants01, ref pantsData.pants01Count, ref pantsData.pants01Idx, new float[] { pantsData.pants01HP, pantsData.pants01Defence }, pantsData.pants01Name, pantsData.pants01Explain);
    }

    public void GetPants02()
    {
        CreatItem(ItemType.Pants, pantsData.pants02, ref pantsData.pants02Count, ref pantsData.pants02Idx, new float[] { pantsData.pants02HP, pantsData.pants02Defence }, pantsData.pants02Name, pantsData.pants02Explain);
    }

    public void GetPants03()
    {
        CreatItem(ItemType.Pants, pantsData.pants03, ref pantsData.pants03Count, ref pantsData.pants03Idx, new float[] { pantsData.pants03HP, pantsData.pants03Defence }, pantsData.pants03Name, pantsData.pants03Explain);
    }

    //************** 신발 얻기 ****************//
    public void GetShoes01()
    {
        CreatItem(ItemType.Shoes, shoesData.shoes01, ref shoesData.shoes01Count, ref shoesData.shoes01Idx, new float[] { shoesData.shoes01HP, shoesData.shoes01Defence }, shoesData.shoes01Name, shoesData.shoes01Explain);
    }

    public void GetShoes02()
    {
        CreatItem(ItemType.Shoes, shoesData.shoes02, ref shoesData.shoes02Count, ref shoesData.shoes02Idx, new float[] { shoesData.shoes02HP, shoesData.shoes02Defence }, shoesData.shoes02Name, shoesData.shoes02Explain);
    }

    public void GetShoes03()
    {
        CreatItem(ItemType.Shoes, shoesData.shoes03, ref shoesData.shoes03Count, ref shoesData.shoes03Idx, new float[] { shoesData.shoes03HP, shoesData.shoes03Defence }, shoesData.shoes03Name, shoesData.shoes03Explain);
    }

    //************** 망토 얻기 ****************//
    public void GetKloak01()
    {
        CreatItem(ItemType.Kloak, kloakData.kloak01, ref kloakData.kloak01Count, ref kloakData.kloak01Idx, new float[] { kloakData.kloak01HP, kloakData.kloak01MP, kloakData.kloak01Defence }, kloakData.kloak01Name, kloakData.kloak01Explain);
    }

    public void GetKloak02()
    {
        CreatItem(ItemType.Kloak, kloakData.kloak02, ref kloakData.kloak02Count, ref kloakData.kloak02Idx, new float[] { kloakData.kloak02HP, kloakData.kloak02MP, kloakData.kloak02Defence }, kloakData.kloak02Name, kloakData.kloak02Explain);
    }

    public void GetKloak03()
    {
        CreatItem(ItemType.Kloak, kloakData.kloak03, ref kloakData.kloak03Count, ref kloakData.kloak03Idx, new float[] { kloakData.kloak03HP, kloakData.kloak03MP, kloakData.kloak03Defence }, kloakData.kloak03Name, kloakData.kloak03Explain);
    }

    //************** 목걸이 얻기 ****************//
    public void GetNeck01()
    {
        CreatItem(ItemType.Neck, neckData.neck01, ref neckData.neck01Count, ref neckData.neck01Idx, new float[] { neckData.neck01MP, neckData.neck01Damage, neckData.neck01MagicDamage }, neckData.neck01Name, neckData.neck01Explain);
    }

    public void GetNeck02()
    {
        CreatItem(ItemType.Neck, neckData.neck02, ref neckData.neck02Count, ref neckData.neck02Idx, new float[] { neckData.neck02MP, neckData.neck02Damage, neckData.neck02MagicDamage }, neckData.neck02Name, neckData.neck02Explain);
    }

    public void GetNeck03()
    {
        CreatItem(ItemType.Neck, neckData.neck03, ref neckData.neck03Count, ref neckData.neck03Idx, new float[] { neckData.neck03MP, neckData.neck03Damage, neckData.neck03MagicDamage }, neckData.neck03Name, neckData.neck03Explain);
    }

    public void GetNeck04()
    {
        CreatItem(ItemType.Neck, neckData.neck04, ref neckData.neck04Count, ref neckData.neck04Idx, new float[] { neckData.neck04MP, neckData.neck04Damage, neckData.neck04MagicDamage }, neckData.neck04Name, neckData.neck04Explain);
    }

    //************** 반지 얻기 ****************//
    public void GetRing01()
    {
        CreatItem(ItemType.Ring, ringData.ring01, ref ringData.ring01Count, ref ringData.ring01Idx, new float[] { ringData.ring01HP, ringData.ring01MP, ringData.ring01Damage, ringData.ring01Defence, ringData.ring01MagicDamage, ringData.ring01FatalProbability, ringData.ring01FatalValue }, ringData.ring01Name, ringData.ring01Explain);
    }

    public void GetRing02()
    {
        CreatItem(ItemType.Ring, ringData.ring02, ref ringData.ring02Count, ref ringData.ring02Idx, new float[] { ringData.ring02HP, ringData.ring02MP, ringData.ring02Damage, ringData.ring02Defence, ringData.ring02MagicDamage, ringData.ring02FatalProbability, ringData.ring02FatalValue }, ringData.ring02Name, ringData.ring02Explain);
    }

    //************** HP 포션 얻기 ****************//
    public void GetHPpotion01()
    {
        potionData.HPpotion01Count++;
        if (potionData.HPpotion01Count > 1)
        {
            itemList[potionData.HPpotion01Idx].GetComponentInChildren<TextMeshProUGUI>().text = potionData.HPpotion01Count.ToString();
        }
        else
        {
            potionData.HPpotion01Idx = itemList.Count;
            CreatItem(ItemType.HPPotion, potionData.HPpotion01, ref potionData.HPpotion01Count, ref potionData.HPpotion01Idx, new float[] { potionData.HPpotion01Value }, potionData.HPpotion01Name, potionData.HPpotion01Explain);
        }
    }

    public void GetHPpotion02()
    {
        potionData.HPpotion02Count++;
        if (potionData.HPpotion02Count > 1)
        {
            itemList[potionData.HPpotion02Idx].GetComponentInChildren<TextMeshProUGUI>().text = potionData.HPpotion02Count.ToString();
        }
        else
        {
            potionData.HPpotion02Idx = itemList.Count;
            CreatItem(ItemType.HPPotion, potionData.HPpotion02, ref potionData.HPpotion02Count, ref potionData.HPpotion02Idx, new float[] { potionData.HPpotion02Value }, potionData.HPpotion02Name, potionData.HPpotion02Explain);
        }
    }

    public void GetHPpotion03()
    {
        potionData.HPpotion03Count++;
        if (potionData.HPpotion03Count > 1)
        {
            itemList[potionData.HPpotion03Idx].GetComponentInChildren<TextMeshProUGUI>().text = potionData.HPpotion03Count.ToString();
        }
        else
        {
            potionData.HPpotion03Idx = itemList.Count;
            CreatItem(ItemType.HPPotion, potionData.HPpotion03, ref potionData.HPpotion03Count, ref potionData.HPpotion03Idx, new float[] { potionData.HPpotion03Value }, potionData.HPpotion03Name, potionData.HPpotion03Explain);
        }
    }

    //************** MP 포션 얻기 ****************//
    public void GetMPpotion01()
    {
        potionData.MPpotion01Count++;
        if (potionData.MPpotion01Count > 1)
        {
            itemList[potionData.MPpotion01Idx].GetComponentInChildren<TextMeshProUGUI>().text = potionData.MPpotion01Count.ToString();
        }
        else
        {
            potionData.MPpotion01Idx = itemList.Count;
            CreatItem(ItemType.MPPotion, potionData.MPpotion01, ref potionData.MPpotion01Count, ref potionData.MPpotion01Idx, new float[] { potionData.MPpotion01Value }, potionData.MPpotion01Name, potionData.MPpotion01Explain);
        }
    }

    public void GetMPpotion02()
    {
        potionData.MPpotion02Count++;
        if (potionData.MPpotion02Count > 1)
        {
            itemList[potionData.MPpotion02Idx].GetComponentInChildren<TextMeshProUGUI>().text = potionData.MPpotion02Count.ToString();
        }
        else
        {
            potionData.MPpotion02Idx = itemList.Count;
            CreatItem(ItemType.MPPotion, potionData.MPpotion02, ref potionData.MPpotion02Count, ref potionData.MPpotion02Idx, new float[] { potionData.MPpotion02Value }, potionData.MPpotion02Name, potionData.MPpotion02Explain);
        }
    }

    public void GetMPpotion03()
    {
        potionData.MPpotion03Count++;
        if (potionData.MPpotion03Count > 1)
        {
            itemList[potionData.MPpotion03Idx].GetComponentInChildren<TextMeshProUGUI>().text = potionData.MPpotion03Count.ToString();
        }
        else
        {
            potionData.MPpotion03Idx = itemList.Count;
            CreatItem(ItemType.MPPotion, potionData.MPpotion03, ref potionData.MPpotion03Count, ref potionData.MPpotion03Idx, new float[] { potionData.MPpotion03Value }, potionData.MPpotion03Name, potionData.MPpotion03Explain);
        }
    }

    //************** 재료 얻기 ****************//
    public void GetMaterial01()
    {
        materialData.material01Count++;
        if (materialData.material01Count > 1)
        {
            itemList[materialData.material01Idx].GetComponentInChildren<TextMeshProUGUI>().text = materialData.material01Count.ToString();
        }
        else
        {
            materialData.material01Idx = itemList.Count;
            CreatItem(ItemType.Material, materialData.material01, ref materialData.material01Count, ref materialData.material01Idx, null, materialData.material01Name, materialData.material01Explain);
        }
    }

    public void GetMaterial02()
    {
        materialData.material02Count++;
        if (materialData.material02Count > 1)
        {
            itemList[materialData.material02Idx].GetComponentInChildren<TextMeshProUGUI>().text = materialData.material02Count.ToString();
        }
        else
        {
            materialData.material02Idx = itemList.Count;
            CreatItem(ItemType.Material, materialData.material02, ref materialData.material02Count, ref materialData.material02Idx, null, materialData.material02Name, materialData.material02Explain);
        }
    }

    public void GetMaterial03()
    {
        materialData.material03Count++;
        if (materialData.material03Count > 1)
        {
            itemList[materialData.material03Idx].GetComponentInChildren<TextMeshProUGUI>().text = materialData.material03Count.ToString();
        }
        else
        {
            materialData.material03Idx = itemList.Count;
            CreatItem(ItemType.Material, materialData.material03, ref materialData.material03Count, ref materialData.material03Idx, null, materialData.material03Name, materialData.material03Explain);
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
            CreatItem(ItemType.Material, materialData.material04, ref materialData.material04Count, ref materialData.material04Idx, null, materialData.material04Name, materialData.material04Explain);
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
            CreatItem(ItemType.Material, materialData.material05, ref materialData.material05Count, ref materialData.material05Idx, null, materialData.material05Name, materialData.material05Explain);
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
            CreatItem(ItemType.Material, materialData.material06, ref materialData.material06Count, ref materialData.material06Idx, null, materialData.material06Name, materialData.material06Explain);
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
            CreatItem(ItemType.Material, materialData.material07, ref materialData.material07Count, ref materialData.material07Idx, null, materialData.material07Name, materialData.material07Explain);
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
            CreatItem(ItemType.Material, materialData.material08, ref materialData.material08Count, ref materialData.material08Idx, null, materialData.material08Name, materialData.material08Explain);
        }
    }

    public void GetItem(ItemType itemType)         //드롭으로 확률적으로 얻는 아이템
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
                    new ItemDrop { dropRate = 75f, getItemAction = GetMaterial06},
                    new ItemDrop { dropRate = 87.5f, getItemAction = GetMaterial07 },
                    new ItemDrop { dropRate = 100f, getItemAction = GetMaterial08 }
                }
            }
            // 다른 아이템 유형들도 동일하게 추가
        };

    }
    public void CreatItem(ItemType itemType, Sprite itemSprite, ref int itemCount, ref int itemIdx,
    float[] values, string itemName, string itemExplain, UnityAction onClickAction = null, int price = 0)
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
            case ItemType.HPPotion:
                var text = Instantiate(itemData.itemCountText, item);
                text.GetComponent<Text>().text = itemCount.ToString();
                item.AddComponent<Button>();
                item.GetComponent<Button>().onClick.AddListener(onClickAction);
                break;
            case ItemType.MPPotion:
                var text02 = Instantiate(itemData.itemCountText, item);
                text02.GetComponent<Text>().text = itemCount.ToString();
                item.AddComponent<Button>();
                item.GetComponent<Button>().onClick.AddListener(onClickAction);
                break;
            case ItemType.Material:
                var matText = Instantiate(itemData.itemCountText, item);
                matText.GetComponent<Text>().text = itemCount.ToString();
                item.GetComponent<Drag>().price = price;
                break;
        }

        item.GetComponent<ItemToolTip>().itemName = itemName;
        item.GetComponent<ItemToolTip>().itemExplain = itemExplain;
        item.SetParent(window);
        item.gameObject.SetActive(true);
    }
    public void AllItemCheck()
    {
        // 아이템 체크
        ItemCheck(ref swordData.sword01Count, GetSword01);
        ItemCheck(ref swordData.sword02Count, GetSword02);
        ItemCheck(ref swordData.sword03Count, GetSword03);
        ItemCheck(ref shieldData.shield01Count, GetShield01);
        ItemCheck(ref shieldData.shield02Count, GetShield02);
        ItemCheck(ref shieldData.shield03Count, GetShield03);
        ItemCheck(ref shieldData.shield04Count, GetShield04);
        ItemCheck(ref hatData.Hat01Count, GetHat01);
        ItemCheck(ref hatData.Hat02Count, GetHat02);
        ItemCheck(ref hatData.Hat03Count, GetHat03);
        ItemCheck(ref hatData.Hat04Count, GetHat04);
        ItemCheck(ref clothData.cloth01Count, GetCloth01);
        ItemCheck(ref clothData.cloth02Count, GetCloth02);
        ItemCheck(ref clothData.cloth03Count, GetCloth03);
        ItemCheck(ref pantsData.pants01Count, GetPants01);
        ItemCheck(ref pantsData.pants02Count, GetPants02);
        ItemCheck(ref pantsData.pants03Count, GetPants03);
        ItemCheck(ref shoesData.shoes01Count, GetShoes01);
        ItemCheck(ref shoesData.shoes02Count, GetShoes02);
        ItemCheck(ref shoesData.shoes03Count, GetShoes03);
        ItemCheck(ref kloakData.kloak01Count, GetKloak01);
        ItemCheck(ref kloakData.kloak02Count, GetKloak02);
        ItemCheck(ref kloakData.kloak03Count, GetKloak03);
        ItemCheck(ref neckData.neck01Count, GetNeck01);
        ItemCheck(ref neckData.neck02Count, GetNeck02);
        ItemCheck(ref neckData.neck03Count, GetNeck03);
        ItemCheck(ref neckData.neck04Count, GetNeck04);
        ItemCheck(ref ringData.ring01Count, GetRing01);
        ItemCheck(ref ringData.ring02Count, GetRing02);

        CheckPotion(ItemData.ItemType.HPPotion, ref potionData.HPpotion01Count, ref potionData.HPpotion01Idx, potionData.HPpotion01, potionData.HPpotion01Name, potionData.HPpotion01Explain, HPHeal01);
        CheckPotion(ItemData.ItemType.HPPotion, ref potionData.HPpotion02Count, ref potionData.HPpotion02Idx, potionData.HPpotion02, potionData.HPpotion02Name, potionData.HPpotion02Explain, HPHeal02);
        CheckPotion(ItemData.ItemType.HPPotion, ref potionData.HPpotion03Count, ref potionData.HPpotion03Idx, potionData.HPpotion03, potionData.HPpotion03Name, potionData.HPpotion03Explain, HPHeal03);
        CheckPotion(ItemData.ItemType.MPPotion, ref potionData.MPpotion01Count, ref potionData.MPpotion01Idx, potionData.MPpotion01, potionData.MPpotion01Name, potionData.MPpotion01Explain, MPHeal01);
        CheckPotion(ItemData.ItemType.MPPotion, ref potionData.MPpotion02Count, ref potionData.MPpotion02Idx, potionData.MPpotion02, potionData.MPpotion02Name, potionData.MPpotion02Explain, MPHeal02);
        CheckPotion(ItemData.ItemType.MPPotion, ref potionData.MPpotion03Count, ref potionData.MPpotion03Idx, potionData.MPpotion03, potionData.MPpotion03Name, potionData.MPpotion03Explain, MPHeal03);

        CheckMaterial(ItemData.ItemType.Material, ref materialData.material01Count, ref materialData.material01Idx, materialData.material01, materialData.material01Price, materialData.material01Name, materialData.material01Explain);
        CheckMaterial(ItemData.ItemType.Material, ref materialData.material02Count, ref materialData.material02Idx, materialData.material02, materialData.material02Price, materialData.material02Name, materialData.material02Explain);
        CheckMaterial(ItemData.ItemType.Material, ref materialData.material03Count, ref materialData.material03Idx, materialData.material03, materialData.material03Price, materialData.material03Name, materialData.material03Explain);
        CheckMaterial(ItemData.ItemType.Material, ref materialData.material04Count, ref materialData.material04Idx, materialData.material04, materialData.material04Price, materialData.material04Name, materialData.material04Explain);
        CheckMaterial(ItemData.ItemType.Material, ref materialData.material05Count, ref materialData.material05Idx, materialData.material05, materialData.material05Price, materialData.material05Name, materialData.material05Explain);
        CheckMaterial(ItemData.ItemType.Material, ref materialData.material06Count, ref materialData.material06Idx, materialData.material06, materialData.material06Price, materialData.material06Name, materialData.material06Explain);
        CheckMaterial(ItemData.ItemType.Material, ref materialData.material07Count, ref materialData.material07Idx, materialData.material07, materialData.material07Price, materialData.material07Name, materialData.material07Explain);
        CheckMaterial(ItemData.ItemType.Material, ref materialData.material08Count, ref materialData.material08Idx, materialData.material08, materialData.material08Price, materialData.material08Name, materialData.material08Explain);
    }

    public void ItemCheck(ref int itemCount, UnityAction action)
    {
        if (itemCount > 0)
        {
            itemCount--;
            action();
        }
    }

    private void CheckPotion(ItemData.ItemType type, ref int potionCount, ref int potionIdx, Sprite potionSprite, string potionName, string potionExplain, UnityAction potionEffect)
    {
        if (potionCount > 0)
        {
            CreatItem(type, potionSprite, ref potionCount, ref potionIdx, null, potionName, potionExplain, potionEffect);
        }
    }

    private void CheckMaterial(ItemData.ItemType type, ref int materialCount, ref int materialIdx, Sprite materialSprite, int materialPrice, string materialName, string materialExplain)
    {
        if (materialCount > 0)
        {
            CreatItem(type, materialSprite, ref materialCount, ref materialIdx, null, materialName, materialExplain);
        }
    }

    public void GetGold()
    { // 골드얻기
        playerData.GoldValue += Random.Range(1, 10);
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
    //*****************************포션 사용*********************************//

    public void HPHeal01()
    {
        playerData.HP += 50;
        if (playerData.HP >= playerData.MaxHP)
        {
            playerData.HP = playerData.MaxHP;
        }
        potionData.HPpotion01Count--;
        itemList[potionData.HPpotion01Idx].GetComponentInChildren<Text>().text = potionData.HPpotion01Count.ToString();
        if (potionData.HPpotion01Count == 0)
        {
            itemList[potionData.HPpotion01Idx].gameObject.SetActive(false);
        }
        GameManager.GM.StatUpdate(PlayerData.PlayerStat.HP);
    }
    public void HPHeal02()
    {
        playerData.HP += 100;
        if (playerData.HP >= playerData.MaxHP)
        {
            playerData.HP = playerData.MaxHP;
        }
        potionData.HPpotion02Count--;
        itemList[potionData.HPpotion02Idx].GetComponentInChildren<Text>().text = potionData.HPpotion02Count.ToString();
        if (potionData.HPpotion02Count == 0)
        {
            itemList[potionData.HPpotion02Idx].gameObject.SetActive(false);
        }
        GameManager.GM.StatUpdate(PlayerData.PlayerStat.HP);
    }
    public void HPHeal03()
    {
        playerData.HP += 500;
        if (playerData.HP >= playerData.MaxHP)
        {
            playerData.HP = playerData.MaxHP;
        }
        potionData.HPpotion03Count--;
        itemList[potionData.HPpotion03Idx].GetComponentInChildren<Text>().text = potionData.HPpotion03Count.ToString();
        if (potionData.HPpotion03Count == 0)
        {
            itemList[potionData.HPpotion03Idx].gameObject.SetActive(false);
        }
        GameManager.GM.StatUpdate(PlayerData.PlayerStat.HP);
    }
    public void MPHeal01()
    {
        playerData.MP += 50;
        if (playerData.MP >= playerData.MaxMP)
        {
            playerData.MP = playerData.MaxMP;
        }
        potionData.MPpotion01Count--;
        itemList[potionData.MPpotion01Idx].GetComponentInChildren<Text>().text = potionData.MPpotion01Count.ToString();
        if (potionData.MPpotion01Count == 0)
        {
            itemList[potionData.MPpotion01Idx].gameObject.SetActive(false);
        }
        GameManager.GM.StatUpdate(PlayerData.PlayerStat.MP);
    }
    public void MPHeal02()
    {
        playerData.MP += 100;
        if (playerData.MP >= playerData.MaxMP)
        {
            playerData.MP = playerData.MaxMP;
        }
        potionData.MPpotion02Count--;
        itemList[potionData.MPpotion02Idx].GetComponentInChildren<Text>().text = potionData.MPpotion02Count.ToString();
        if (potionData.MPpotion02Count == 0)
        {
            itemList[potionData.MPpotion02Idx].gameObject.SetActive(false);
        }
        GameManager.GM.StatUpdate(PlayerData.PlayerStat.MP);
    }
    public void MPHeal03()
    {
        playerData.MP += 500;
        if (playerData.MP >= playerData.MaxMP)
        {
            playerData.MP = playerData.MaxMP;
        }
        potionData.MPpotion03Count--;
        itemList[potionData.MPpotion03Idx].GetComponentInChildren<Text>().text = potionData.MPpotion03Count.ToString();
        if (potionData.MPpotion03Count == 0)
        {
            itemList[potionData.MPpotion03Idx].gameObject.SetActive(false);
        }
        GameManager.GM.StatUpdate(PlayerData.PlayerStat.MP);
    }
    //****************************아이템 착용 확인**************************//
    public void SwordOnOff(bool _active)
    {
        items[0].SetActive(_active);
        playerAttack.IsSword(_active);

    }
    public void ShieldTakeOff(bool _active)
    {
        items[1].SetActive(_active);
        playerAttack.IsShield(_active);
    }
}
[System.Serializable]
public class ItemDrop
{
    public float dropRate;
    public System.Action getItemAction;
}
