using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] PlayerData playerData;
    [SerializeField] ItemData itemData;
    [SerializeField] RectTransform inventoryTr;
    [SerializeField] RectTransform equipmentTr;
    private RectTransform itemTr;
    public Vector2 origPos;
    private CanvasGroup canvasGroup;
    ItemInfo itemInfo;
    public static GameObject DraggingItem = null;
    //************ ������ ���� ������ ����*******************//
    public float hp = 0;
    public float mp = 0;
    public float damage = 0;
    public float defence = 0;
    public float magicDamage = 0;
    public float fatalProbabillity = 0;
    public float fatalValue = 0;
    public int price = 0;
    bool isEquip;
    Transform parent;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemTr = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        itemInfo = GetComponent<ItemInfo>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {//Ŭ���ϸ� DraaginItem�� ���� �ٲ۴�
        DraggingItem = this.transform.gameObject;
        canvasGroup.blocksRaycasts = false;
        origPos = itemTr.position;
        parent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {// Ŭ���ϴµ��� ��ġ�� �Ǵ��Ͽ� ��ġ�� �κ��丮 ���̳� ������̳Ŀ� ���� �θ� �޶���
        itemTr.position = Input.mousePosition;
        Vector2 pos = itemTr.position;
        if ((100.0f < pos.x && pos.x < 750.0f) && (200.0f < pos.y && pos.y < 900.0f))
        {
            itemTr.SetParent(equipmentTr);
        }
        else if ((1000.0f < pos.x && pos.x < 1800.0f) && (150.0f < pos.y && pos.y < 900.0f))
        {
            itemTr.SetParent(inventoryTr);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DraggingItem = null;
        canvasGroup.blocksRaycasts = true;
        if (itemTr.parent == equipmentTr || itemTr.parent == inventoryTr)
        {//�� �θ� �κ��丮�� ���Ծƴ� �κ��丮â, ���â���ӹ����ִٸ� �����ִ� ��ġ�� ������
            transform.SetParent(parent);
        }
        if(transform.parent.GetComponent<ItemInfo>() != null)
        {
            if (transform.parent.gameObject.GetComponent<ItemInfo>().type == itemInfo.type)
            {// �������� ���â�� ���ٸ� 
                if (itemInfo.type == ItemData.ItemType.Sword || itemInfo.type == ItemData.ItemType.Shield)
                    ItemTypeSelect(itemInfo.type, true);
                StatUp();
                isEquip = true;
                GameManager.GM.AllStatUpdata();
                ItemManager.itemInst.AllItemTrSave();
                switch(itemInfo.type)
                {
                    case ItemData.ItemType.Sword:
                        itemData.isSword = true;
                        break;
                    case ItemData.ItemType.Shield:
                        itemData.isShield = true;
                        break;
                    case ItemData.ItemType.Hat:
                        itemData.isHat = true;
                        break;
                    case ItemData.ItemType.Cloth:
                        itemData.isCloth = true;
                        break;
                    case ItemData.ItemType.Pants:
                        itemData.isPants = true;
                        break;
                    case ItemData.ItemType.Shoes:
                        itemData.isShoes = true;
                        break;
                    case ItemData.ItemType.Kloak:
                        itemData.isKloak = true;
                        break;
                    case ItemData.ItemType.Neck:
                        itemData.isNeck = true;
                        break;
                    case ItemData.ItemType.Ring:
                        itemData.isRing = true;
                        break;
                }
            }
            else if(transform.parent.GetComponent<ItemInfo>().type != itemInfo.type)
            {
                if (itemInfo.type == ItemData.ItemType.Sword || itemInfo.type == ItemData.ItemType.Shield)
                    ItemTypeSelect(itemInfo.type ,false);
                transform.SetParent(parent);
                if(isEquip)
                    StatDown();
                isEquip = false;
                switch (itemInfo.type)
                {
                    case ItemData.ItemType.Sword:
                        itemData.isSword = false;
                        break;
                    case ItemData.ItemType.Shield:
                        itemData.isShield = false;
                        break;
                    case ItemData.ItemType.Hat:
                        itemData.isHat = false;
                        break;
                    case ItemData.ItemType.Cloth:
                        itemData.isCloth = false;
                        break;
                    case ItemData.ItemType.Pants:
                        itemData.isPants = false;
                        break;
                    case ItemData.ItemType.Shoes:
                        itemData.isShoes = false;
                        break;
                    case ItemData.ItemType.Kloak:
                        itemData.isKloak = false;
                        break;
                    case ItemData.ItemType.Neck:
                        itemData.isNeck = false;
                        break;
                    case ItemData.ItemType.Ring:
                        itemData.isRing = false;
                        break;
                }
                GameManager.GM.AllStatUpdata();
                ItemManager.itemInst.AllItemTrSave();
            }
        }
        else
        {
            if (itemInfo.type == ItemData.ItemType.Sword || itemInfo.type == ItemData.ItemType.Shield)
                ItemTypeSelect(itemInfo.type, false);
            if (isEquip)
                StatDown();
            isEquip = false;
            GameManager.GM.AllStatUpdata();
            ItemManager.itemInst.AllItemTrSave();
        }
    }

    private void StatUp()
    {
        playerData.HP += hp;
        playerData.MP += mp;
        playerData.AttackValue += damage;
        playerData.DefenceValue += defence;
        playerData.MagicAttackValue += magicDamage;
        playerData.FatalProbability += fatalProbabillity;
        playerData.FatalValue += fatalValue;
    }
    private void StatDown()
    {
        playerData.HP -= hp;
        playerData.MP -= mp;
        playerData.AttackValue -= damage;
        playerData.DefenceValue -= defence;
        playerData.MagicAttackValue -= magicDamage;
        playerData.FatalProbability -= fatalProbabillity;
        playerData.FatalValue -= fatalValue;
    }


    public void ItemTypeSelect(ItemData.ItemType type,bool _bool)
    {
        switch (type)
        {
            case ItemData.ItemType.Sword:
                ItemManager.itemInst.SwordOnOff(_bool);
                break;
            case ItemData.ItemType.Shield:
                ItemManager.itemInst.ShieldTakeOff(_bool);
                break;
        }
    }
    public void ItemChange(ItemData.ItemType type, float value01)
    {
        switch (type)
        {
            case ItemData.ItemType.Sword:
                damage = value01;
                break;
            case ItemData.ItemType.Shield:
                defence = value01;
                break;
        }
    }
    public void ItemChange(ItemData.ItemType type, float HP, float Defence)
    {
        switch (type)
        {
            case ItemData.ItemType.Hat:
                hp = HP;
                defence = Defence;
                break;
            case ItemData.ItemType.Cloth:
                hp = HP;
                defence = Defence;
                break;
            case ItemData.ItemType.Pants:
                hp = HP;
                defence = Defence;
                break;
            case ItemData.ItemType.Shoes:
                hp = HP;
                defence = Defence;
                break;
        }
    }
    public void ItemChange(ItemData.ItemType type, float HPAndMP, float MPAndDamage, float DefenceAndMagiceDamage)
    {
        switch (type)
        {
            case ItemData.ItemType.Kloak:
                hp = HPAndMP;
                mp = MPAndDamage;
                defence = DefenceAndMagiceDamage;
                break;
            case ItemData.ItemType.Neck:
                mp = MPAndDamage;
                damage = MPAndDamage;
                magicDamage = DefenceAndMagiceDamage;
                break;
        }
    }
    public void ItemChange(ItemData.ItemType type,float HP, float MP, float Damage, float Defence, float MagicDamage, float FatalProbabillity, float FatalValue)
    {
        switch (type)
        {
            case ItemData.ItemType.Ring:
                hp = HP;
                mp = MP;
                defence = Defence;
                fatalValue = FatalValue;
                fatalProbabillity = FatalProbabillity;
                magicDamage = MagicDamage;
                damage = Damage;
                break;
        }
    }
    public void IsEuqip(bool _isEquip)
    {
        isEquip = _isEquip;
    }
}
