using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour
{
    public static ItemManager itemInst;
    [Header("아이템 데이터")]
    [SerializeField] SunDryItemData itemData;
    [SerializeField] ItemData swordData;
    [SerializeField] ItemData shieldData;
    [SerializeField] ItemData hatData;
    [SerializeField] ItemData clothData;
    [SerializeField] ItemData pantsData;
    [SerializeField] ItemData shoesData;
    [SerializeField] ItemData kloakData;
    [SerializeField] ItemData neckData;
    [SerializeField] ItemData ringData;
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
    [SerializeField] List<RectTransform> windowList = new List<RectTransform>(); // 아이템들이 들어갈 리스트
    List<Transform> quickSlot = new List<Transform>();          // 아이템 퀵슬롯 이미지리스트
    private Dictionary<ItemType, List<ItemDrop>> itemDropTable; // 아이템들을 얻을때 확률을 조정할때필요한 변수
    bool isSelect;
    // itemDataJsons
    ItemDataJson[] swordDataJson = new ItemDataJson[3];
    ItemDataJson[] shieldDataJson = new ItemDataJson[4];
    ItemDataJson[] hatDataJson = new ItemDataJson[4];
    ItemDataJson[] clothDataJson = new ItemDataJson[3];
    ItemDataJson[] pantsDataJson = new ItemDataJson[3];
    ItemDataJson[] shoesDataJson = new ItemDataJson[3];
    ItemDataJson[] kloakDataJson = new ItemDataJson[3];
    ItemDataJson[] neckDataJson = new ItemDataJson[4];
    ItemDataJson[] ringDataJson = new ItemDataJson[2];
    ItemDataJson[] potionJson = new ItemDataJson[6];
    public ItemDataJson[] materialJson = new ItemDataJson[8];
    GameManager GM;
    private void Awake()
    {
        itemInst = this;
    }
    private void Start()
    {
        GM = GameManager.GM;
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
        for (int i = 0; i < 4; i++)
        {
            quickSlot.Add(quick.GetChild(i).GetComponent<Transform>());
        }
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        getItem = playerAttack.GetComponent<PlayerUIController>();

    }
    public void Initialize()
    {
        DataManager.dataInst.uiElements = itemList;
        AllItemDataJsonFind();
        InitializeItemDropTable();
        AllItemCheck();
    }

    private void AllItemDataJsonFind()
    {
        swordDataJson[0] = DataManager.dataInst.FindItem(swordData.Name_01);
        swordDataJson[1] = DataManager.dataInst.FindItem(swordData.Name_02);
        swordDataJson[2] = DataManager.dataInst.FindItem(swordData.Name_03);
        shieldDataJson[0] = DataManager.dataInst.FindItem(shieldData.Name_01);
        shieldDataJson[1] = DataManager.dataInst.FindItem(shieldData.Name_02);
        shieldDataJson[2] = DataManager.dataInst.FindItem(shieldData.Name_03);
        shieldDataJson[3] = DataManager.dataInst.FindItem(shieldData.Name_04);
        hatDataJson[0] = DataManager.dataInst.FindItem(hatData.Name_01);
        hatDataJson[1] = DataManager.dataInst.FindItem(hatData.Name_02);
        hatDataJson[2] = DataManager.dataInst.FindItem(hatData.Name_03);
        hatDataJson[3] = DataManager.dataInst.FindItem(hatData.Name_03);
        clothDataJson[0] = DataManager.dataInst.FindItem(clothData.Name_01);
        clothDataJson[1] = DataManager.dataInst.FindItem(clothData.Name_02);
        clothDataJson[2] = DataManager.dataInst.FindItem(clothData.Name_03);
        pantsDataJson[0] = DataManager.dataInst.FindItem(pantsData.Name_01);
        pantsDataJson[1] = DataManager.dataInst.FindItem(pantsData.Name_02);
        pantsDataJson[2] = DataManager.dataInst.FindItem(pantsData.Name_03);
        shoesDataJson[0] = DataManager.dataInst.FindItem(shoesData.Name_01);
        shoesDataJson[1] = DataManager.dataInst.FindItem(shoesData.Name_02);
        shoesDataJson[2] = DataManager.dataInst.FindItem(shoesData.Name_03);
        kloakDataJson[0] = DataManager.dataInst.FindItem(kloakData.Name_01);
        kloakDataJson[1] = DataManager.dataInst.FindItem(kloakData.Name_02);
        kloakDataJson[2] = DataManager.dataInst.FindItem(kloakData.Name_03);
        neckDataJson[0] = DataManager.dataInst.FindItem(neckData.Name_01);
        neckDataJson[1] = DataManager.dataInst.FindItem(neckData.Name_02);
        neckDataJson[2] = DataManager.dataInst.FindItem(neckData.Name_03);
        neckDataJson[3] = DataManager.dataInst.FindItem(neckData.Name_04);
        ringDataJson[0] = DataManager.dataInst.FindItem(ringData.Name_01);
        ringDataJson[1] = DataManager.dataInst.FindItem(ringData.Name_02);
        potionJson[0] = DataManager.dataInst.FindItem(potionData.HPpotion01Name);
        potionJson[1] = DataManager.dataInst.FindItem(potionData.HPpotion02Name);
        potionJson[2] = DataManager.dataInst.FindItem(potionData.HPpotion03Name);
        potionJson[3] = DataManager.dataInst.FindItem(potionData.MPpotion01Name);
        potionJson[4] = DataManager.dataInst.FindItem(potionData.MPpotion02Name);
        potionJson[5] = DataManager.dataInst.FindItem(potionData.MPpotion03Name);
        materialJson[0] = DataManager.dataInst.FindItem(materialData.material01Name);
        materialJson[1] = DataManager.dataInst.FindItem(materialData.material02Name);
        materialJson[2] = DataManager.dataInst.FindItem(materialData.material03Name);
        materialJson[3] = DataManager.dataInst.FindItem(materialData.material04Name);
        materialJson[4] = DataManager.dataInst.FindItem(materialData.material05Name);
        materialJson[5] = DataManager.dataInst.FindItem(materialData.material06Name);
        materialJson[6] = DataManager.dataInst.FindItem(materialData.material07Name);
        materialJson[7] = DataManager.dataInst.FindItem(materialData.material08Name);
    }
    #region 아이템 얻기
    //************** 검얻기****************//
    public void GetSword01() { CreateWeaponItem(ItemType.Sword, swordData.Image_01, ref swordDataJson[0], new float[] { swordData.Value_01[0] }, swordData.Name_01); }
    public void GetSword02() { CreateWeaponItem(ItemType.Sword, swordData.Image_02, ref swordDataJson[1], new float[] { swordData.Value_02[0] }, swordData.Name_02); }
    public void GetSword03() { CreateWeaponItem(ItemType.Sword, swordData.Image_03, ref swordDataJson[2], new float[] { swordData.Value_03[0] }, swordData.Name_03); }
    // 방패 얻기
    public void GetShield01() { CreateWeaponItem(ItemType.Shield, shieldData.Image_01, ref shieldDataJson[0], new float[] { shieldData.Value_01[0] }, shieldData.Name_01); }
    public void GetShield02() { CreateWeaponItem(ItemType.Shield, shieldData.Image_02, ref shieldDataJson[1], new float[] { shieldData.Value_02[0] }, shieldData.Name_02); }
    public void GetShield03() { CreateWeaponItem(ItemType.Shield, shieldData.Image_03, ref shieldDataJson[2], new float[] { shieldData.Value_03[0] }, shieldData.Name_03); }
    public void GetShield04() { CreateWeaponItem(ItemType.Shield, shieldData.Image_04, ref shieldDataJson[3], new float[] { shieldData.Value_04[0] }, shieldData.Name_04); }
    // 헬멧 얻기
    public void GetHat01() { CreateWeaponItem(ItemType.Hat, hatData.Image_01, ref hatDataJson[0], new float[] { hatData.Value_01[0], hatData.Value_01[1] }, hatData.Name_01); }
    public void GetHat02() { CreateWeaponItem(ItemType.Hat, hatData.Image_02, ref hatDataJson[1], new float[] { hatData.Value_02[0], hatData.Value_02[1] }, hatData.Name_02); }
    public void GetHat03() { CreateWeaponItem(ItemType.Hat, hatData.Image_03, ref hatDataJson[2], new float[] { hatData.Value_03[0], hatData.Value_03[1] }, hatData.Name_03); }
    public void GetHat04() { CreateWeaponItem(ItemType.Hat, hatData.Image_04, ref hatDataJson[3], new float[] { hatData.Value_04[0], hatData.Value_04[1] }, hatData.Name_04); }
    // 옷 얻기
    public void GetCloth01() { CreateWeaponItem(ItemType.Cloth, clothData.Image_01, ref clothDataJson[0], new float[] { clothData.Value_01[0], clothData.Value_01[1] }, clothData.Name_01); }
    public void GetCloth02() { CreateWeaponItem(ItemType.Cloth, clothData.Image_02, ref clothDataJson[1], new float[] { clothData.Value_02[0], clothData.Value_02[1] }, clothData.Name_02); }
    public void GetCloth03() { CreateWeaponItem(ItemType.Cloth, clothData.Image_03, ref clothDataJson[2], new float[] { clothData.Value_03[0], clothData.Value_03[1] }, clothData.Name_03); }
    // 바지 얻기
    public void GetPants01() { CreateWeaponItem(ItemType.Pants, pantsData.Image_01, ref pantsDataJson[0], new float[] { pantsData.Value_01[0], pantsData.Value_01[1] }, pantsData.Name_01); }
    public void GetPants02() { CreateWeaponItem(ItemType.Pants, pantsData.Image_02, ref pantsDataJson[1], new float[] { pantsData.Value_02[0], pantsData.Value_02[1] }, pantsData.Name_02); }
    public void GetPants03() { CreateWeaponItem(ItemType.Pants, pantsData.Image_03, ref pantsDataJson[2], new float[] { pantsData.Value_03[0], pantsData.Value_03[1] }, pantsData.Name_03); }
    // 신발 얻기
    public void GetShoes01() { CreateWeaponItem(ItemType.Shoes, shoesData.Image_01, ref shoesDataJson[0], new float[] { shoesData.Value_01[0], shoesData.Value_01[1] }, shoesData.Name_01); }
    public void GetShoes02() { CreateWeaponItem(ItemType.Shoes, shoesData.Image_02, ref shoesDataJson[1], new float[] { shoesData.Value_02[0], shoesData.Value_02[1] }, shoesData.Name_02); }
    public void GetShoes03() { CreateWeaponItem(ItemType.Shoes, shoesData.Image_03, ref shoesDataJson[2], new float[] { shoesData.Value_03[0], shoesData.Value_03[1] }, shoesData.Name_03); }
    // 망토 얻기
    public void GetKloak01() { CreateWeaponItem(ItemType.Kloak, kloakData.Image_01, ref kloakDataJson[0], new float[] { kloakData.Value_01[0], kloakData.Value_01[1], kloakData.Value_01[2] }, kloakData.Name_01); }
    public void GetKloak02() { CreateWeaponItem(ItemType.Kloak, kloakData.Image_02, ref kloakDataJson[1], new float[] { kloakData.Value_02[0], kloakData.Value_02[1], kloakData.Value_02[2] }, kloakData.Name_02); }
    public void GetKloak03() { CreateWeaponItem(ItemType.Kloak, kloakData.Image_03, ref kloakDataJson[2], new float[] { kloakData.Value_03[0], kloakData.Value_03[1], kloakData.Value_03[2] }, kloakData.Name_03); }
    // 목걸이 얻기
    public void GetNeck01() { CreateWeaponItem(ItemType.Neck, neckData.Image_01, ref neckDataJson[0], new float[] { neckData.Value_01[0], neckData.Value_01[1], neckData.Value_01[2] }, neckData.Name_01); }
    public void GetNeck02() { CreateWeaponItem(ItemType.Neck, neckData.Image_02, ref neckDataJson[1], new float[] { neckData.Value_02[0], neckData.Value_02[1], neckData.Value_02[2] }, neckData.Name_02); }
    public void GetNeck03() { CreateWeaponItem(ItemType.Neck, neckData.Image_03, ref neckDataJson[2], new float[] { neckData.Value_03[0], neckData.Value_03[1], neckData.Value_03[2] }, neckData.Name_03); }
    public void GetNeck04() { CreateWeaponItem(ItemType.Neck, neckData.Image_04, ref neckDataJson[3], new float[] { neckData.Value_04[0], neckData.Value_04[1], neckData.Value_04[2] }, neckData.Name_04); }
    // 반지 얻기
    public void GetRing01() { CreateWeaponItem(ItemType.Ring, ringData.Image_01, ref ringDataJson[0], new float[] { ringData.Value_01[0], ringData.Value_01[1], ringData.Value_01[2], ringData.Value_01[3], ringData.Value_01[4], ringData.Value_01[5], ringData.Value_01[6] }, ringData.Name_01); }
    public void GetRing02() { CreateWeaponItem(ItemType.Ring, ringData.Image_02, ref ringDataJson[1], new float[] { ringData.Value_02[0], ringData.Value_02[1], ringData.Value_02[2], ringData.Value_02[3], ringData.Value_02[4], ringData.Value_02[5], ringData.Value_02[6] }, ringData.Name_02); }
    //************** HP 포션 얻기 ****************//
    public void GetHPpotion01() { GetPotionMat(ref potionJson[0], potionData.HPpotion01, ItemType.HPPotion, potionData.HPpotion01Name, potionData.HPpotion01quick, HPHeal01, potionData.HPpotion01Value); }
    public void GetHPpotion02() { GetPotionMat(ref potionJson[1], potionData.HPpotion02, ItemType.HPPotion, potionData.HPpotion02Name, potionData.HPpotion02quick, HPHeal02, potionData.HPpotion02Value); }
    public void GetHPpotion03() { GetPotionMat(ref potionJson[2], potionData.HPpotion03, ItemType.HPPotion, potionData.HPpotion03Name, potionData.HPpotion03quick, HPHeal03, potionData.HPpotion03Value); }
    //************** MP 포션 얻기 ****************//
    public void GetMPpotion01() { GetPotionMat(ref potionJson[3], potionData.MPpotion01, ItemType.MPPotion, potionData.MPpotion01Name, potionData.MPpotion01quick, MPHeal01, potionData.MPpotion01Value); }
    public void GetMPpotion02() { GetPotionMat(ref potionJson[4], potionData.MPpotion02, ItemType.MPPotion, potionData.MPpotion02Name, potionData.MPpotion02quick, MPHeal02, potionData.MPpotion02Value); }
    public void GetMPpotion03() { GetPotionMat(ref potionJson[5], potionData.MPpotion03, ItemType.MPPotion, potionData.MPpotion03Name, potionData.MPpotion03quick, MPHeal03, potionData.MPpotion03Value); }

    //************** 재료 얻기 ****************//
    public void GetMaterial01() { GetPotionMat(ref materialJson[0], materialData.material01, ItemType.Material, materialData.material01Name, materialData.material01Price); }
    public void GetMaterial02() { GetPotionMat(ref materialJson[1], materialData.material02, ItemType.Material, materialData.material02Name, materialData.material02Price); }
    public void GetMaterial03() { GetPotionMat(ref materialJson[2], materialData.material03, ItemType.Material, materialData.material03Name, materialData.material03Price); }
    public void GetMaterial04() { GetPotionMat(ref materialJson[3], materialData.material04, ItemType.Material, materialData.material04Name, materialData.material04Price); }
    public void GetMaterial05() { GetPotionMat(ref materialJson[4], materialData.material05, ItemType.Material, materialData.material05Name, materialData.material05Price); }
    public void GetMaterial06() { GetPotionMat(ref materialJson[5], materialData.material06, ItemType.Material, materialData.material06Name, materialData.material06Price); }
    public void GetMaterial07() { GetPotionMat(ref materialJson[6], materialData.material07, ItemType.Material, materialData.material07Name, materialData.material07Price); }
    public void GetMaterial08() { GetPotionMat(ref materialJson[7], materialData.material08, ItemType.Material, materialData.material08Name, materialData.material08Price); }
    private void GetPotionMat(ref ItemDataJson itemDataJson, Sprite itemImage, ItemType type, string itemName, int priceAndQuickIdx, UnityAction action = null, float potionValue = 0)
    {
        itemDataJson.Count++;
        if (itemDataJson.Count > 1)
        {
            itemList[itemDataJson.Idx].GetComponentInChildren<Text>().text = itemDataJson.Count.ToString();
            string path = GetTransformPath(itemList[itemDataJson.Idx]);
            DataManager.dataInst.SaveItemData(itemName, path, itemDataJson.Count, itemDataJson.Idx);
        }
        else
        { itemDataJson.Idx = PotionMatCreate(type, itemImage, itemDataJson, itemName, priceAndQuickIdx, action, potionValue); }
    }
    #endregion
    #region 아이템 얻는 메소드
    public void GetItem(ItemType itemType, int goldValue = 0)
    {
        if (itemType == ItemType.Gold) { GoldPlus(Random.Range(goldValue, goldValue + 10)); return; }
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
                    new ItemDrop { dropRate = 80.0f, getItemAction = GetHPpotion01 },
                    new ItemDrop { dropRate = 95.0f, getItemAction = GetHPpotion02 },
                    new ItemDrop { dropRate = 100.0f, getItemAction = GetHPpotion03 }
                }
            },
            {
                ItemType.MPPotion, new List<ItemDrop>
                {
                    new ItemDrop { dropRate = 80.0f, getItemAction = GetMPpotion01 },
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
    public void CreateWeaponItem(ItemType itemType, Sprite itemSprite, ref ItemDataJson itemDataJson, float[] values, string itemName)
    {
        itemDataJson.Count++;
        if (itemDataJson.Count == 1)
        {
            RectTransform item = ItemVase(itemType, itemSprite, ref itemDataJson.Idx, itemName);
            string explain = "";
            explain = WeaponCase(itemType, values, item);
            item.GetComponent<ItemToolTip>().itemExplain = explain;
            RectTransform window = GetWindow();
            item.SetParent(window);
            string path = GetTransformPath(item);
            ItemSave( itemDataJson);
            ItemDataJson newitem = new ItemDataJson
            {
                Count = itemDataJson.Count,
                Name = itemName,
                Idx = itemDataJson.Idx,
                Path = path
            };
            item.GetComponent<Drag>().itemDataJson = newitem;
            item.gameObject.SetActive(true);
        }
        else if (itemDataJson.Count > 1) itemDataJson.Count = 1;
    }

    private RectTransform ItemVase(ItemType itemType, Sprite itemSprite, ref int itemIdx, string itemName)
    {
        for (int i = 0; i < itemList.Count; i++) { if (!itemList[i].gameObject.activeSelf) { itemIdx = i; break; } }
        var item = itemList[itemIdx];
        item.GetComponent<Image>().sprite = itemSprite;
        item.GetComponent<ItemInfo>().type = itemType;
        item.GetComponent<ItemToolTip>().itemName = itemName;
        return item;
    }

    private static string WeaponCase(ItemType itemType, float[] values, RectTransform item)
    {
        switch (itemType)
        {
            case ItemType.Sword:
                item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                return "공격력 + " + values[0].ToString();
            case ItemType.Shield:
                item.GetComponent<Drag>().ItemChange(itemType, values[0]);
                return "방어력 + " + values[0].ToString();
            case ItemType.Hat:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                return "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
            case ItemType.Cloth:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                return "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
            case ItemType.Pants:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                return "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
            case ItemType.Shoes:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1]);
                return "HP + " + values[0].ToString() + "방어력 + " + values[1].ToString();
            case ItemType.Kloak:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                return "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "공격력 + " + values[2].ToString();
            case ItemType.Neck:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2]);
                return "MP + " + values[0].ToString() + "공격력 + " + values[1].ToString() + "MagicDamage + " + values[2].ToString();
            case ItemType.Ring:
                item.GetComponent<Drag>().ItemChange(itemType, values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                return "HP + " + values[0].ToString() + "MP + " + values[1].ToString() + "방어력 + " + values[2].ToString() + "치명타공격력 + " +
                    values[3].ToString() + "치명타확률 + " + values[4].ToString() + "마법데미지" + values[5].ToString() + "공격력" + values[5].ToString();
        }
        return null;
    }

    private int PotionMatCreate(ItemType itemType, Sprite itemSprite, ItemDataJson itemDataJson, string itemName, int priceAndQuickIdx, UnityAction onClickAction, float potionValue = 0)
    {
        RectTransform item = ItemVase(itemType, itemSprite, ref itemDataJson.Idx, itemName);
        string explain = "";
        switch (itemType)
        {
            case ItemType.HPPotion:
                PotionCase(itemDataJson, onClickAction, item, itemName);
                explain = "HP + " + potionValue.ToString();
                break;
            case ItemType.MPPotion:
                PotionCase(itemDataJson, onClickAction,  item, itemName);
                explain = "MP + " + potionValue.ToString();
                break;
            case ItemType.Material:
                var matText = Instantiate(itemData.itemCountText, item);
                matText.GetComponent<Text>().text = itemDataJson.Count.ToString();
                item.GetComponent<Drag>().price = priceAndQuickIdx;
                explain = priceAndQuickIdx.ToString() + " 원";
                break;
        }
        item.GetComponent<ItemToolTip>().itemExplain = explain;


        RectTransform window = GetWindow();
        item.SetParent(window);
        string path = GetTransformPath(item);
        ItemSave(itemDataJson);
        ItemDataJson newitem = new ItemDataJson
        {
            Count = itemDataJson.Count,
            Name = itemName,
            Idx = itemDataJson.Idx,
            Path = path
        };
        item.GetComponent<Drag>().itemDataJson = newitem;
        item.gameObject.SetActive(true);
        return itemDataJson.Idx;
    }
    #endregion
    #region 아이템 체크 및 위치 저장
    public void AllItemCheck()
    {
        // 아이템 체크
        ItemCheck(ItemType.Sword, swordData.Image_01, ref swordDataJson[0], new float[] { swordData.Value_01[0] }, swordData.Name_01);
        ItemCheck(ItemType.Sword, swordData.Image_02, ref swordDataJson[1], new float[] { swordData.Value_02[0] }, swordData.Name_02);
        ItemCheck(ItemType.Sword, swordData.Image_03, ref swordDataJson[2], new float[] { swordData.Value_03[0] }, swordData.Name_03);
        ItemCheck(ItemType.Shield, shieldData.Image_01, ref shieldDataJson[0], new float[] { shieldData.Value_01[0] }, shieldData.Name_01);
        ItemCheck(ItemType.Shield, shieldData.Image_02, ref shieldDataJson[1], new float[] { shieldData.Value_02[0] }, shieldData.Name_02);
        ItemCheck(ItemType.Shield, shieldData.Image_03, ref shieldDataJson[2], new float[] { shieldData.Value_03[0] }, shieldData.Name_03);
        ItemCheck(ItemType.Shield, shieldData.Image_04, ref shieldDataJson[3], new float[] { shieldData.Value_04[0] }, shieldData.Name_04);
        ItemCheck(ItemType.Hat, hatData.Image_01, ref hatDataJson[0], new float[] { hatData.Value_01[0], hatData.Value_01[1] }, hatData.Name_01);
        ItemCheck(ItemType.Hat, hatData.Image_02, ref hatDataJson[1], new float[] { hatData.Value_02[0], hatData.Value_02[1] }, hatData.Name_02);
        ItemCheck(ItemType.Hat, hatData.Image_03, ref hatDataJson[2], new float[] { hatData.Value_03[0], hatData.Value_03[1] }, hatData.Name_03);
        ItemCheck(ItemType.Hat, hatData.Image_04, ref hatDataJson[3], new float[] { hatData.Value_04[0], hatData.Value_04[1] }, hatData.Name_04);
        ItemCheck(ItemType.Cloth, clothData.Image_01, ref clothDataJson[0], new float[] { clothData.Value_01[0], clothData.Value_01[1] }, clothData.Name_01);
        ItemCheck(ItemType.Cloth, clothData.Image_02, ref clothDataJson[1], new float[] { clothData.Value_02[0], clothData.Value_02[1] }, clothData.Name_02);
        ItemCheck(ItemType.Cloth, clothData.Image_03, ref clothDataJson[2], new float[] { clothData.Value_03[0], clothData.Value_03[1] }, clothData.Name_03);
        ItemCheck(ItemType.Pants, pantsData.Image_01, ref pantsDataJson[0], new float[] { pantsData.Value_01[0], pantsData.Value_01[1] }, pantsData.Name_01);
        ItemCheck(ItemType.Pants, pantsData.Image_02, ref pantsDataJson[1], new float[] { pantsData.Value_02[0], pantsData.Value_02[1] }, pantsData.Name_02);
        ItemCheck(ItemType.Pants, pantsData.Image_03, ref pantsDataJson[2], new float[] { pantsData.Value_03[0], pantsData.Value_03[1] }, pantsData.Name_03);
        ItemCheck(ItemType.Shoes, shoesData.Image_01, ref shoesDataJson[0], new float[] { shoesData.Value_01[0], shoesData.Value_01[1] }, shoesData.Name_01);
        ItemCheck(ItemType.Shoes, shoesData.Image_02, ref shoesDataJson[1], new float[] { shoesData.Value_02[0], shoesData.Value_02[1] }, shoesData.Name_02);
        ItemCheck(ItemType.Shoes, shoesData.Image_03, ref shoesDataJson[2], new float[] { shoesData.Value_03[0], shoesData.Value_03[1] }, shoesData.Name_03);
        ItemCheck(ItemType.Kloak, kloakData.Image_01, ref kloakDataJson[0], new float[] { kloakData.Value_01[0], kloakData.Value_01[1], kloakData.Value_01[2] }, kloakData.Name_01);
        ItemCheck(ItemType.Kloak, kloakData.Image_02, ref kloakDataJson[1], new float[] { kloakData.Value_02[0], kloakData.Value_02[1], kloakData.Value_02[2] }, kloakData.Name_02);
        ItemCheck(ItemType.Kloak, kloakData.Image_03, ref kloakDataJson[2], new float[] { kloakData.Value_03[0], kloakData.Value_03[1], kloakData.Value_03[2] }, kloakData.Name_03);
        ItemCheck(ItemType.Neck, neckData.Image_01, ref neckDataJson[0], new float[] { neckData.Value_01[0], neckData.Value_01[1], neckData.Value_01[2] }, neckData.Name_01);
        ItemCheck(ItemType.Neck, neckData.Image_02, ref neckDataJson[1], new float[] { neckData.Value_02[0], neckData.Value_02[1], neckData.Value_02[2] }, neckData.Name_02);
        ItemCheck(ItemType.Neck, neckData.Image_03, ref neckDataJson[2], new float[] { neckData.Value_03[0], neckData.Value_03[1], neckData.Value_03[2] }, neckData.Name_03);
        ItemCheck(ItemType.Neck, neckData.Image_04, ref neckDataJson[3], new float[] { neckData.Value_04[0], neckData.Value_04[1], neckData.Value_04[2] }, neckData.Name_04);
        ItemCheck(ItemType.Ring, ringData.Image_01, ref ringDataJson[0], new float[] { ringData.Value_01[0], ringData.Value_01[1], ringData.Value_01[2], ringData.Value_01[3],
            ringData.Value_01[4], ringData.Value_01[5], ringData.Value_01[6] }, ringData.Name_01);
        ItemCheck(ItemType.Ring, ringData.Image_02, ref ringDataJson[1], new float[] { ringData.Value_02[0], ringData.Value_02[1], ringData.Value_02[2], ringData.Value_02[3],
            ringData.Value_02[4], ringData.Value_02[5], ringData.Value_02[6] }, ringData.Name_02);
        CheckPotionMat(ItemType.HPPotion, potionData.HPpotion01, ref potionJson[0], potionData.HPpotion01quick, potionData.HPpotion01Name, HPHeal01, potionData.HPpotion01Value);
        CheckPotionMat(ItemType.HPPotion, potionData.HPpotion02, ref potionJson[1], potionData.HPpotion02quick, potionData.HPpotion02Name, HPHeal02, potionData.HPpotion02Value);
        CheckPotionMat(ItemType.HPPotion, potionData.HPpotion03, ref potionJson[2], potionData.HPpotion03quick, potionData.HPpotion03Name, HPHeal03, potionData.HPpotion03Value);
        CheckPotionMat(ItemType.MPPotion, potionData.MPpotion01, ref potionJson[3], potionData.MPpotion01quick, potionData.MPpotion01Name, MPHeal01, potionData.MPpotion01Value);
        CheckPotionMat(ItemType.MPPotion, potionData.MPpotion02, ref potionJson[4], potionData.MPpotion02quick, potionData.MPpotion02Name, MPHeal02, potionData.MPpotion02Value);
        CheckPotionMat(ItemType.MPPotion, potionData.MPpotion03, ref potionJson[5], potionData.MPpotion03quick, potionData.MPpotion03Name, MPHeal03, potionData.MPpotion03Value);

        CheckPotionMat(ItemType.Material, materialData.material01, ref materialJson[0], materialData.material01Price, materialData.material01Name);
        CheckPotionMat(ItemType.Material, materialData.material02, ref materialJson[1], materialData.material02Price, materialData.material02Name);
        CheckPotionMat(ItemType.Material, materialData.material03, ref materialJson[2], materialData.material03Price, materialData.material03Name);
        CheckPotionMat(ItemType.Material, materialData.material04, ref materialJson[3], materialData.material04Price, materialData.material04Name);
        CheckPotionMat(ItemType.Material, materialData.material05, ref materialJson[4], materialData.material05Price, materialData.material05Name);
        CheckPotionMat(ItemType.Material, materialData.material06, ref materialJson[5], materialData.material06Price, materialData.material06Name);
        CheckPotionMat(ItemType.Material, materialData.material07, ref materialJson[6], materialData.material07Price, materialData.material07Name);
        CheckPotionMat(ItemType.Material, materialData.material08, ref materialJson[7], materialData.material08Price, materialData.material08Name);
        GoldUpdate();
    }
    //아이템이 있는지 없는지 확인할 매서드
    private void ItemCheck(ItemType itemType, Sprite itemSprite, ref ItemDataJson itemDataJson, float[] values, string itemName)
    {
        if (itemDataJson != null && itemDataJson.Count > 0)
        {
            RectTransform item = ItemVase(itemType, itemSprite, ref itemDataJson.Idx, itemName);
            string explain = "";
            Transform trParent = GetParentTransformByPath(itemDataJson.Path);
            if (trParent != null)
            {
                ItemInfo itemInfo = item.GetComponent<ItemInfo>();
                if (trParent.GetComponent<ItemInfo>() != null && itemInfo.type == trParent.GetComponent<ItemInfo>().type)
                {
                    Drag itemDrag = item.GetComponent<Drag>();
                    itemDrag.IsEuqip(true);
                    if (itemType == ItemType.Sword || itemType == ItemType.Shield)
                    {
                        itemDrag.ItemTypeSelect(itemType, true);
                    }
                    explain = WeaponCase(itemType, values, item);
                }
                item.SetParent(trParent);
            }
            else
            {
                explain = WeaponCase(itemType, values, item);
                RectTransform window = GetWindow();
                item.SetParent(window);
            }
            item.GetComponent<Drag>().itemDataJson = itemDataJson;
            item.GetComponent<ItemToolTip>().itemExplain = explain;
            item.gameObject.SetActive(true);
        }
    }
    private void CheckPotionMat(ItemType itemType, Sprite itemSprite, ref ItemDataJson itemDataJson, int priceAndQuickIdx, string itemName, UnityAction onClickAction = null, float potionValue = 0)
    {
        if (itemDataJson != null && itemDataJson.Count > 0)
        {
            RectTransform item = ItemVase(itemType, itemSprite, ref itemDataJson.Idx, itemName);
            Transform trParent = GetParentTransformByPath(itemDataJson.Path);
            string explain = "";
            bool isPrent = false;
            if (trParent != null)
            {
                for (int i = 0; i < quickSlot.Count; i++)
                {
                    if (trParent == quickSlot[i])
                    {
                        switch (itemType)
                        {
                            case ItemType.HPPotion:
                                explain = "HP + " + potionValue.ToString();
                                break;
                            case ItemType.MPPotion:
                                explain = "MP + " + potionValue.ToString();
                                break;
                        }
                        var text = Instantiate(itemData.itemCountText, item);
                        text.GetComponent<Text>().text = itemDataJson.Count.ToString();
                        item.AddComponent<Button>();
                        isPrent = true;
                        item.GetComponent<Button>().onClick.AddListener(onClickAction);
                        getItem.SetAction(i, onClickAction);
                        item.SetParent(trParent);
                        break;
                    }
                }
                if (!isPrent)
                {
                    explain = PotionMatCheckMethod(itemType, priceAndQuickIdx, itemName, onClickAction, potionValue, itemDataJson, item, explain);
                    item.SetParent(trParent);
                }
            }
            else
            {
                explain = PotionMatCheckMethod(itemType, priceAndQuickIdx, itemName, onClickAction, potionValue, itemDataJson, item, explain);
                RectTransform window = GetWindow();
                item.SetParent(window);
            }
            item.GetComponent<Drag>().itemDataJson = itemDataJson;
            ItemSave(itemDataJson);
            item.GetComponent<ItemToolTip>().itemExplain = explain;
            item.gameObject.SetActive(true);
        }
    }

    private string PotionMatCheckMethod(ItemType itemType, int priceAndQuickIdx, string itemName, UnityAction onClickAction, float potionValue, ItemDataJson itemDataJson, RectTransform item, string explain)
    {
        switch (itemType)
        {
            case ItemType.HPPotion:
                PotionCase(itemDataJson, onClickAction, item, itemName);
                explain = "HP + " + potionValue.ToString();
                break;
            case ItemType.MPPotion:
                PotionCase(itemDataJson, onClickAction, item, itemName);
                explain = "MP + " + potionValue.ToString();
                break;
            case ItemType.Material:
                var matText = Instantiate(itemData.itemCountText, item);
                matText.GetComponent<Text>().text = itemDataJson.Count.ToString();
                item.GetComponent<Drag>().price = priceAndQuickIdx;
                explain = priceAndQuickIdx.ToString() + " 원";
                break;
        }
        return explain;
    }

    private void PotionCase(ItemDataJson itemDataJson, UnityAction onClickAction, RectTransform item, string itemName)
    {
        var text = Instantiate(itemData.itemCountText, item);
        text.GetComponent<Text>().text = itemDataJson.Count.ToString();
        item.AddComponent<Button>();
        item.GetComponent<Button>().onClick.AddListener(onClickAction);
        EventTrigger eventTrigger = item.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        itemDataJson.Name = itemName;
        Debug.Log(itemDataJson.Name);
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data, onClickAction, itemDataJson); });
        eventTrigger.triggers.Add(entry);
    }

    // 경로에 따라 Transform을 반환하는 메서드
    private Transform GetTransformByPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;

        string[] pathElements = path.Split('/');
        Transform currentTransform = null;

        foreach (string element in pathElements)
        {
            if (currentTransform == null)
            {
                // 루트 Transform을 찾기
                GameObject obj = GameObject.Find(element);
                if (obj != null) { currentTransform = obj.transform; }
                else { return null; }
            }
            else
            {
                // 현재 Transform의 자식 Transform을 찾기
                Transform childTransform = currentTransform.Find(element);
                if (childTransform != null) { currentTransform = childTransform; }  // Tr이 존재한다면 Tr을 반환한다
                else { return null; }
            }
        }

        return currentTransform;
    }
    // 부모를 찾는 메서드
    private Transform GetParentTransformByPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        string[] pathElements = path.Split('/');
        if (pathElements.Length <= 1) { return null; }
        // 부모 경로 분리
        string parentPath = string.Join("/", pathElements, 0, pathElements.Length - 1);
        // 부모 Transform 찾기
        Transform parentTransform = GetTransformByPath(parentPath);
        if (parentTransform == null) { return null; }
        return parentTransform;
    }
    // 아이템의 위치를 경로로 저장할 메서드
    public void ItemSave(ItemDataJson itemDataJson)
    {
        string path = GetTransformPath(itemList[itemDataJson.Idx]);
        DataManager.dataInst.SaveItemData(itemDataJson.Name, path, itemDataJson.Count, itemDataJson.Idx);
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
    private RectTransform GetWindow()
    {
        foreach (RectTransform window in windowList)
        {
            if (window.childCount == 0) return window;
        }
        return null;
    }
    public void GoldPlus(int gold)
    {
        GM.playerDataJson.GoldValue += gold;
        goldText.text = GM.playerDataJson.GoldValue.ToString();
    }
    public void GoldUpdate() { goldText.text = GM.playerDataJson.GoldValue.ToString(); }  // 골드 상태 업데이트
    #region 포션 메서드
    //*****************************포션 사용*********************************//


    public void HPHeal01() => Heal("HP", 50, ref potionJson[0]);
    public void HPHeal02() => Heal("HP", 100, ref potionJson[1]);
    public void HPHeal03() => Heal("HP", 500, ref potionJson[2]);
    public void MPHeal01() => Heal("MP", 50, ref potionJson[3]);
    public void MPHeal02() => Heal("MP", 100, ref potionJson[4]);
    public void MPHeal03() => Heal("MP", 500, ref potionJson[5]);
    public void Heal(string type, int amount, ref ItemDataJson itemDataJson)
    {
        if (type == "HP")
        {
            GM.playerDataJson.HP += amount;
            hpHealingEff.SetActive(true);
            hpImage.fillAmount = GM.playerDataJson.HP / GM.playerDataJson.MaxHP;
            if (GM.playerDataJson.HP > GM.playerDataJson.MaxHP)
            {
                GM.playerDataJson.HP = GM.playerDataJson.MaxHP;
            }
            GameManager.GM.StatUpdate(PlayerStat.HP);
        }
        else if (type == "MP")
        {
            GM.playerDataJson.MP += amount;
            mpHealingEff.SetActive(true);
            mpImage.fillAmount = GM.playerDataJson.MP / GM.playerDataJson.MaxMP;
            if (GM.playerDataJson.MP > GM.playerDataJson.MaxMP)
            {
                GM.playerDataJson.MP = GM.playerDataJson.MaxMP;
            }
            GameManager.GM.StatUpdate(PlayerStat.MP);
        }

        itemDataJson.Count--;
        itemList[itemDataJson.Idx].GetComponentInChildren<Text>().text = itemDataJson.Count.ToString();

        if (itemDataJson.Count == 0)
        {
            ClearPotion(itemDataJson.Idx);
            itemList[itemDataJson.Idx].gameObject.SetActive(false);
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
    private void OnPointerDownDelegate(PointerEventData data, UnityAction action, ItemDataJson itemDataJson)
    {
        if (data.button == PointerEventData.InputButton.Right)
        {
            isSelect = false;
            StartCoroutine(PotionSelect(action, itemDataJson));
        }
    }
    IEnumerator PotionSelect(UnityAction action, ItemDataJson itemDataJson)
    {
        while (!isSelect)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Alpha1)) { PotionSelectMethod(action, itemDataJson, 0); }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) { PotionSelectMethod(action, itemDataJson, 1); }
            else if (Input.GetKeyDown(KeyCode.Alpha3)) { PotionSelectMethod(action, itemDataJson, 2); }
            else if (Input.GetKeyDown(KeyCode.Alpha4)) { PotionSelectMethod(action, itemDataJson, 3); }
        }
    }

    private void PotionSelectMethod(UnityAction action, ItemDataJson itemDataJson, int quickIdx)
    {
        getItem.SetAction(quickIdx, action);
        itemList[itemDataJson.Idx].SetParent(quickSlot[quickIdx]);
        ItemSave( itemDataJson);
        isSelect = true;
    }
    #endregion
    // 아이템을 다썻을때
    public void ClearMaterial(int itemIdx)
    {
        itemList[itemIdx].gameObject.SetActive(false);
        itemList[itemIdx].SetParent(itemGroup);
    }
    //****************************아이템 착용 확인**************************//
    public void SwordOnOff(bool _active) { playerAttack.IsSword(_active); }
    public void ShieldTakeOff(bool _active) { playerAttack.IsShield(_active); }
    // 새로 게임을 시작할때
}
[System.Serializable]
public class ItemDrop
{
    public float dropRate;
    public System.Action getItemAction;
}
